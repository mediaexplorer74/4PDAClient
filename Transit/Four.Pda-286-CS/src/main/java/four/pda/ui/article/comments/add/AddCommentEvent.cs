using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Add
{
    /// <summary>
    /// Created by asavinova on 11/03/16.
    /// </summary>
    public class AddCommentEvent
    {
        private long replyId;
        private string replyAuthor;
        public AddCommentEvent()
        {
        }

        public AddCommentEvent(long replyId, string replyAuthor)
        {
            this.replyId = replyId;
            this.replyAuthor = replyAuthor;
        }

        public virtual long GetReplyId()
        {
            return replyId;
        }

        public virtual string GetReplyAuthor()
        {
            return replyAuthor;
        }
    }
}