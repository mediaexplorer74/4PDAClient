package four.pda.client.parsers;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.client.exceptions.ParseException;
import four.pda.client.model.Profile;

/**
 * Created by asavinova on 19/02/16.
 */
public class ProfileParser {

	private static final Logger L = LoggerFactory.getLogger(ProfileParser.class);

	public Profile parse(String pageSource) {

		Profile profile = new Profile();

		Document document = Jsoup.parse(pageSource);

		try {
			Element imgElement = document.select("div.photo > img").first();
			profile.setPhoto(imgElement.attr("src"));

			Element loginElement = document.select("div.user-box > h1").first();
			profile.setLogin(loginElement.text());

			Element infoElement = document.select("form > ul").first();

			// Удаляем блокнот со страницы профиля
			Elements liElements = infoElement.getElementsByTag("li");
			for (Element li : liElements) {
				if (!li.select(".icon-pencil").isEmpty()) {
					li.remove();
					break;
				}
			}

			profile.setInfo(infoElement.outerHtml());

		} catch (Exception e) {
			String message = "Can't parse profile page";
			L.error(message, e);
			throw new ParseException(message, e);
		}

		return profile;
	}

}
