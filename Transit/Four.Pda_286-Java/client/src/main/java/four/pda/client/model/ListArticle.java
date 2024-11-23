package four.pda.client.model;

import java.util.Date;

/**
 * Created by asavinova on 09/04/15.
 */
public class ListArticle extends AbstractArticle {

	private Date publishedDate;
	private int commentsCount;

	public Date getPublishedDate() {
		return publishedDate;
	}

	public void setPublishedDate(Date publishedDate) {
		this.publishedDate = publishedDate;
	}

	public int getCommentsCount() {
		return commentsCount;
	}

	public void setCommentsCount(int commentsCount) {
		this.commentsCount = commentsCount;
	}

}
