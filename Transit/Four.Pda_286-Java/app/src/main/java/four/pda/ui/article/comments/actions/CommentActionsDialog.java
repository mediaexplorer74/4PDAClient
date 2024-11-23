package four.pda.ui.article.comments.actions;

import android.app.Activity;
import android.content.Intent;
import android.text.Html;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.widget.Toolbar;
import androidx.core.app.ShareCompat;
import androidx.fragment.app.DialogFragment;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.Click;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.FragmentArg;
import org.androidannotations.annotations.OnActivityResult;
import org.androidannotations.annotations.ViewById;

import java.text.SimpleDateFormat;

import javax.inject.Inject;

import four.pda.App;
import four.pda.EventBus;
import four.pda.R;
import four.pda.analytics.Analytics;
import four.pda.client.FourPdaClient;
import four.pda.client.model.Comment;
import four.pda.ui.article.comments.add.AddCommentEvent;
import four.pda.ui.auth.AuthActivity_;
import four.pda.ui.profile.ProfileActivity_;

/**
 * Created by asavinova on 25/04/16.
 */
@EFragment(R.layout.comment_actions_dialog)
public class CommentActionsDialog extends DialogFragment {

	private static final SimpleDateFormat DATE_FORMAT = new SimpleDateFormat("dd.MM.yy HH:ss");
	private static final int LIKE_AUTH_REQUEST_CODE = 0;

	@FragmentArg DialogParams params;

	@ViewById Toolbar toolbar;

	@ViewById TextView nickView;
	@ViewById TextView dateView;

	@ViewById View likesCheckView;
	@ViewById TextView likesCountView;

	@ViewById TextView contentView;

	@ViewById TextView replyButton;

	@ViewById View likeProgressView;
 	@ViewById ImageView likeButton;

	@Bean EventBus eventBus;
	@Bean Analytics analytics;

	@Inject FourPdaClient client;

	@AfterViews
	void afterViews() {

		((App) getContext().getApplicationContext()).component().inject(this);

		analytics.comments().showDialog();

		toolbar.setTitle(R.string.show_comment_dialog_title);
		toolbar.setNavigationIcon(R.drawable.ic_close_white_24dp);
		toolbar.setNavigationOnClickListener(v -> CommentActionsDialog.this.dismiss());

		nickView.setText(params.authorName());

		String verboseDate = DATE_FORMAT.format(params.date());
		dateView.setText(verboseDate);

		contentView.setText(Html.fromHtml(params.content()));

		replyButton.setVisibility(params.canReply() ? View.VISIBLE : View.GONE);

		updateLikes();

	}

	@Click(R.id.share_button)
	void share() {
		analytics.comments().share();
		startActivity(ShareCompat.IntentBuilder.from(getActivity())
				.setType("text/plain")
				.setText(client.getCommentUrl(params.articleId(), params.articleDate(), params.id()))
				.createChooserIntent());
		dismiss();
	}

	@Click(R.id.like_button)
	void likeButton() {
		analytics.comments().like();
		startActivityForResult(new Intent(getActivity(), AuthActivity_.class), LIKE_AUTH_REQUEST_CODE);
	}

	@Click(R.id.reply_button)
	void reply() {
		analytics.comments().reply();
		eventBus.post(new AddCommentEvent(params.id(), params.authorName()));
		dismiss();
	}

	@Click(R.id.profile_button)
	void profile() {
		analytics.comments().profileClicked();
		ProfileActivity_.intent(getActivity())
				.profileId(params.authorId())
				.start();
	}

	@OnActivityResult(LIKE_AUTH_REQUEST_CODE)
	void onResult(int resultCode) {
		if (Activity.RESULT_OK == resultCode) {
			like();
		}
	}

	void like() {

		switch (params.canLike()) {

			case ALREADY_LIKED:
				Toast.makeText(getContext(), R.string.article_comment_like_already, Toast.LENGTH_SHORT).show();
				break;

			case CAN:

				likeButton.setVisibility(View.INVISIBLE);
				likeProgressView.setVisibility(View.VISIBLE);

				getLoaderManager().initLoader(0, null,
						new LikeCommentLoaderCallbacks(this, getContext(), client)).forceLoad();

				break;

			default:
				throw new IllegalStateException("Unexpected CanLike value " + params.canLike().name());

		}

	}

	void updateLikes() {

		likesCountView.setText(String.valueOf(params.likeCount()));

		likesCheckView.setVisibility(
				params.canLike() == Comment.CanLike.ALREADY_LIKED ?
						View.VISIBLE : View.GONE
		);

		likeButton.setVisibility(params.canLike() == Comment.CanLike.CANT ? View.INVISIBLE : View.VISIBLE);

	}

}
