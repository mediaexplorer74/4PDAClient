package four.pda.client.model;

import java.util.List;

/**
 * Created by asavinova on 05/04/16.
 */
public class CommentsContainer {

	private boolean canAddNewComment;
	private List<AbstractComment> comments;

	public boolean canAddNewComment() {
		return canAddNewComment;
	}

	public void setCanAddNewComment(boolean canAddNewComment) {
		this.canAddNewComment = canAddNewComment;
	}

	public List<AbstractComment> getComments() {
		return comments;
	}

	public void setComments(List<AbstractComment> comments) {
		this.comments = comments;
	}

}
