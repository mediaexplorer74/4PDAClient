package four.pda.ui.article.comments;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

import four.pda.R;
import four.pda.client.model.AbstractComment;
import four.pda.client.model.Comment;
import four.pda.client.model.CommentsContainer;
import four.pda.client.model.DeletedComment;
import four.pda.ui.article.comments.add.AddCommentViewHolder;

/**
 * Created by asavinova on 05/12/15.
 */
class CommentsAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

	enum Type {
		REGULAR,
		DELETED,
		ADD
	}

	private final LayoutInflater inflater;
	private List<AbstractComment> comments = new ArrayList<>();
	private boolean canAddNewComment = false;
	private int viewWidth;

	public CommentsAdapter(Context context) {
		inflater = LayoutInflater.from(context);
	}

	@Override
	public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
		if (viewType == Type.ADD.ordinal()) {
			View view = inflater.inflate(R.layout.add_comment_item, parent, false);
			return new AddCommentViewHolder(view);
		}

		if (viewType == Type.DELETED.ordinal()) {
			View view = inflater.inflate(R.layout.deleted_comment_item, parent, false);
			return new RecyclerView.ViewHolder(view) {};
		}

		View view = inflater.inflate(R.layout.comment_list_item, parent, false);
		return new CommentViewHolder(view);
	}

	@Override
	public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
		int type = getItemViewType(position);

		if (type == Type.ADD.ordinal()) {
			return;
		}

		AbstractComment abstractComment = comments.get(position);

		if (type == Type.REGULAR.ordinal()) {
			((CommentViewHolder) holder).setComment((Comment) abstractComment);
		}

		int left = viewWidth / 30 * abstractComment.getLevel();
		holder.itemView.setPadding(left, 0, 0, 0);
	}

	@Override
	public int getItemCount() {
		return comments.size() + (canAddNewComment ? 1 : 0);
	}

	@Override
	public int getItemViewType(int position) {
		if (position >= comments.size()) {
			return Type.ADD.ordinal();
		}

		if (comments.get(position) instanceof DeletedComment) {
			return Type.DELETED.ordinal();
		}

		return Type.REGULAR.ordinal();
	}

	public void setViewWidth(int width) {
		this.viewWidth = width;
	}

	public void setCommentsContainer(CommentsContainer container) {
		this.comments = new ArrayList<>();

		canAddNewComment = container.canAddNewComment();
		addComments(container.getComments());
	}

	public void likeChanged(long commentId, int likesCount) {

		for (int i = 0; i < comments.size(); i++) {

			AbstractComment abstractComment = comments.get(i);

			if (!(abstractComment instanceof Comment)) {
				continue;
			}

			Comment comment = (Comment) abstractComment;

			if (comment.getId() == commentId) {
				comment.getKarma().setCanLike(Comment.CanLike.ALREADY_LIKED);
				comment.getKarma().setLikesCount(likesCount);
				notifyItemChanged(i);
				break;
			}

		}

	}

	private void addComments(List<AbstractComment> tree) {

		if (tree == null) return;

		for (AbstractComment comment : tree) {
			comments.add(comment);
			addComments(comment.getChildren());
		}
	}

}
