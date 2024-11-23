package four.pda.ui.article.comments.add;

import android.app.Dialog;
import android.text.TextUtils;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.appcompat.widget.Toolbar;
import androidx.fragment.app.DialogFragment;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.Click;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.FragmentArg;
import org.androidannotations.annotations.ViewById;

import javax.inject.Inject;

import four.pda.App;
import four.pda.Dao;
import four.pda.EventBus;
import four.pda.R;
import four.pda.client.FourPdaClient;
import four.pda.client.model.CommentsContainer;
import four.pda.ui.Keyboard;
import four.pda.ui.SupportView;
import four.pda.ui.article.comments.UpdateCommentsEvent;

/**
 * Created by asavinova on 16/03/16.
 */
@EFragment(R.layout.comment_add_dialog)
public class AddCommentDialog extends DialogFragment {

	private static final int ADD_COMMENT_LOADER_ID = 0;

	@FragmentArg long postId;
	@FragmentArg Long replyId;
	@FragmentArg String replyAuthor;

	@ViewById
	Toolbar toolbar;
	@ViewById EditText messageEditText;
	@ViewById SupportView supportView;

	@Inject FourPdaClient client;
	@Inject Keyboard keyboard;

	@Bean EventBus eventBus;
	@Bean Dao dao;

	@AfterViews
	void afterViews() {
		((App) getActivity().getApplication()).component().inject(this);

		toolbar.setTitle(replyId == null ? R.string.comments_new : R.string.comments_reply_title);
		toolbar.setNavigationIcon(R.drawable.ic_close_white_24dp);
		toolbar.setNavigationOnClickListener(v -> AddCommentDialog.this.dismiss());

		if (replyId != null) {
			String replyText = replyAuthor + ",\n";
			messageEditText.setText(replyText);
			messageEditText.setSelection(replyText.length());
		}

		keyboard.showFor(messageEditText);
	}

	@Override
	public void onStart() {
		super.onStart();

		Dialog dialog = getDialog();
		if (dialog != null) {
			// Максимально растягивает окно диалога
			dialog.getWindow().setLayout(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT);
		}
	}

	@Click(R.id.add_comment_button)
	void addCommentClicked() {
		String message = messageEditText.getText().toString();
		if (TextUtils.isEmpty(message)) {
			messageEditText.setError("Empty!");
		} else {
			addComment();
		}
	}

	void addComment() {
		supportView.showProgress();
		getLoaderManager().restartLoader(ADD_COMMENT_LOADER_ID, null, new AddCommentCallbacks(this)).forceLoad();
	}

	void updateComments(CommentsContainer comments) {
		eventBus.post(new UpdateCommentsEvent(comments));
		dismiss();
	}

}
