using Four.Pda.Client.Model;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments
{
    /// <summary>
    /// Created by asavinova on 25/04/16.
    /// </summary>
    public class CommentActionsEvent
    {
        private Comment comment;
        public CommentActionsEvent(Comment comment)
        {
            this.comment = comment;
        }

        public virtual Comment GetComment()
        {
            return comment;
        }
    }
}