package four.pda.analytics;

import android.content.Context;

import org.androidannotations.annotations.EBean;

import java.util.HashMap;

import four.pda.client.CategoryType;

/**
 * Analytics.
 *
 * @author Pavel Savinov.
 */
@EBean(scope = EBean.Scope.Singleton)
public class Analytics {

	private Drawer drawer = new Drawer();
	private ArticlesList articlesList = new ArticlesList();
	private Article article = new Article();
	private Comments comments = new Comments();
	private Search search = new Search();

	private final AnalyticsTracker tracker;

	public Analytics(Context context) {
		this.tracker = new AnswersProxyTracker(context);
	}

	public Drawer drawer() {
		return drawer;
	}

	public ArticlesList articlesList() {
		return articlesList;
	}

	public Article article() {
		return article;
	}

	public Comments comments() {
		return comments;
	}

	public Search search() {
		return search;
	}

	public class Drawer {

		public void aboutClicked() {
			tracker.sendCustomEvent("Drawer.About.Click");
		}

		public void categoryClicked(CategoryType type) {
			tracker.sendCustomEvent("Drawer.Category.Click",
					new HashMap<String, String>() {{
						put("type", type.name());
					}});
		}

		public void feedbackClicked() {
			tracker.sendCustomEvent("Drawer.Feedback.Click");
		}

		public void loginClicked() {
			tracker.sendCustomEvent("Drawer.Login.Click");
		}

		public void logoutClicked() {
			tracker.sendCustomEvent("Drawer.Logout.Click");
		}

		public void profileClicked() {
			tracker.sendCustomEvent("Drawer.Profile.Click");
		}

	}

	public class ArticlesList {

		public void scrollUp(int currentPosition) {
			tracker.sendCustomEvent("ArticleList.ScrollUp",
					new HashMap<String, String>() {{
						put("position", String.valueOf(currentPosition));
					}});
		}

		public void profileClicked() {
			tracker.sendCustomEvent("ArticleList.Profile.Click");
		}

	}

	public class Article {

		public void open() {
			tracker.sendCustomEvent("Article.Open");
		}

		public void profileClicked() {
			tracker.sendCustomEvent("Article.Profile.Click");
		}

		public void textZoomOpen() {
			tracker.sendCustomEvent("Article.TextZoom.Open");
		}

		public void textZoomSet(int zoom) {
			tracker.sendCustomEvent("Article.TextZoom.Set",
					new HashMap<String, String>() {{
						put("zoom", String.valueOf(zoom));
					}});
		}

		public void share() {
			tracker.sendCustomEvent("Article.Share");
		}

		public void openImageGallery() {
			tracker.sendCustomEvent("Article.ImageGallery.Open");
		}

	}

	public class Comments {

		public void open() {
			tracker.sendCustomEvent("Comments.Open");
		}

		public void add() {
			tracker.sendCustomEvent("Comments.AddButton.Click");
		}

		public void reply() {
			tracker.sendCustomEvent("Comments.ReplyButton.Click");
		}

		public void showDialog() {
			tracker.sendCustomEvent("Comments.ActionsDialog.Show");
		}

		public void profileClicked() {
			tracker.sendCustomEvent("Comments.ActionsDialog.Profile.Click");
		}

		public void like() {
			tracker.sendCustomEvent("Comments.ActionsDialog.Like");
		}

		public void share() {
			tracker.sendCustomEvent("Comments.ActionsDialog.Share");
		}

	}

	public class Search {

		public void open() {
			tracker.sendCustomEvent("Search.Open");
		}

		public void scrollUp(int currentPosition) {
			tracker.sendCustomEvent("Search.ScrollUp",
					new HashMap<String, String>() {{
						put("position", String.valueOf(currentPosition));
					}});
		}

		public void load(String searchCriteria) {
			tracker.search(searchCriteria);
		}

	}

}
