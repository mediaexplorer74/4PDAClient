package four.pda.client;

import org.junit.Assert;
import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.IOException;
import java.util.Calendar;
import java.util.Date;

import four.pda.client.model.ListArticle;
import four.pda.client.parsers.ArticleListParser;

/**
 * Created by asavinova on 19/01/16.
 */
public class DatePublishedParserTest extends AbstractTest {

	private static final Logger L = LoggerFactory.getLogger(DatePublishedParserTest.class);

	@Test
	public void fivePages() throws IOException {

		Calendar calendar = Calendar.getInstance();
		calendar.add(Calendar.YEAR, 1);
		Date newest = calendar.getTime();

		Date oldest;

		for (int page = 1; page <= 5; page++) {

			String pageSource = getHtmlSource("/page/" + page);

			for (ListArticle article : new ArticleListParser().parse(pageSource)) {

				L.trace("Check article [{}:{}] {}", page, article.getId(), article.getTitle());

				oldest = article.getPublishedDate();

				if (!oldest.after(newest)) {
					L.trace(String.format("Newest date %s after oldest date %s", newest, oldest));
				} else {
					L.error(String.format("Newest date %s cant be before oldest date %s", newest, oldest));
				}

				Assert.assertFalse("Newest date cant be before oldest date", oldest.after(newest));

				newest = oldest;
			}
		}

	}

}
