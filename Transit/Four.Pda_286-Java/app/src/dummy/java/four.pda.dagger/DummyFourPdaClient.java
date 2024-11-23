package four.pda.dagger;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import four.pda.client.CategoryType;
import four.pda.client.FourPdaClient;
import four.pda.client.LoginParams;
import four.pda.client.exceptions.ParseException;
import four.pda.client.model.AbstractComment;
import four.pda.client.model.ArticleContent;
import four.pda.client.model.Comment;
import four.pda.client.model.CommentsContainer;
import four.pda.client.model.DeletedComment;
import four.pda.client.model.ListArticle;
import four.pda.client.model.Profile;
import four.pda.client.model.SearchContainer;
import four.pda.client.model.SearchListArticle;
import four.pda.client.model.User;
import okhttp3.OkHttpClient;

/**
 * Created by asavinova on 23/02/16.
 */
public class DummyFourPdaClient extends FourPdaClient {

	private static List<ListArticle> articles = new ArrayList<>();
	private static List<AbstractComment> comments = new ArrayList<>();

	static {
		addArticle(1, "Ошибка парсинга", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
		addArticle(2, "Ошибка сети", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
		addArticle(3, "Ошибка парсинга комментариев", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
		addArticle(4, "Ошибка сети при запросе комментариев", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
		// http://4pda.ru/2016/03/26/286724/
		addArticle(286724, "Honor 7 получает обновление до Android Marshmallow", "http://s.4pda.to/2RBAcCaBdc1x4jkcsl7mWqZlpBepS7dfWN5z0.jpg");
		// http://4pda.ru/2016/03/25/286355/
		addArticle(286355, "Microsoft работает над универсальным приложением Skype", "http://s.4pda.to/2RBAdJQfItAff0ZxZkac7eLOd9jwhpr4Dw8W.jpg");
		// http://4pda.ru/2016/03/25/286425/
		addArticle(286425, "Выиграй 1 ТБ и антивирус на все гаджеты в конкурсе от Dr. Web", "http://s.4pda.to/2RBAdFIaF2xopvCJdarD5jSlphuRODs2224z1.jpg");

		addComment("Ortisiz", 0, "Ну вот как раз геймпад и куплю)", 1, Comment.CanLike.ALREADY_LIKED);
		addDeletedComment(0);
		addComment("ibis_87", 0, "Не то чтобы я считал, что игра никакая, она, судя по всему, хорошая, но зачем такая сложность искусственная нужна, решительно не понимаю.", 0, Comment.CanLike.CAN);
		addComment("Dr_Destroi", 1, "ibis_87, \nЗдесь весь кайф в том что ты моешь пройти игру не совершенствуя своего персонажа а совершенствуя свои знания о тактике, ловушек и мув сете врагов! Это уже доказано игроками которые прошли игры серии Souls без прокачки своих персов!", 12, Comment.CanLike.CAN);
		addComment("ibis_87", 2, "Dr_Destroi, \nЭто я понимаю, потому и говорю, что игра, судя по всему, хорошая. Я только не вижу в этом никакого интереса. Вот приходите вы домой после работы, устали, поели, поговорили с женой, потаскали на руках ребенка. И садитесь часик...поумирать? Позапоминать каждый поворот каждого подземелья? Я понимаю, в чем концепция игры, но непонимаю, как это может приносить удовольствия тем, у кого нет вагона времени играть в неуставшем состоянии.", 123, Comment.CanLike.CAN);
		addComment("tapdroid", 3, "ibis_87, удовольствие? я бы сказал - играть интересно.", 0, Comment.CanLike.CAN);
		addComment("ibis_87", 4, "Вот я и не понимаю удовольствия в этом. Если у меня после 10 часов на работе будет гореть стул, то еще до победы над боссом геймпад окажется в телевизоре, а диск - на барахолке. ", 0, Comment.CanLike.CAN);
		addDeletedComment(5);
		addComment("Very long nick name that I can imagine in the world", 6, "tapdroid, \nА зачем еще играть?", 0, Comment.CanLike.CAN);
		addComment("Dr_Destroi", 7, "Тогда игра станет такой же как и все а не одной из миллиона!", 0, Comment.CanLike.CAN);
		addComment("ibis_87", 8, "Dr_Destroi, \nИз-за того, что к тому, чем она УЖЕ является, не отрезая НИЧЕГО, добавят что-то еще?", 0, Comment.CanLike.CAN);
		addDeletedComment(0);
		addDeletedComment(0);
	}

	private static void addArticle(int id, String title, String image) {
		ListArticle article = new ListArticle();
		article.setId(id);
		article.setTitle(title);
		article.setDescription(title);
		article.setImage(image);
		article.setDate(new Date());
		article.setPublishedDate(new Date());
		article.setCommentsCount(comments.size());
		article.setAuthor(getUser("Test"));
		articles.add(article);
	}

	private static void addComment(String nick, int level, String content, int likesCount, Comment.CanLike canLike) {
		Comment comment = new Comment();
		comment.setId(newId());
		comment.setDate(new Date());
		comment.setUser(getUser(nick));
		comment.setLevel(level);
		comment.setContent(content);
		comment.setCanReply(level < 8);

		Comment.Karma karma = new Comment.Karma();
		karma.setLikesCount(likesCount);
		karma.setCanLike(canLike);
		karma.setUnknown2(false);
		karma.setUnknown3(0);
		comment.setKarma(karma);

		comments.add(comment);
	}

	private static User getUser(String nick) {
		User user = new User();
		user.setId(1);
		user.setNickname(nick);
		return user;
	}

	private static void addDeletedComment(int level) {
		DeletedComment comment = new DeletedComment();
		comment.setId(newId());
		comment.setLevel(level);
		comment.setContent("Комментарий удален");
		comment.setCanReply(false);
		comments.add(comment);
	}

	public DummyFourPdaClient(OkHttpClient client) {
		super(client);
	}

	private static long newId() {
		return (long) (Math.random() * Long.MAX_VALUE);
	}

	@Override
	public List<ListArticle> getArticles(CategoryType type, int page) throws IOException {
		return articles;
	}

	@Override
	public ArticleContent getArticleContent(Date date, long id) throws IOException {
		if (id == 1) {
			throw new ParseException("");
		}

		if (id == 2) {
			throw new IOException();
		}

		for (ListArticle article : articles) {
			if (article.getId() == id) {
				ArticleContent articleContent = new ArticleContent();
				articleContent.setContent(article.getDescription());
				articleContent.setImages(new ArrayList<String>());
				return articleContent;
			}
		}

		return null;
	}

	@Override
	public CommentsContainer getArticleComments(Date date, Long id) throws IOException {
		if (id == 3) {
			throw new ParseException("");
		}

		if (id == 4) {
			throw new IOException();
		}

		CommentsContainer container = new CommentsContainer();
		container.setComments(comments);
		container.setCanAddNewComment(true);

		return container;
	}

	@Override
	public CommentsContainer addComment(long postId, Long replyId, String message) {
		CommentsContainer container = new CommentsContainer();
		container.setCanAddNewComment(true);

		Comment comment = new Comment();
		comment.setId(System.currentTimeMillis());
		comment.setDate(new Date());
		comment.setUser(getUser("You"));
		comment.setContent(message);

		if (replyId == null) {
			comment.setLevel(0);
			comments.add(comment);
			container.setComments(comments);
			return container;
		}

		List<AbstractComment> updatedComments = new ArrayList<>();
		for (AbstractComment cmnt : comments) {
			updatedComments.add(cmnt);

			if (cmnt.getId() == replyId) {
				comment.setLevel(cmnt.getLevel() + 1);
				updatedComments.add(comment);
			}
		}
		comments = updatedComments;
		container.setComments(comments);
		return container;
	}

	@Override
	public long login(LoginParams params) throws IOException {
		return 4975039l;
	}

	@Override
	public boolean logout() throws IOException {
		return true;
	}

	@Override
	public Profile getProfile(long id) throws IOException {
		Profile profile = new Profile();
		profile.setLogin("var.ann");
		profile.setPhoto("http://s.4pda.to/tp6nuQlKPdPSv8fwz1HfNVeHMOUxPbaFg.jpg");
		profile.setInfo("User info");
		return profile;
	}

	@Override
	public SearchContainer searchArticles(String search, int page) throws IOException {
		SearchContainer container = new SearchContainer();

		container.setAllArticlesCount(articles.size());
		container.setHasNextPage(false);

		List<SearchListArticle> searchArticles = new ArrayList<>();
		double position = 0;
		for (ListArticle article : articles) {
			SearchListArticle searchArticle = new SearchListArticle();
			searchArticle.setId(article.getId());
			searchArticle.setDate(article.getDate());
			searchArticle.setTitle(article.getTitle());
			searchArticle.setDescription(article.getDescription());
			searchArticle.setImage(article.getImage());
			searchArticle.setPosition(position);
			searchArticle.setAuthor(article.getAuthor());
			searchArticles.add(searchArticle);
			position++;
		}

		container.setArticles(searchArticles);

		return container;
	}

	@Override
	public void likeArticleComment(long articleId, long commentId) throws IOException {
		for (AbstractComment comment : comments) {
			if (comment.getId() == commentId) {
				Comment.Karma karma = ((Comment) comment).getKarma();
				karma.setLikesCount(karma.getLikesCount() + 1);
				karma.setCanLike(Comment.CanLike.ALREADY_LIKED);
				((Comment) comment).setKarma(karma);
				return;
			}
		}
	}

}
