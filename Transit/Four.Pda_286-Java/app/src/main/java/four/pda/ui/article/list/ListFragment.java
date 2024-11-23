package four.pda.ui.article.list;

import android.util.TypedValue;
import android.view.View;

import androidx.appcompat.widget.Toolbar;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.Click;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.FragmentArg;
import org.androidannotations.annotations.ViewById;

import javax.inject.Inject;

import four.pda.App;
import four.pda.Dao;
import four.pda.R;
import four.pda.analytics.Analytics;
import four.pda.client.CategoryType;
import four.pda.client.FourPdaClient;
import four.pda.ui.BaseFragment;
import four.pda.ui.CategoryTitleMap;
import four.pda.ui.SupportView;
import four.pda.ui.article.SearchActivity_;

/**
 * Created by asavinova on 10/04/15.
 */
@EFragment(R.layout.article_list)
public class ListFragment extends BaseFragment implements SwipeRefreshLayout.OnRefreshListener {

	private static final int LOADER_ID = 0;

	@FragmentArg CategoryType category;

	@ViewById View container;
	@ViewById Toolbar toolbar;
	@ViewById SwipeRefreshLayout refresh;
	@ViewById RecyclerView recyclerView;
	@ViewById View upButton;
	@ViewById SupportView supportView;

	@Bean Dao dao;
	@Bean Analytics analytics;

	@Inject FourPdaClient client;

	int page = 1;
	ArticlesAdapter adapter;

	private GridLayoutManager layoutManager;

	@AfterViews
	void afterViews() {
		((App) getActivity().getApplication()).component().inject(this);

		toolbar.setTitle(CategoryTitleMap.get(category));
		showMenuIcon();

		toolbar.inflateMenu(R.menu.articles_list);
		toolbar.getMenu().findItem(R.id.search).setOnMenuItemClickListener(item -> {
			SearchActivity_.intent(getActivity()).start();
			return true;
		});

		container.getViewTreeObserver().addOnGlobalLayoutListener(() -> {
			if (container == null) return;

			int spanCount = (int) (container.getWidth() / container.getResources().getDimension(R.dimen.list_item_width));
			if (spanCount > 1) {
				layoutManager.setSpanCount(spanCount);
			}
		});

		layoutManager = new GridLayoutManager(getActivity(), 1, LinearLayoutManager.VERTICAL, false);
		recyclerView.setLayoutManager(layoutManager);
		adapter = new ArticlesAdapter(getActivity(), null);
		recyclerView.setAdapter(adapter);

		recyclerView.addOnScrollListener(new RecyclerView.OnScrollListener() {
			@Override
			public void onScrolled(RecyclerView recyclerView, int dx, int dy) {

				int visibleItemCount = layoutManager.getChildCount();
				int totalItemCount = layoutManager.getItemCount();
				int firstVisibleItemPosition = layoutManager.findFirstVisibleItemPosition();

				int loadLevel = totalItemCount - visibleItemCount;
				boolean shouldLoad = firstVisibleItemPosition >= loadLevel;

				if (!refresh.isRefreshing() && shouldLoad) {
					loadData();
				}
			}
		});

		refresh.setOnRefreshListener(this);
		refresh.setColorSchemeResources(R.color.primary);

		float progressOffset = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 24, getResources().getDisplayMetrics());
		refresh.setProgressViewOffset(false, 0, (int) progressOffset);

		loadData();
	}

	@Click
	void upButton() {
		analytics.articlesList().scrollUp(layoutManager.findFirstVisibleItemPosition());
		layoutManager.scrollToPosition(0);
	}

	void loadData() {
		refresh.setRefreshing(true);

		int itemCount = adapter.getItemCount();
		if (itemCount == 0) {
			supportView.showProgress();
		}

		getLoaderManager().restartLoader(LOADER_ID, null, new ListCallbacks(this)).forceLoad();
	}

	@Override
	public void onRefresh() {
		page = 1;
		loadData();
	}

	public CategoryType getCategory() {
		return category;
	}

}
