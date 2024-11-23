using Android.Database;
using Android.Os;
using Android.View;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Com.Google.Android.Material.Snackbar;
using Org.Slf4j;
using Java.Util;
using Four.Pda;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.List
{
    /// <summary>
    /// Created by asavinova on 24/05/16.
    /// </summary>
    public class ListCallbacks : LoaderCallbacks<LoadResult<IList<ListArticle>>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(ListCallbacks));
        private ListFragment fragment;
        public ListCallbacks(ListFragment fragment)
        {
            this.fragment = fragment;
        }

        public virtual AsyncTaskLoader<LoadResult<IList<ListArticle>>> OnCreateLoader(int id, Bundle args)
        {
            return new AnonymousAsyncTaskLoader(this.fragment.GetActivity());
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(ListCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly ListCallbacks parent;
            public LoadResult<IList<ListArticle>> LoadInBackground()
            {
                try
                {
                    IList<ListArticle> articles = fragment.client.GetArticles(fragment.category, fragment.page);
                    return new LoadResult(articles);
                }
                catch (Exception e)
                {
                    L.Error("Articles page request error", e);
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<IList<ListArticle>>> loader, LoadResult<IList<ListArticle>> result)
        {
            fragment.refresh.SetRefreshing(false);
            if (result.IsError())
            {
                View.OnClickListener retryListener = (v) => fragment.LoadData();
                int itemCount = fragment.adapter.GetItemCount();
                if (itemCount == 0)
                {
                    fragment.upButton.SetVisibility(View.GONE);
                    fragment.supportView.ShowError(fragment.GetString(R.@string.article_list_network_error), retryListener);
                    return;
                }

                Snackbar.Make(fragment.container, R.@string.article_list_network_error, Snackbar.LENGTH_INDEFINITE).SetAction(R.@string.retry_button, retryListener).Show();
                return;
            }

            bool needClearData = fragment.page == 1;
            fragment.dao.SetArticles(result.GetData(), fragment.category, needClearData);
            Cursor cursor = fragment.dao.GetArticleCursor(fragment.category);
            fragment.adapter.SwapCursor(cursor);
            fragment.adapter.NotifyDataSetChanged();
            fragment.supportView.Hide();
            fragment.upButton.SetVisibility(View.VISIBLE);
            fragment.page++;
        }

        public virtual void OnLoaderReset(Loader<LoadResult<IList<ListArticle>>> loader)
        {
        }
    }
}