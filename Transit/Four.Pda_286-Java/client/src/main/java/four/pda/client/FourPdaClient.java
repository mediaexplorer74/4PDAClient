package four.pda.client;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.IOException;
import java.net.URLEncoder;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import four.pda.client.exceptions.LoginException;
import four.pda.client.model.ArticleContent;
import four.pda.client.model.Captcha;
import four.pda.client.model.CommentsContainer;
import four.pda.client.model.ListArticle;
import four.pda.client.model.Profile;
import four.pda.client.model.SearchContainer;
import four.pda.client.parsers.ArticleListParser;
import four.pda.client.parsers.ArticlePageParser;
import four.pda.client.parsers.CaptchaParser;
import four.pda.client.parsers.CommentTreeParser;
import four.pda.client.parsers.LoginParser;
import four.pda.client.parsers.ProfileParser;
import four.pda.client.parsers.ReviewListParser;
import four.pda.client.parsers.SearchArticlesParser;
import okhttp3.FormBody;
import okhttp3.HttpUrl;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import okhttp3.ResponseBody;

/**
 * Created by asavinova on 09/04/15.
 */
public class FourPdaClient {

	private static final Logger L = LoggerFactory.getLogger(FourPdaClient.class);

	public static final SimpleDateFormat ARTICLE_DATE_FORMAT = new SimpleDateFormat("yyyy/MM/dd");

	private static final String HOST = "4pda.ru";
	private static final String BASE_URL = "http://" + HOST + "/";

	private OkHttpClient client;

	public FourPdaClient(OkHttpClient client) {
		this.client = client;
	}

	public long login(LoginParams params) throws IOException {
		String url = BASE_URL + "forum/index.php?act=auth";

		String login = URLEncoder.encode(params.getLogin(), "CP1251");
		String password = URLEncoder.encode(params.getPassword(), "CP1251");
		RequestBody requestBody = new FormBody.Builder()
				.addEncoded("login", login)
				.addEncoded("password", password)
				.add("captcha-time", params.getCaptchaTime())
				.add("captcha-sig", params.getCaptchaSig())
				.add("captcha", params.getCaptcha())
				.build();

		Request request = new Request.Builder()
				.post(requestBody)
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			return new LoginParser().parse(body.string());
		} catch (LoginException e) {
			L.info("Login error", e);
			throw e;
		} catch (RuntimeException e) {
			L.error("Can't parse login result page", e);
			throw e;
		} finally {
			body.close();
		}
	}

	public boolean logout() throws IOException {
		String url = BASE_URL + "forum/index.php?act=login&CODE=03";
		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		return response.isSuccessful();
	}

	public Profile getProfile(long id) throws IOException {
		String url = BASE_URL + "forum/index.php?showuser=" + id;
		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			return new ProfileParser().parse(body.string());
		} catch (RuntimeException e) {
			L.error("Can't parse profile page", e);
			throw e;
		} finally {
			body.close();
		}
	}

	public List<ListArticle> getArticles(CategoryType type, int page) throws IOException {
		String category = "";
		if (type != CategoryType.ALL) {
			category = type.name().toLowerCase() + "/";
		}

		String url = BASE_URL + category + "page/" + page;
		url = addRandomToUrl(url);

		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			if (type == CategoryType.REVIEWS) {
                return new ReviewListParser().parse(body.string());
            } else {
                return new ArticleListParser().parse(body.string());
            }
		} catch (RuntimeException e) {
			L.error("Can't parse page at " + url);
			throw e;
		} finally {
			body.close();
		}
	}

	public String getArticleUrl(Date date, long id) {
		String fullId = ARTICLE_DATE_FORMAT.format(date) + "/" + id + "?salt=" + System.currentTimeMillis();
		return BASE_URL + fullId;
	}

	public ArticleContent getArticleContent(Date date, long id) throws IOException {
		String url = getArticleUrl(date, id);
		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			return new ArticlePageParser().parse(body.string());
		} catch (RuntimeException e) {
			L.error("Can't parse page at " + url);
			throw e;
		} finally {
			body.close();
		}
	}

	public CommentsContainer getArticleComments(Date date, Long id) throws IOException {
		String url = getArticleUrl(date, id);
		url = addRandomToUrl(url);
		
		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			return new CommentTreeParser().parse(body.string());
		} catch (RuntimeException e) {
			L.error(String.format("Can't parse comments at %s",  url), e);
			throw e;
		} finally {
			body.close();
		}
	}

	public void likeArticleComment(long articleId, long commentId) throws IOException {

		String url = BASE_URL + "wp-content/plugins/karma/ajax.php?" +
				"p=" + articleId + "&c=" + commentId + "&v=1";

		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();

		if (!response.isSuccessful()) {
			throw new IllegalStateException("Comment like was not success");
		}

	}

	public Captcha getCaptcha() throws IOException {
		String url = BASE_URL + "forum/index.php?act=login";
		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			return new CaptchaParser().parse(body.string());
		} catch (RuntimeException e) {
			L.error("Can't parse login page", e);
			throw e;
		} finally {
			body.close();
		}
	}

	public CommentsContainer addComment(long postId, Long replyId, String message) throws IOException {
		String url = BASE_URL + "wp-comments-post.php";

		RequestBody requestBody = new FormBody.Builder()
				.add("comment_post_ID", String.valueOf(postId))
				.add("comment_reply_ID", String.valueOf(replyId == null ? 0 : replyId))
				.add("comment_reply_dp", String.valueOf(replyId == null ? 0 : 1))
				.add("comment", message)
				.build();

		Request request = new Request.Builder()
				.post(requestBody)
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		ResponseBody body = response.body();

		try {
			return new CommentTreeParser().parse(body.string());
		} catch (RuntimeException e) {
			L.error(String.format("Can't parse comments at %s", url), e);
			throw e;
		} finally {
			body.close();
		}
	}

	public String getCommentUrl(long articleId, Date articleDate, long commentId) {
		return getArticleUrl(articleDate, articleId) + "/#comment" + commentId;
	}

	/**
	 * Ищет новости по заданной строке и странице.
	 *
	 * @param searchCriteria строка поиска
	 * @param page номер страницы выдачи начиная с 1
	 * @return контейнер с результатами поиска
	 * @throws IOException
     */
	public SearchContainer searchArticles(String searchCriteria, int page) throws IOException {

		if (StringUtils.isBlank(searchCriteria)) {
			throw new IllegalArgumentException("Search criteria can't be empty");
		}

		if (page < 1) {
			throw new IllegalArgumentException("Page can't be less than 1");
		}

		HttpUrl url = new HttpUrl.Builder()
				.scheme("http")
				.host(HOST)
				.addPathSegment("page")
				.addPathSegment(String.valueOf(page))
				.addEncodedQueryParameter("s", URLEncoder.encode(searchCriteria, "CP1251"))
				.addQueryParameter("random", String.valueOf(Math.random()))
				.build();

		if (L.isTraceEnabled()) {
			L.trace("URL: " + url.toString());
		}

		Request request = new Request.Builder()
				.url(url)
				.build();

		Response response = client.newCall(request).execute();
		String body = response.body().string();

		try {
			return new SearchArticlesParser().parse(body, page);
		} catch (RuntimeException e) {
			L.error(String.format("Can't parse search page at %s", url), e);
			throw e;
		}
	}

	private String addRandomToUrl(String url) {
		return url + "?" + Math.random();
	}

}
