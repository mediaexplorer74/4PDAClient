package four.pda.client;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;

import java.io.IOException;
import java.util.Arrays;
import java.util.Collection;

import four.pda.client.model.ArticleContent;
import four.pda.client.parsers.ArticlePageParser;

/**
 * Test that checks comments count inside article page.
 *
 * @author Pavel Savinov (swapii@gmail.com)
 */
@RunWith(Parameterized.class)
public class ArticleCommentsCountTest extends AbstractTest {

	private final String articleUrl;
	private final int expectedCommentsCount;

	@Parameterized.Parameters
	public static Collection<Object[]> data() {
		return Arrays.asList(new Object[][]{
				{"/2016/08/02/314873/", 390},
				{"/2016/08/05/315605/", 216},
				{"/2016/08/07/315763/", 113},
				{"/2016/08/07/315800/", 95},
				{"/2016/08/05/315459/", 25},
				{"/2016/07/29/313980/", 12},
				{"/2016/07/29/313895/", 7},
				{"/2016/08/02/314727/", 4},
		});
	}

	public ArticleCommentsCountTest(String articleUrl, int expectedCommentsCount) {
		this.articleUrl = articleUrl;
		this.expectedCommentsCount = expectedCommentsCount;
	}

	@Test
	public void checkCommentsCount() throws IOException {
		String page = articleUrl;
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertEquals("Unexpected comments count", expectedCommentsCount, article.getCommentsCount());
	}

}
