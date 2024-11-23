package four.pda.ui.article.search;

/**
 * Created by asavinova on 08/05/16.
 */
public class SearchArticlesEvent {

	private String searchCriteria;

	public SearchArticlesEvent(String searchCriteria) {
		this.searchCriteria = searchCriteria;
	}

	public String getSearchCriteria() {
		return searchCriteria;
	}

}
