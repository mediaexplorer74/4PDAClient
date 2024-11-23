package four.pda.ui.article.comments;

import four.pda.client.model.Comment;

/**
 * Created by asavinova on 25/04/16.
 */
public class CommentActionsEvent {

	private Comment comment;

	public CommentActionsEvent(Comment comment) {
		this.comment = comment;
	}

	public Comment getComment() {
		return comment;
	}

}
