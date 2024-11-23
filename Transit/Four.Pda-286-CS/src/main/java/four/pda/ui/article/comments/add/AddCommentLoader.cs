using Androidx.Loader.Content;
using Org.Slf4j;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Add
{
    /// <summary>
    /// Created by asavinova on 01/09/16.
    /// </summary>
    public class AddCommentLoader : AsyncTaskLoader<LoadResult<CommentsContainer>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(AddCommentLoader));
        private AddCommentDialog fragment;
        public AddCommentLoader(AddCommentDialog fragment) : base(fragment.GetActivity())
        {
            this.fragment = fragment;
        }

        public override LoadResult<CommentsContainer> LoadInBackground()
        {
            try
            {
                string message = fragment.messageEditText.GetText().ToString();
                CommentsContainer container = fragment.client.AddComment(fragment.postId, fragment.replyId, message);
                return new LoadResult(container);
            }
            catch (Exception e)
            {
                L.Error("Add comment request error", e);
                return new LoadResult(e);
            }
        }
    }
}