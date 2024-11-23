package four.pda.ui.article.comments.add;

import androidx.loader.content.AsyncTaskLoader;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.client.model.CommentsContainer;
import four.pda.ui.LoadResult;

/**
 * Created by asavinova on 01/09/16.
 */
public class AddCommentLoader extends AsyncTaskLoader<LoadResult<CommentsContainer>> {

	private static final Logger L = LoggerFactory.getLogger(AddCommentLoader.class);

	private AddCommentDialog fragment;

	public AddCommentLoader(AddCommentDialog fragment) {
		super(fragment.getActivity());
		this.fragment = fragment;
	}

	@Override
	public LoadResult<CommentsContainer> loadInBackground() {
		try {
			String message = fragment.messageEditText.getText().toString();
			CommentsContainer container = fragment.client.addComment(
					fragment.postId,
					fragment.replyId,
					message
			);
			return new LoadResult<>(container);
		} catch (Exception e) {
			L.error("Add comment request error", e);
			return new LoadResult<>(e);
		}
	}

}
