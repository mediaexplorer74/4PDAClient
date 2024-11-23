package four.pda.ui.article.comments.add;

/**
 * Created by asavinova on 11/03/16.
 */
public class AddCommentEvent {

	private Long replyId;
	private String replyAuthor;

	public AddCommentEvent() {
	}

	public AddCommentEvent(long replyId, String replyAuthor) {
		this.replyId = replyId;
		this.replyAuthor = replyAuthor;
	}

	public Long getReplyId() {
		return replyId;
	}

	public String getReplyAuthor() {
		return replyAuthor;
	}

}
