using Four.Pda.Client.Model;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments
{
    /// <summary>
    /// Created by asavinova on 16/03/16.
    /// </summary>
    public class UpdateCommentsEvent
    {
        private CommentsContainer container;
        public UpdateCommentsEvent(CommentsContainer container)
        {
            this.container = container;
        }

        public virtual CommentsContainer GetCommentsContainer()
        {
            return container;
        }
    }
}