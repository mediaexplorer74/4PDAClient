package four.pda.client;

import org.junit.Assert;
import org.junit.Test;

import java.io.IOException;

import four.pda.client.model.ArticleContent;
import four.pda.client.parsers.ArticlePageParser;

/**
 * Created by asavinova on 01/11/15.
 */
public class ArticleContentParserTest extends AbstractTest {

	@Test
	public void simplePage() throws IOException {
		String mainPage = "/2015/11/01/254823/";
		String pageSource = getHtmlSource(mainPage);
		new ArticlePageParser().parse(pageSource);
	}

	@Test
	public void pageWithVideo() throws IOException {
		String page = "/2015/11/01/254700/";
		String pageSource = getHtmlSource(page);
		new ArticlePageParser().parse(pageSource);
	}

	@Test
	public void reviewPage() throws IOException {
		String page = "/2015/10/31/254594/";
		String pageSource = getHtmlSource(page);
		new ArticlePageParser().parse(pageSource);
	}

	@Test
	public void checkPageWithImages() throws IOException {
		String page = "/2016/08/31/320881/";
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertTrue("Unexpected images list", article.getImages().size() == 21);
	}

	@Test
	public void checkPageWithGalleryImages() throws IOException {
		String page = "/2016/09/01/320964/";
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertTrue("Unexpected images list", article.getImages().size() == 5);
	}

	@Test
	public void checkPageWithScreenshots() throws IOException {
		String page = "/2016/09/01/321053/";
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertTrue("Unexpected images list", article.getImages().size() == 24);
	}

	@Test
	public void checkPageWithoutImages() throws IOException {
		String page = "/2016/08/31/320758/";
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertTrue("Images list is not empty!", article.getImages().isEmpty());
	}

	@Test
	public void checkPageWithImagesWithoutLinks() throws IOException {
		String page = "/2016/09/01/321164/";
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertTrue("Unexpected images list", article.getImages().size() == 5);
	}

	@Test
	public void checkPageWithOnlyVideo() throws IOException {
		String page = "/2016/09/03/321807/";
		String pageSource = getHtmlSource(page);
		ArticleContent article = new ArticlePageParser().parse(pageSource);
		Assert.assertTrue("Images list is not empty!", article.getImages().isEmpty());
	}

}
