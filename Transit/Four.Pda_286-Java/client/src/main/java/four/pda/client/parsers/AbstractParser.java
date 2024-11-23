package four.pda.client.parsers;

import org.jsoup.nodes.Element;

import java.util.Calendar;
import java.util.Date;

import four.pda.client.model.User;

/**
 * Created by swap_i on 25/10/15.
 */
public class AbstractParser {

	protected IdAndDate getIdAndDateFromUrl(String url) {

		url = url.replace("http://4pda.ru", "");

		String[] urlParts = url.split("/");

		IdAndDate idAndDate = new IdAndDate();

		idAndDate.id = Long.parseLong(urlParts[4]);

		Calendar calendar = Calendar.getInstance();
		int year = Integer.parseInt(urlParts[1]);
		int month = Integer.parseInt(urlParts[2]) - 1;
		int date = Integer.parseInt(urlParts[3]);
		calendar.set(year, month, date, 0, 0, 0);
		idAndDate.date = calendar.getTime();

		return idAndDate;
	}

	protected User getUserFromLinkElement(Element userElement) {
		User user = new User();

		String authorHref = userElement.attr("href");
		user.setId(Long.parseLong(authorHref.split("=")[1]));
		user.setNickname(userElement.text());

		return user;
	}

	class IdAndDate {
		long id;
		Date date;
	}

}
