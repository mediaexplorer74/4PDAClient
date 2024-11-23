using Android.Content;
using Android.Os;
using Android.View;
using Android.Widget;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Actions
{
    /// <summary>
    /// Created by pavel on 07/06/16.
    /// </summary>
    class LikeCommentLoaderCallbacks : LoaderCallbacks<LoadResult<Void>>
    {
        private CommentActionsDialog dialog;
        private Context context;
        private FourPdaClient client;
        public LikeCommentLoaderCallbacks(CommentActionsDialog dialog, Context context, FourPdaClient client)
        {
            this.dialog = dialog;
            this.context = context;
            this.client = client;
        }

        public virtual Loader<LoadResult<Void>> OnCreateLoader(int id, Bundle args)
        {
            return new AnonymousAsyncTaskLoader(context);
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(LikeCommentLoaderCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly LikeCommentLoaderCallbacks parent;
            public LoadResult<Void> LoadInBackground()
            {
                try
                {
                    client.LikeArticleComment(dialog.@params.ArticleId(), dialog.@params.Id());
                    return new LoadResult((Void)null);
                }
                catch (Exception e)
                {
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<Void>> loader, LoadResult<Void> result)
        {
            dialog.likeButton.SetVisibility(View.VISIBLE);
            dialog.likeProgressView.SetVisibility(View.INVISIBLE);
            if (result.IsError())
            {
                Toast.MakeText(context, R.@string.article_comment_like_error, Toast.LENGTH_SHORT).Show();
                return;
            }

            dialog.@params = new AutoValue_DialogParams(dialog.@params.Id(), dialog.@params.AuthorId(), dialog.@params.AuthorName(), dialog.@params.Date(), Comment.CanLike.ALREADY_LIKED, dialog.@params.LikeCount() + 1, dialog.@params.Content(), dialog.@params.CanReply(), dialog.@params.ArticleId(), dialog.@params.ArticleDate());
            dialog.UpdateLikes();
            dialog.eventBus.Post(new UserLikesSomebodyCommentEvent(dialog.@params.Id(), dialog.@params.LikeCount()));
        }

        public virtual void OnLoaderReset(Loader<LoadResult<Void>> loader)
        {
        }
    }
}