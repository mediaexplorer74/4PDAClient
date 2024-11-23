package four.pda.client;

import org.apache.commons.lang3.StringUtils;
import org.junit.Assert;
import org.junit.Test;

import java.io.IOException;
import java.net.URLEncoder;
import java.util.List;

import four.pda.client.model.AbstractArticle;
import four.pda.client.model.ArticleContent;
import four.pda.client.model.ListArticle;
import four.pda.client.model.SearchContainer;
import four.pda.client.parsers.ArticleListParser;
import four.pda.client.parsers.ArticlePageParser;
import four.pda.client.parsers.ReviewListParser;
import four.pda.client.parsers.SearchArticlesParser;

/**
 * @author Pavel Savinov (swapii@gmail.com)
 */
public class ArticleLabelTest extends AbstractTest {

	@Test
	public void labelsParsedFromDiscountList() throws IOException {

		String source = getHtmlSource("/tag/discount/");

		List<ListArticle> articles = new ArticleListParser().parse(source);
		for (ListArticle article : articles) {
			AbstractArticle.Label label = article.getLabel();
			Assert.assertNotNull(label);
			Assert.assertTrue(StringUtils.isNotBlank(label.getName()));
		}

	}

	@Test
	public void labelsParsedFromReviewPage() throws IOException {

		List<ListArticle> articles = new ReviewListParser().parse(getHtmlSource("/reviews/"));
		ListArticle article = articles.get(0);
		String articleUrl = getArticleUrl(article.getDate(), article.getId());
		ArticleContent content = new ArticlePageParser().parse(getHtmlSource(articleUrl));

		AbstractArticle.Label label = content.getLabel();
		Assert.assertNotNull(label);
		Assert.assertTrue(StringUtils.isNotBlank(label.getName()));
	}

	@Test
	public void labelPresentOnSearchPage() throws IOException {

		String query = "SuperSale: удобный поиск распродаж и скидок";
		String encodedQuery = URLEncoder.encode(query, "CP1251");
		SearchContainer container = new SearchArticlesParser().parse(getHtmlSource("/?s=" + encodedQuery), 0);

		Assert.assertEquals(1, container.getAllArticlesCount());

		AbstractArticle.Label label = container.getArticles().get(0).getLabel();
		Assert.assertEquals("Проект поддержки разработчиков", label.getName());
		Assert.assertEquals("orange", label.getColor());
	}

	@Test
	public void labelPresentOnArticlePage() throws IOException {

		// SuperSale: удобный поиск распродаж и скидок
		String source = getHtmlSource("/2016/11/25/330215/");
		ArticleContent content = new ArticlePageParser().parse(source);

		AbstractArticle.Label label = content.getLabel();
		Assert.assertEquals("Проект поддержки разработчиков", label.getName());
		Assert.assertEquals("orange", label.getColor());
	}

	@Test
	public void labelNotPresentOnRegularPage() throws IOException {

		// Главная распродажа осени: гид по скидкам на игры для iOS и Android
		String source = getHtmlSource("/2016/11/27/330346/");
		ArticleContent content = new ArticlePageParser().parse(source);

		AbstractArticle.Label label = content.getLabel();
		Assert.assertNull(label);
	}

}
