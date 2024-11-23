package four.pda.client;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.io.IOException;

import four.pda.client.model.SearchContainer;
import okhttp3.OkHttpClient;

/**
 * Created by pavel on 11/06/16.
 */
public class SearchArticlesByClientTest {

	private FourPdaClient client;

	@Before
	public void prepare() {
		client = new FourPdaClient(new OkHttpClient());
	}

	@Test
	public void searchOneWord() throws IOException {
		SearchContainer container = client.searchArticles("android", 1);
		Assert.assertTrue("Has not next page", container.hasNextPage());
		Assert.assertTrue("All articles count is 0", container.getAllArticlesCount() > 0);
		Assert.assertFalse("This page articles count is 0", container.getArticles().isEmpty());
	}

	@Test
	public void searchCyrillic() throws IOException {
		SearchContainer container = client.searchArticles("телефон", 1);
		Assert.assertTrue("Has not next page", container.hasNextPage());
		Assert.assertTrue("All articles count is 0", container.getAllArticlesCount() > 0);
		Assert.assertFalse("This page articles count is 0", container.getArticles().isEmpty());
	}

}
