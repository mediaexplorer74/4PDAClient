package four.pda.ui.article;

import android.os.Bundle;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.EActivity;
import org.androidannotations.annotations.FragmentById;
import org.androidannotations.annotations.InstanceState;
import org.androidannotations.annotations.ViewById;
import org.androidannotations.annotations.sharedpreferences.Pref;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.EventBus;
import four.pda.Preferences_;
import four.pda.R;
import four.pda.client.CategoryType;
import four.pda.ui.DrawerFragment;
import four.pda.ui.article.comments.CommentsFragment;
import four.pda.ui.article.comments.CommentsFragment_;
import four.pda.ui.article.list.ListFragment;
import four.pda.ui.article.list.ListFragment_;
import four.pda.ui.article.one.ArticleFragment;
import four.pda.ui.article.one.ArticleFragment_;

/**
 * Created by asavinova on 10/04/15.
 */
@EActivity(R.layout.activity_articles)
public class NewsActivity extends AppCompatActivity implements DrawerFragment.ChangeCategoryListener {

	private static final Logger L = LoggerFactory.getLogger(NewsActivity.class);

	@FragmentById DrawerFragment drawer;

	@ViewById DrawerLayout drawerLayout;

	@Pref Preferences_ preferences;
	@Bean EventBus eventBus;

	@InstanceState CategoryType category = CategoryType.ALL;

	private long backButtonLastPressedTime;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		if (savedInstanceState == null) {

			L.debug("Add list fragment with category {}", category);

			ListFragment fragment = ListFragment_.builder()
					.category(category)
					.build();

			getSupportFragmentManager().beginTransaction()
					.add(R.id.list_container, fragment)
					.commit();
		}

		getSupportFragmentManager().addOnBackStackChangedListener(() -> {
			Fragment fragment = getSupportFragmentManager().findFragmentById(R.id.list_container);

			if (fragment == null) {
				return;
			}

			category = ((ListFragment) fragment).getCategory();
			drawer.setCategorySelected(category);
		});
	}

	@AfterViews
	void afterViews() {
		if (preferences.isFirstRun().get()) {
			if (drawerLayout != null) {
				drawerLayout.openDrawer(GravityCompat.START);
			}
			preferences.isFirstRun().put(false);
		}
		drawer.setCategorySelected(category);
	}

	@Override
	protected void onResume() {
		super.onResume();
		drawer.addListener(this);
		eventBus.register(this);
	}

	@Override
	protected void onPause() {
		super.onPause();
		drawer.removeListener(this);
		eventBus.unregister(this);
	}

	@Override
	public void onChange(CategoryType newCategory) {
		L.debug("Category changed to {}", newCategory.name());

		if (drawerLayout != null) {
			drawerLayout.closeDrawer(GravityCompat.START);
		}

		Fragment itemFragment = getSupportFragmentManager().findFragmentById(R.id.item_container);

		if (category == newCategory) {
			// Если категория та же, то убираем фрагмент со статьей и все
			if (itemFragment != null) {
				getSupportFragmentManager().beginTransaction()
						.remove(itemFragment)
						.addToBackStack(null)
						.commit();
			}
			return;
		}

		ListFragment listFragment = ListFragment_.builder()
				.category(newCategory)
				.build();

		FragmentTransaction transaction = getSupportFragmentManager().beginTransaction()
				.replace(R.id.list_container, listFragment);

		if (itemFragment != null) {
			transaction.remove(itemFragment);
		}

		transaction
				.addToBackStack(null)
				.commit();

		category = newCategory;
	}

	public void onEvent(ShowArticleEvent event) {
		L.debug("Show article with id {}", event.getId());

		ArticleFragment fragment = ArticleFragment_.builder()
				.id(event.getId())
				.date(event.getDate())
				.title(event.getTitle())
				.image(event.getImage())
				.authorId(event.getAuthorId())
				.authorName(event.getAuthorName())
				.labelName(event.getLabelName())
				.labelColor(event.getLabelColor())
				.build();

		getSupportFragmentManager().beginTransaction()
				.replace(R.id.item_container, fragment)
				.addToBackStack(null)
				.commit();
	}

	public void onEvent(ShowArticleCommentsEvent event) {
		L.debug("Show comments for article with id {}", event.getArticleId());

		CommentsFragment fragment = CommentsFragment_.builder()
				.articleId(event.getArticleId())
				.articleDate(event.getArticleDate())
				.build();

		getSupportFragmentManager().beginTransaction()
				.replace(R.id.item_container, fragment)
				.addToBackStack(null)
				.commit();
	}

	@Override
	public void onBackPressed() {

		if (getSupportFragmentManager().getBackStackEntryCount() > 0) {
			super.onBackPressed();
			return;
		}

		if (System.currentTimeMillis() - backButtonLastPressedTime > 2000) {
			Toast.makeText(this, R.string.exit_message, Toast.LENGTH_SHORT).show();
			backButtonLastPressedTime = System.currentTimeMillis();
			return;
		}

		finish();
	}

}
