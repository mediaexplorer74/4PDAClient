package four.pda.ui.article;

import java.util.Date;

/**
 * Created by asavinova on 05/12/15.
 */
public class ShowArticleCommentsEvent {

	private long articleId;
	private Date articleDate;

	public ShowArticleCommentsEvent(long articleId, Date articleDate) {
		this.articleId = articleId;
		this.articleDate = articleDate;
	}

	public long getArticleId() {
		return articleId;
	}

	public Date getArticleDate() {
		return articleDate;
	}

}
