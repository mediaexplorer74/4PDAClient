package four.pda.client.parsers;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.client.exceptions.ParseException;
import four.pda.client.model.Captcha;

/**
 * Created by asavinova on 08/02/16.
 */
public class CaptchaParser {

	private static final Logger L = LoggerFactory.getLogger(CaptchaParser.class);

	public Captcha parse(String pageSource) {

		Captcha captcha = new Captcha();

		Document document = Jsoup.parse(pageSource);
		Element divElement = document.select("div.captcha").first();

		try {
			Element time = divElement.select("input[name=captcha-time]").first();
			captcha.setTime(time.attr("value"));

			Element sig = divElement.select("input[name=captcha-sig]").first();
			captcha.setSig(sig.attr("value"));

			Element img = divElement.select("img").first();
			captcha.setUrl(img.attr("src"));
		} catch (Exception e) {
			String message = "Can't parse captcha";
			L.error(message, e);
			throw new ParseException(message, e);
		}

		return captcha;
	}

}
