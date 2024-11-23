package four.pda.client.parsers;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.ArrayList;
import java.util.List;

import four.pda.client.exceptions.ParseException;
import four.pda.client.model.ListArticle;

/**
 * Created by asavinova on 13/04/15.
 */
public class ReviewListParser extends AbstractParser {

	private static final Logger L = LoggerFactory.getLogger(ReviewListParser.class);

	public List<ListArticle> parse(String pageSource) {

		Document document = Jsoup.parse(pageSource);
		Elements elements = document.select("li[itemtype=http://schema.org/CreativeWork][itemscope]");

		List<ListArticle> articles = new ArrayList<>();
		for (Element element : elements) {
			ListArticle article;
			try {
				article = articleFromElement(element);
			} catch (Exception e) {
				String message = "Can't parse reviews list";
				L.error(message, e);
				throw new ParseException(message, e);
			}
			if (article == null) {
				continue;
			}
			articles.add(article);
		}

		if (articles.isEmpty()) {
			String message = "Review list is empty";
			L.error(message);
			throw new ParseException(message);
		}

		return articles;
	}

	private ListArticle articleFromElement(Element element) {

		ListArticle article = new ListArticle();

		String url = element.select("a[itemprop=url]").attr("href");

		try {
			IdAndDate idAndDate = getIdAndDateFromUrl(url);
			article.setId(idAndDate.id);
			article.setDate(idAndDate.date);
		} catch (Exception e) {
			L.error("Can't parse id and date from url [{}]", url);
			return null;
		}

		String title = element.select("span[itemprop=name]").get(0).text();
		article.setTitle(title);

		Element content = element.select("div.content").first();
		content.select("h1").remove();
		String description = content.html();
		article.setDescription(description);

		String image = element.select("img[itemprop=image]").attr("src");
		article.setImage(image);

		int commentsCount = Integer.parseInt(element.select("a.v-count").text());
		article.setCommentsCount(commentsCount);

		String publishedDate = element.select("meta[itemprop=datePublished]").first().attr("content");
		try {
			article.setPublishedDate(ArticleListParser.PUBLISHED_DATE_FORMAT.parse(publishedDate));
		} catch (java.text.ParseException e) {
			String message = "Can't parse datePublished tag content as date";
			L.error(message, e);
			throw new RuntimeException(message, e);
		}

		return article;
	}

}
