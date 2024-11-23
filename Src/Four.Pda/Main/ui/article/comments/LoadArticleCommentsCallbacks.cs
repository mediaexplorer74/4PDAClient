using Android.Os;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments
{
    /// <summary>
    /// Created by asavinova on 11/03/16.
    /// </summary>
    class LoadArticleCommentsCallbacks : LoaderCallbacks<LoadResult<CommentsContainer>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(LoadArticleCommentsCallbacks));
        private CommentsFragment fragment;
        public LoadArticleCommentsCallbacks(CommentsFragment fragment)
        {
            this.fragment = fragment;
        }

        public virtual Loader<LoadResult<CommentsContainer>> OnCreateLoader(int id, Bundle args)
        {
            return new AnonymousAsyncTaskLoader(fragment.GetActivity());
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(LoadArticleCommentsCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly LoadArticleCommentsCallbacks parent;
            public LoadResult<CommentsContainer> LoadInBackground()
            {
                try
                {
                    return new LoadResult(fragment.client.GetArticleComments(fragment.articleDate, fragment.articleId));
                }
                catch (Exception e)
                {
                    L.Error("Article comments request error", e);
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<CommentsContainer>> loader, LoadResult<CommentsContainer> result)
        {
            fragment.refresh.SetRefreshing(false);
            if (result.GetException() == null)
            {
                fragment.adapter.SetCommentsContainer(result.GetData());
                fragment.adapter.NotifyDataSetChanged();
                fragment.supportView.Hide();
                return;
            }

            fragment.supportView.ShowError(fragment.GetString(R.@string.comments_network_error), (v) => fragment.LoadData());
        }

        public virtual void OnLoaderReset(Loader<LoadResult<CommentsContainer>> loader)
        {
        }
    }
}