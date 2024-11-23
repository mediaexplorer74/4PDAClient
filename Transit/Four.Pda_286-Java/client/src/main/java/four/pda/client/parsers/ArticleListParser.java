package four.pda.client.parsers;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

import four.pda.client.exceptions.ParseException;
import four.pda.client.model.ListArticle;

/**
 * Created by asavinova on 09/04/15.
 */
public class ArticleListParser extends AbstractParser {

	private static final Logger L = LoggerFactory.getLogger(ArticleListParser.class);

	public static final SimpleDateFormat PUBLISHED_DATE_FORMAT = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");

	public List<ListArticle> parse(String pageSource) {

		Document document = Jsoup.parse(pageSource);
		Elements elements = document.select("article [itemtype=http://schema.org/Article]");

		List<ListArticle> articles = new ArrayList<>();
		for (Element element : elements) {

			ListArticle article;
			try {
				article = parseListItem(element);
			} catch (Exception e) {
				String message = "Can't parse articles list";
				L.error(message, e);
				throw new ParseException(message, e);
			}

			if (article == null) {
				continue;
			}
			articles.add(article);
		}

		if (articles.isEmpty()) {
			String message = "Articles list is empty";
			L.error(message);
			throw new ParseException(message);
		}

		return articles;
	}

	private ListArticle parseListItem(Element element) {

		if (element.attr("data-ztm").isEmpty()) {
			// Не парсим рекламу
			return null;
		}

		ListArticle article = new ListArticle();

		String url = element.select("a[itemprop=url]").first().attr("href");

		if (url.contains("/special/")) {
			// Не парсим специальные ссылки
			// TODO Парсить спец. ссылки в отдельную модель
			return null;
		}

		if (url.contains("/tag/")) {
			// Не парсим статьи-тэги
			// TODO Парсить статьи-тэги отдельно
			return null;
		}

		try {
			IdAndDate idAndDate = getIdAndDateFromUrl(url);
			article.setId(idAndDate.id);
			article.setDate(idAndDate.date);
		} catch (Exception e) {
			L.error("Can't parse id and date from url [{}]", url);
			return null;
		}

		Element labelEl = element.select(".visual .label").first();
		article.setLabel(new ArticleLabelParser().parse(labelEl));

		article.setTitle(element.select(".list-post-title > a > span").text());
		article.setDescription(element.select("div[itemprop=description] > p").html());

		String imageSrc = element.select("img[itemprop=image]").first().attr("src");
		article.setImage(imageSrc);

		int commentsCount = Integer.parseInt(element.select("a.v-count").text());
		article.setCommentsCount(commentsCount);

		String publishedDate = element.select("meta[itemprop=datePublished]").first().attr("content");
		try {
			article.setPublishedDate(PUBLISHED_DATE_FORMAT.parse(publishedDate));
		} catch (java.text.ParseException e) {
			String message = "Can't parse datePublished tag content as date";
			L.error(message, e);
			throw new RuntimeException(message, e);
		}

		article.setAuthor(getUserFromLinkElement(element.select(".autor > a").first()));

		return article;
	}

}
