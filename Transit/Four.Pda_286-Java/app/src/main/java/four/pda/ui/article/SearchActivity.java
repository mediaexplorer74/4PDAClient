package four.pda.ui.article;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.EActivity;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.EventBus;
import four.pda.R;
import four.pda.ui.article.comments.CommentsFragment;
import four.pda.ui.article.comments.CommentsFragment_;
import four.pda.ui.article.one.ArticleFragment;
import four.pda.ui.article.one.ArticleFragment_;
import four.pda.ui.article.search.SearchFragment;
import four.pda.ui.article.search.SearchFragment_;

/**
 * Created by asavinova on 02/06/16.
 */
@EActivity(R.layout.activity_search)
public class SearchActivity extends AppCompatActivity {

	private static final Logger L = LoggerFactory.getLogger(SearchActivity.class);

	@Bean EventBus eventBus;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		if (savedInstanceState == null) {
			SearchFragment fragment = SearchFragment_.builder().build();
			getSupportFragmentManager().beginTransaction()
					.add(R.id.list_container, fragment)
					.commit();
		}
	}

	@Override
	protected void onResume() {
		super.onResume();
		eventBus.register(this);
	}

	@Override
	protected void onPause() {
		super.onPause();
		eventBus.unregister(this);
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

		finish();
	}


}
