package four.pda.client.parsers;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.util.ArrayList;
import java.util.List;

import four.pda.client.exceptions.LoginException;

/**
 * Created by asavinova on 16/02/16.
 */
public class LoginParser {

	public long parse(String pageSource) {

		Document document = Jsoup.parse(pageSource);
		Elements elements = document.select("ul.errors-list > li");

		if (!elements.isEmpty()) {
			List<String> errors = new ArrayList<>();
			for (Element element : elements) {
				errors.add(element.text());
			}
			throw new LoginException(errors);
		}

		Element linkProfileElement = document.select("div.i-code > a").first();

		if (linkProfileElement == null) {
			throw new IllegalStateException("Can't find profile link");
		}

		String href = linkProfileElement.attr("href");
		String memberId = href.substring(href.lastIndexOf("=") + 1);
		return Long.parseLong(memberId);
	}

}
