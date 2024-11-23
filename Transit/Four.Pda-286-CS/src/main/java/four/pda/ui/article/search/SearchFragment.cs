using Android.View;
using Android.Widget;
using Androidx.Recyclerview.Widget;
using Com.Google.Android.Material.Snackbar;
using Org.Androidannotations.Annotations;
using Org.Apache.Commons.Lang3;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Search
{
    /// <summary>
    /// Created by asavinova on 07/05/16.
    /// </summary>
    public class SearchFragment : BaseFragment
    {
        private static readonly int LOADER_ID = 0;
        LinearLayout layout;
        SearchView searchView;
        TextView allArticlesCountView;
        RecyclerView recyclerView;
        View upButton;
        SupportView supportView;
        Dao dao;
        EventBus eventBus;
        Analytics analytics;
        FourPdaClient client;
        Keyboard keyboard;
        string currentSearchCriteria;
        int allArticlesCount;
        int currentPage;
        bool hasNextPage;
        private SearchAdapter adapter;
        private LinearLayoutManager layoutManager;
        virtual void AfterViews()
        {
            ((App)GetActivity().GetApplication()).Component().Inject(this);
            analytics.Search().Open();
            searchView.OnActionViewExpanded();
            searchView.SetOnQueryTextListener(new AnonymousOnQueryTextListener(this));
            layoutManager = new LinearLayoutManager(GetActivity(), LinearLayoutManager.VERTICAL, false);
            adapter = new SearchAdapter(GetActivity(), null);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(adapter);
            recyclerView.AddOnScrollListener(new AnonymousOnScrollListener(this));
            if (currentPage == 0)
            {
                keyboard.Toggle(searchView);
            }
            else
            {
                UpdateViews();
            }
        }

        private sealed class AnonymousOnQueryTextListener : OnQueryTextListener
        {
            public AnonymousOnQueryTextListener(SearchFragment parent)
            {
                this.parent = parent;
            }

            private readonly SearchFragment parent;
            public bool OnQueryTextSubmit(string query)
            {
                keyboard.Hide(GetActivity());
                searchView.ClearFocus();
                analytics.Search().Load(currentSearchCriteria);
                LoadData();
                return true;
            }

            public bool OnQueryTextChange(string newSearchQuery)
            {
                if (!StringUtils.Equals(currentSearchCriteria, newSearchQuery))
                {
                    ResetViews();
                }

                currentSearchCriteria = newSearchQuery;
                return false;
            }
        }

        private sealed class AnonymousOnScrollListener : OnScrollListener
        {
            public AnonymousOnScrollListener(SearchFragment parent)
            {
                this.parent = parent;
            }

            private readonly SearchFragment parent;
            public void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                int visibleItemCount = layoutManager.GetChildCount();
                int totalItemCount = layoutManager.GetItemCount();
                int firstVisibleItemPosition = layoutManager.FindFirstVisibleItemPosition();
                bool isLoadInProgress = supportView.IsLoading();
                int loadLevel = totalItemCount - visibleItemCount * 2;
                bool shouldLoadMore = firstVisibleItemPosition >= loadLevel;
                if (!isLoadInProgress && hasNextPage && shouldLoadMore)
                {
                    LoadData();
                }
            }
        }

        virtual void UpButton()
        {
            analytics.Search().ScrollUp(layoutManager.FindFirstVisibleItemPosition());
            layoutManager.ScrollToPosition(0);
        }

        virtual void ArrowBackButton()
        {
            GetActivity().Finish();
        }

        private void ResetViews()
        {
            currentPage = 0;
            allArticlesCountView.SetVisibility(View.GONE);
            adapter.SwapCursor(null);
            upButton.SetVisibility(View.GONE);
        }

        virtual void LoadData()
        {
            if (adapter.IsEmpty())
            {
                supportView.ShowProgress();
            }

            GetLoaderManager().RestartLoader(LOADER_ID, SearchCallbacks.CreateBundle(currentSearchCriteria, currentPage), new SearchCallbacks(this)).ForceLoad();
        }

        virtual void OnNewDataLoaded(SearchContainer container)
        {
            bool needClearData = currentPage == 0;
            dao.SetSearchArticles(container.GetArticles(), needClearData);
            currentPage++;
            hasNextPage = container.HasNextPage();
            allArticlesCount = container.GetAllArticlesCount();
            UpdateViews();
        }

        private void UpdateViews()
        {
            allArticlesCountView.SetText(GetString(R.@string.search_articles_count, allArticlesCount));
            allArticlesCountView.SetVisibility(View.VISIBLE);
            adapter.SwapCursor(dao.GetSearchArticleCursor());
            adapter.NotifyDataSetChanged();
            supportView.Hide();
            if (!adapter.IsEmpty())
            {
                upButton.SetVisibility(View.VISIBLE);
            }
        }

        virtual void ShowError()
        {
            View.OnClickListener retryListener = (v) => LoadData();
            if (adapter.IsEmpty())
            {
                upButton.SetVisibility(View.GONE);
                supportView.ShowError(GetString(R.@string.article_list_network_error), retryListener);
                return;
            }

            Snackbar.Make(layout, R.@string.article_list_network_error, Snackbar.LENGTH_INDEFINITE).SetAction(R.@string.retry_button, retryListener).Show();
        }
    }
}