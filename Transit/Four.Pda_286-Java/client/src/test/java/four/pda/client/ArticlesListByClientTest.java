package four.pda.client;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.io.IOException;

import four.pda.client.model.ListArticle;
import okhttp3.OkHttpClient;

/**
 * Created by pavel on 11/06/16.
 */
public class ArticlesListByClientTest {

	private FourPdaClient client;

	@Before
	public void prepare() {
		client = new FourPdaClient(new OkHttpClient());
	}

	@Test
	public void atLeastOneArticleHasComments() throws IOException {
		for (CategoryType categoryType : CategoryType.values()) {
			boolean atLeastOneArticleHasComments = false;
			for (ListArticle article : client.getArticles(categoryType, 1)) {
				if (article.getCommentsCount() > 0) {
					atLeastOneArticleHasComments = true;
				}
			}
			Assert.assertTrue(
					"No article with comments on first page of category " + categoryType.name(),
					atLeastOneArticleHasComments
			);
		}
	}

}
