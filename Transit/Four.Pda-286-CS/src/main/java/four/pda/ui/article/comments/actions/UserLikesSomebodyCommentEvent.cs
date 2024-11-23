using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Actions
{
    /// <summary>
    /// Событие генерируется когда пользователь лайкнул комментарий к статье.
    /// </summary>
    public class UserLikesSomebodyCommentEvent
    {
        private readonly long commentId;
        private readonly int likesCount;
        public UserLikesSomebodyCommentEvent(long commentId, int likesCount)
        {
            this.commentId = commentId;
            this.likesCount = likesCount;
        }

        public virtual long GetCommentId()
        {
            return commentId;
        }

        public virtual int GetLikesCount()
        {
            return likesCount;
        }
    }
}