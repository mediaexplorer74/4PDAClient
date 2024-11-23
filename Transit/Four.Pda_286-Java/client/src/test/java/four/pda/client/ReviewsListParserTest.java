package four.pda.client;

import org.junit.Assert;
import org.junit.Test;

import java.io.IOException;
import java.util.List;

import four.pda.client.model.ListArticle;
import four.pda.client.parsers.ArticlePageParser;
import four.pda.client.parsers.ReviewListParser;

/**
 * Created by swap_i on 25/10/15.
 */
public class ReviewsListParserTest extends AbstractTest {

	private static final String REVIEW_PATH = "/reviews";

	@Test
	public void mainPage() throws IOException {
		String pageSource = getHtmlSource(REVIEW_PATH);
		List<ListArticle> articles = new ReviewListParser().parse(pageSource);
		Assert.assertTrue("Wrong reviews list size", articles.size() > 10);
	}

	@Test
	public void checkContentArticles() throws IOException {
		checkReviews(getHtmlSource(REVIEW_PATH));
		checkReviews(getHtmlSource(REVIEW_PATH + "/page/2/"));
	}

	private void checkReviews(String pageSource) throws IOException {
		List<ListArticle> articles = new ReviewListParser().parse(pageSource);
		for (ListArticle article : articles) {
			String source = getHtmlSource(getArticleUrl(article.getDate(), article.getId()));
			new ArticlePageParser().parse(source);
		}
	}
}
