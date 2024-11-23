using Android.Os;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Org.Slf4j;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Search
{
    /// <summary>
    /// Created by asavinova on 07/05/16.
    /// </summary>
    public class SearchCallbacks : LoaderCallbacks<LoadResult<SearchContainer>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(SearchCallbacks));
        private static readonly string SEARCH_CRITERIA_BUNDLE_ARG = "search";
        private static readonly string CURRENT_PAGE_BUNDLE_ARG = "page";
        private SearchFragment fragment;
        public SearchCallbacks(SearchFragment fragment)
        {
            this.fragment = fragment;
        }

        public static Bundle CreateBundle(string searchText, int currentPage)
        {
            Bundle bundle = new Bundle();
            bundle.PutString(SEARCH_CRITERIA_BUNDLE_ARG, searchText);
            bundle.PutInt(CURRENT_PAGE_BUNDLE_ARG, currentPage);
            return bundle;
        }

        public virtual Loader<LoadResult<SearchContainer>> OnCreateLoader(int id, Bundle args)
        {
            string searchCriteria = args.GetString(SEARCH_CRITERIA_BUNDLE_ARG);
            int currentPage = args.GetInt(CURRENT_PAGE_BUNDLE_ARG);
            return new AnonymousAsyncTaskLoader(fragment.GetActivity());
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(SearchCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly SearchCallbacks parent;
            public LoadResult<SearchContainer> LoadInBackground()
            {
                try
                {
                    SearchContainer container = fragment.client.SearchArticles(searchCriteria, currentPage + 1);
                    return new LoadResult(container);
                }
                catch (Exception e)
                {
                    L.Error("Search articles request error", e);
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<SearchContainer>> loader, LoadResult<SearchContainer> result)
        {
            fragment.supportView.Hide();
            if (result.IsError())
            {
                fragment.ShowError();
                return;
            }

            fragment.OnNewDataLoaded(result.GetData());
        }

        public virtual void OnLoaderReset(Loader<LoadResult<SearchContainer>> loader)
        {
        }
    }
}