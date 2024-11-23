package four.pda.templates.exporter;

import four.pda.client.parsers.ArticlePageParser;
import four.pda.template.NewsArticleTemplate;

/**
 * Created by pavel on 26/05/16.
 */
public class ArticleExporter extends Exporter {

	public static void main(String[] args) {
		ArticleExporter exporter = new ArticleExporter();
		exporter.convertPage("2016/05/26/300085", "news_text");
		exporter.convertPage("2016/04/27/294201", "review");
		exporter.convertPage("2016/09/01/321164", "images_without_links");
		exporter.convertPage("2016/09/02/321525", "screenshots");
	}

	@Override
	protected String getCroppedPage(String originalPage) {
		return new ArticlePageParser().parse(originalPage).getContent();
	}

	@Override
	protected String getWrappedPage(String croppedPage) {
		return new NewsArticleTemplate().make(croppedPage);
	}

}
