package four.pda.ui.article.comments;

import four.pda.client.model.CommentsContainer;

/**
 * Created by asavinova on 16/03/16.
 */
public class UpdateCommentsEvent {

	private CommentsContainer container;

	public UpdateCommentsEvent(CommentsContainer container) {
		this.container = container;
	}

	public CommentsContainer getCommentsContainer() {
		return container;
	}

}
