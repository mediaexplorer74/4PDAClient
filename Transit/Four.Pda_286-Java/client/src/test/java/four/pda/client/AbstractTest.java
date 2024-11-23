package four.pda.client;

import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by swap_i on 25/10/15.
 */
public abstract class AbstractTest {

	protected static final String BASE_URL = "http://4pda.ru";
	private static final SimpleDateFormat ARTICLE_DATE_FORMAT = new SimpleDateFormat("yyyy/MM/dd");

	protected String getHtmlSource(String urlPath) throws IOException {
		String url = BASE_URL + urlPath;
		char delimiter = url.contains("?") ? '&' : '?';
		return IOUtils.toString(new URL(url + delimiter + Math.random()), "cp1251");
	}

	protected String getArticleUrl(Date date, long id) {
		return "/" + ARTICLE_DATE_FORMAT.format(date) + "/" + id;
	}

}
