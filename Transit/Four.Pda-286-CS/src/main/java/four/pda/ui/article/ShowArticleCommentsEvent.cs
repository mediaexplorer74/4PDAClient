using Java.Util;
using System.Diagnostics;

namespace Four.Pda.Ui.Article
{
    /// <summary>
    /// Created by asavinova on 05/12/15.
    /// </summary>
    public class ShowArticleCommentsEvent
    {
        private long articleId;
        private Date articleDate;
        public ShowArticleCommentsEvent(long articleId, Date articleDate)
        {
            this.articleId = articleId;
            this.articleDate = articleDate;
        }

        public virtual long GetArticleId()
        {
            return articleId;
        }

        public virtual Date GetArticleDate()
        {
            return articleDate;
        }
    }
}