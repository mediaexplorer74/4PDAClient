package four.pda.ui.article.comments;

import android.text.Html;
import android.view.View;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.text.SimpleDateFormat;

import butterknife.BindView;
import butterknife.ButterKnife;
import four.pda.EventBus_;
import four.pda.R;
import four.pda.client.model.Comment;

/**
 * Created by asavinova on 05/12/15.
 */
class CommentViewHolder extends RecyclerView.ViewHolder {

	private static final SimpleDateFormat DATE_FORMAT = new SimpleDateFormat("dd.MM.yy HH:ss");

	@BindView(R.id.author_info_view) View authorInfoView;
	@BindView(R.id.delimiter_view) View delimiterView;
	@BindView(R.id.nick_view) TextView nickView;
	@BindView(R.id.date_view) TextView dateView;

	@BindView(R.id.likes_check_view) View likesCheckView;
	@BindView(R.id.likes_count_view) TextView likesView;

	@BindView(R.id.content_view) TextView contentView;

	public CommentViewHolder(final View view) {
		super(view);
		ButterKnife.bind(this, view);
	}

	public void setComment(final Comment comment) {

		nickView.setText(comment.getUser().getNickname());

		String verboseDate = DATE_FORMAT.format(comment.getDate());
		dateView.setText(verboseDate);

		int likes = comment.getKarma().getLikesCount();
		likesView.setText(String.valueOf(likes));

		boolean alreadyLiked = comment.getKarma().getCanLike() == Comment.CanLike.ALREADY_LIKED;
		likesCheckView.setVisibility(alreadyLiked ? View.VISIBLE : View.GONE);

		contentView.setText(Html.fromHtml(comment.getContent()));

		itemView.setOnClickListener(v ->
				EventBus_.getInstance_(v.getContext())
				.post(new CommentActionsEvent(comment)));

	}

}
