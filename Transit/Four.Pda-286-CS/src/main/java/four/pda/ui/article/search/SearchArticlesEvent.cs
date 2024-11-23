using System.Diagnostics;

namespace Four.Pda.Ui.Article.Search
{
    /// <summary>
    /// Created by asavinova on 08/05/16.
    /// </summary>
    public class SearchArticlesEvent
    {
        private string searchCriteria;
        public SearchArticlesEvent(string searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }

        public virtual string GetSearchCriteria()
        {
            return searchCriteria;
        }
    }
}