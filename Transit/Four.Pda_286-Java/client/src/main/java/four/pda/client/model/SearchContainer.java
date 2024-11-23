package four.pda.client.model;

import java.util.List;

/**
 * Created by asavinova on 07/05/16.
 */
public class SearchContainer {

	private int allArticlesCount;
	private boolean hasNextPage;
	private List<SearchListArticle> articles;

	public int getAllArticlesCount() {
		return allArticlesCount;
	}

	public void setAllArticlesCount(int allArticlesCount) {
		this.allArticlesCount = allArticlesCount;
	}

	public boolean hasNextPage() {
		return hasNextPage;
	}

	public void setHasNextPage(boolean hasNextPage) {
		this.hasNextPage = hasNextPage;
	}

	public List<SearchListArticle> getArticles() {
		return articles;
	}

	public void setArticles(List<SearchListArticle> articles) {
		this.articles = articles;
	}

}
