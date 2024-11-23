package four.pda.ui.article.search;

import android.view.View;
import android.widget.LinearLayout;
import android.widget.SearchView;
import android.widget.TextView;

import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.snackbar.Snackbar;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.Click;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.InstanceState;
import org.androidannotations.annotations.ViewById;
import org.apache.commons.lang3.StringUtils;

import javax.inject.Inject;

import four.pda.App;
import four.pda.Dao;
import four.pda.EventBus;
import four.pda.R;
import four.pda.analytics.Analytics;
import four.pda.client.FourPdaClient;
import four.pda.client.model.SearchContainer;
import four.pda.ui.BaseFragment;
import four.pda.ui.Keyboard;
import four.pda.ui.SupportView;

/**
 * Created by asavinova on 07/05/16.
 */
@EFragment(R.layout.search)
public class SearchFragment extends BaseFragment {

	private static final int LOADER_ID = 0;

	@ViewById LinearLayout layout;
	@ViewById SearchView searchView;
	@ViewById TextView allArticlesCountView;
	@ViewById RecyclerView recyclerView;
	@ViewById View upButton;
	@ViewById SupportView supportView;

	@Bean Dao dao;
	@Bean EventBus eventBus;
	@Bean Analytics analytics;

	@Inject FourPdaClient client;
	@Inject Keyboard keyboard;

	@InstanceState String currentSearchCriteria;
	@InstanceState int allArticlesCount;
	@InstanceState int currentPage;
	@InstanceState boolean hasNextPage;

	private SearchAdapter adapter;
	private LinearLayoutManager layoutManager;

	@AfterViews
	void afterViews() {
		((App) getActivity().getApplication()).component().inject(this);

		analytics.search().open();

		searchView.onActionViewExpanded();
		searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {

			@Override
			public boolean onQueryTextSubmit(String query) {
				keyboard.hide(getActivity());
				searchView.clearFocus();
				analytics.search().load(currentSearchCriteria);
				loadData();
				return true;
			}

			@Override
			public boolean onQueryTextChange(String newSearchQuery) {
				if (!StringUtils.equals(currentSearchCriteria, newSearchQuery)) {
					resetViews();
				}
				currentSearchCriteria = newSearchQuery;
				return false;
			}

		});

		layoutManager = new LinearLayoutManager(getActivity(), LinearLayoutManager.VERTICAL, false);
		adapter = new SearchAdapter(getActivity(), null);

		recyclerView.setLayoutManager(layoutManager);
		recyclerView.setAdapter(adapter);

		recyclerView.addOnScrollListener(new RecyclerView.OnScrollListener() {
			@Override
			public void onScrolled(RecyclerView recyclerView, int dx, int dy) {

				int visibleItemCount = layoutManager.getChildCount();
				int totalItemCount = layoutManager.getItemCount();
				int firstVisibleItemPosition = layoutManager.findFirstVisibleItemPosition();

				boolean isLoadInProgress = supportView.isLoading();
				int loadLevel = totalItemCount - visibleItemCount * 2;
				boolean shouldLoadMore = firstVisibleItemPosition >= loadLevel;
				if (!isLoadInProgress && hasNextPage && shouldLoadMore) {
					loadData();
				}

			}
		});

		if (currentPage == 0) {
			keyboard.toggle(searchView);
		} else {
			updateViews();
		}

	}

	@Click
	void upButton() {
		analytics.search().scrollUp(layoutManager.findFirstVisibleItemPosition());
		layoutManager.scrollToPosition(0);
	}

	@Click
	void arrowBackButton() {
		getActivity().finish();
	}

	private void resetViews() {
		currentPage = 0;
		allArticlesCountView.setVisibility(View.GONE);
		adapter.swapCursor(null);
		upButton.setVisibility(View.GONE);
	}

	void loadData() {
		if (adapter.isEmpty()) {
			supportView.showProgress();
		}
		getLoaderManager().restartLoader(
				LOADER_ID,
				SearchCallbacks.createBundle(currentSearchCriteria, currentPage),
				new SearchCallbacks(this)
		).forceLoad();
	}

	void onNewDataLoaded(SearchContainer container) {

		boolean needClearData = currentPage == 0;
		dao.setSearchArticles(container.getArticles(), needClearData);

		currentPage++;
		hasNextPage = container.hasNextPage();
		allArticlesCount = container.getAllArticlesCount();

		updateViews();
	}

	private void updateViews() {
		allArticlesCountView.setText(getString(R.string.search_articles_count, allArticlesCount));
		allArticlesCountView.setVisibility(View.VISIBLE);

		adapter.swapCursor(dao.getSearchArticleCursor());
		adapter.notifyDataSetChanged();

		supportView.hide();

		if (!adapter.isEmpty()) {
			upButton.setVisibility(View.VISIBLE);
		}
	}

	void showError() {

		View.OnClickListener retryListener = v -> loadData();

		if (adapter.isEmpty()) {
			upButton.setVisibility(View.GONE);
			supportView.showError(getString(R.string.article_list_network_error), retryListener);
			return;
		}

		Snackbar
				.make(layout, R.string.article_list_network_error, Snackbar.LENGTH_INDEFINITE)
				.setAction(R.string.retry_button, retryListener)
				.show();

	}

}
