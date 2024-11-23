package four.pda.client;

import org.junit.Assert;
import org.junit.Test;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.List;

import four.pda.client.model.AbstractComment;
import four.pda.client.model.Comment;
import four.pda.client.model.CommentsContainer;
import four.pda.client.model.DeletedComment;
import four.pda.client.model.ListArticle;
import four.pda.client.parsers.ArticleListParser;
import four.pda.client.parsers.CommentTreeParser;

/**
 * Created by asavinova on 02/11/15.
 */
public class CommentTreeParserTest extends AbstractTest {

	private static final SimpleDateFormat DATE_FORMAT = new SimpleDateFormat("dd.MM.yy HH:ss");

	@Test
	public void firstComment() throws IOException {
		String pageSource = getHtmlSource("/2014/10/27/182819/");
		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		List<AbstractComment> comments = container.getComments();

		Assert.assertNotNull(comments);
		Assert.assertTrue(comments.size() > 0);

		Comment firstComment = (Comment) comments.get(0);
		Assert.assertEquals("Unexpected comment id", 1945077, firstComment.getId());
		Assert.assertEquals("Unexpected comment user id", 1857314, firstComment.getUser().getId());
		Assert.assertEquals("Unexpected comment user nickname", "Qwertymisha", firstComment.getUser().getNickname());
		Assert.assertEquals("Unexpected comment content", "Норм", firstComment.getContent());
		Assert.assertEquals("Unexpected comment level", 0, firstComment.getLevel());
		Assert.assertEquals("Unexpected comment date", "27.10.14 13:41", DATE_FORMAT.format(firstComment.getDate()));
	}

	@Test
	public void childComments() throws IOException {
		String pageSource = getHtmlSource("/2014/10/27/182819/");
		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		List<AbstractComment> comments = container.getComments();

		Assert.assertNotNull(comments);
		Assert.assertTrue(comments.size() > 0);

		Comment secondComment = (Comment) comments.get(1);
		List<AbstractComment> childComments = secondComment.getChildren();

		Comment child1 = (Comment) childComments.get(0);
		Assert.assertEquals("Unexpected comment id", 1945383, child1.getId());
		Assert.assertEquals("Unexpected comment user id", 3804015, child1.getUser().getId());
		Assert.assertEquals("Unexpected comment user nickname", "keeapk", child1.getUser().getNickname());
		Assert.assertEquals("Unexpected comment content", "xbrat, <br>Как ей пользоваться чего то не понял ?:)", child1.getContent());
		Assert.assertEquals("Unexpected comment level", 1, child1.getLevel());
		Assert.assertEquals("Unexpected comment date", "27.10.14 15:46", DATE_FORMAT.format(child1.getDate()));
	}

	@Test
	public void deletedComments() throws IOException {
		String pageSource = getHtmlSource("/2014/10/19/181576/");
		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		List<AbstractComment> comments = container.getComments();

		Assert.assertNotNull(comments);
		Assert.assertTrue(comments.size() > 0);

		Comment firstComment = (Comment) comments.get(0);
		List<AbstractComment> childComments = firstComment.getChildren();

		Comment child1 = (Comment) childComments.get(0);
		Assert.assertEquals("Unexpected comment id", 1932557, child1.getId());
		Assert.assertEquals("Unexpected comment user id", 2262571, child1.getUser().getId());
		Assert.assertEquals("Unexpected comment user nickname", "mrPhilL", child1.getUser().getNickname());
		Assert.assertEquals("Unexpected comment content", "9900 цена. Что-то перегнули...", child1.getContent());
		Assert.assertEquals("Unexpected comment level", 1, child1.getLevel());
		Assert.assertEquals("Unexpected comment date", "20.10.14 00:55", DATE_FORMAT.format(child1.getDate()));

		Comment child2 = (Comment) childComments.get(1);
		Assert.assertEquals("Unexpected comment id", 1932736, child2.getId());
		Assert.assertEquals("Unexpected comment user id", 3856586, child2.getUser().getId());
		Assert.assertEquals("Unexpected comment user nickname", "RDash", child2.getUser().getNickname());
		Assert.assertEquals("Unexpected comment content", "Vacum, <br>Ржать?))", child2.getContent().substring(0, 19));
		Assert.assertEquals("Unexpected comment level", 1, child2.getLevel());
		Assert.assertEquals("Unexpected comment date", "20.10.14 09:29", DATE_FORMAT.format(child2.getDate()));

	}

