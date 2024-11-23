using Android.Os;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Four.Pda;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Add
{
    /// <summary>
    /// Created by asavinova on 16/03/16.
    /// </summary>
    public class AddCommentCallbacks : LoaderCallbacks<LoadResult<CommentsContainer>>
    {
        private AddCommentDialog fragment;
        public AddCommentCallbacks(AddCommentDialog fragment)
        {
            this.fragment = fragment;
        }

        public virtual AddCommentLoader OnCreateLoader(int id, Bundle args)
        {
            return new AddCommentLoader(fragment);
        }

        public virtual void OnLoadFinished(Loader<LoadResult<CommentsContainer>> loader, LoadResult<CommentsContainer> result)
        {
            fragment.supportView.Hide();
            if (result.GetException() != null)
            {
                fragment.supportView.ShowError(fragment.GetString(R.@string.add_comment_network_error), (v) => fragment.AddComment());
                return;
            }

            fragment.UpdateComments(result.GetData());
        }

        public virtual void OnLoaderReset(Loader<LoadResult<CommentsContainer>> loader)
        {
        }
    }
}