	@Test
	public void treeDeletedComments() throws IOException {
		String pageSource = getHtmlSource("/2013/12/20/130961/");
		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		List<AbstractComment> comments = container.getComments();

		Assert.assertNotNull(comments);
		Assert.assertEquals(35, comments.size());
	}

	@Test
	public void canCommentOnFirstPage() throws IOException {
		String url = getArticleUrlFromPage(1);
		String pageSource = getHtmlSource(url);
		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		Assert.assertTrue("The first article must be able to comment", container.canAddNewComment());
	}

	@Test
	public void canCommentOnSecondPage() throws IOException {
		String url = getArticleUrlFromPage(2);
		String pageSource = getHtmlSource(url);
		CommentsContainer container = new CommentTreeParser().parse(pageSource);

		Assert.assertTrue("The article from second page must be able to comment", container.canAddNewComment());

		checkCanComment(container.getComments());
	}

	private void checkCanComment(List<AbstractComment> tree) {

		if (tree == null) return;

		for (AbstractComment comment : tree) {
			checkCanComment(comment.getChildren());

			if (comment instanceof DeletedComment) {
				Assert.assertFalse("Deleted comment should not be able to comment", comment.canReply());
				continue;
			}

			if (comment.getLevel() < 8) {
				Assert.assertTrue("Comment below 8 level should be able to comment", comment.canReply());
			} else {
				Assert.assertFalse("Comment more than 8 levels should not be able to comment", comment.canReply());
			}
		}
	}

	@Test
	public void cantCommentOnOldArticles() throws IOException {
		String url = getArticleUrlFromPage(30);
		String pageSource = getHtmlSource(url);
		CommentsContainer container = new CommentTreeParser().parse(pageSource);

		Assert.assertFalse("Older article should not be able to comment", container.canAddNewComment());

		List<AbstractComment> comments = container.getComments();
		for (AbstractComment comment : comments) {
			Assert.assertFalse("Older comments should not be able to comment", comment.canReply());
		}
	}

	@Test
	public void articleLikesParsed() throws IOException {

		String pageSource = getHtmlSource("/2013/12/20/130961/");
		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		List<AbstractComment> comments = container.getComments();

		for (AbstractComment abstractComment : comments) {

			if (!(abstractComment instanceof Comment)) {
				continue;
			}

			Comment comment = (Comment) abstractComment;

			Assert.assertNotNull("Karma of comment " + comment.getId() + " not parsed", comment.getKarma());

			expectIdAndLikes(comment, 1341794, 49);
			expectIdAndLikes(comment, 1341798, 23);
			expectIdAndLikes(comment, 1341960, 17);

		}

	}

	private void expectIdAndLikes(Comment comment, int expectedId, int expectedLikes) {
		if (comment.getId() == expectedId) {
			Assert.assertEquals("Comment likes not expected", expectedLikes, comment.getKarma().getLikesCount());
		}
	}

	@Test
	public void unknownKarmaParamsStillZero() throws IOException {

		String pageSource = getHtmlSource("/2013/12/20/130961/");
		//TODO Добавить проверку первых нескольких страниц и статей с них

		CommentsContainer container = new CommentTreeParser().parse(pageSource);
		List<AbstractComment> comments = container.getComments();

		for (AbstractComment abstractComment : comments) {

			if (!(abstractComment instanceof Comment)) {
				continue;
			}

			Comment comment = (Comment) abstractComment;

			Comment.Karma karma = comment.getKarma();
			long id = comment.getId();

			Assert.assertEquals(
					"Unknown karma param 2 of comment " + id + " is not false",
					false, karma.getUnknown2()
			);

			Assert.assertEquals(
					"Unknown karma param 3 of comment " + id + " is not zero",
					0, karma.getUnknown3()
			);

		}
	}

	private String getArticleUrlFromPage(int page) throws IOException {
		String pageSource = getHtmlSource("/page/" + page);
		List<ListArticle> articles = new ArticleListParser().parse(pageSource);
		ListArticle article = articles.get(0);
		return getArticleUrl(article.getDate(), article.getId());
	}

}
