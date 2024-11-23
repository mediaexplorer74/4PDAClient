package four.pda.ui.article.comments.actions;

import android.os.Parcelable;

import com.google.auto.value.AutoValue;

import java.util.Date;

import four.pda.client.model.Comment;

/**
 * Created by pavel on 07/06/16.
 */
@AutoValue
public abstract class DialogParams implements Parcelable {

    abstract long id();

	abstract long authorId();

    abstract String authorName();

    abstract Date date();

    /**
	 * @return {@link four.pda.client.model.Comment.CanLike#serverValue}
	 * @see four.pda.client.model.Comment.CanLike#fromServerValue(int)
	 */
    abstract Comment.CanLike canLike();

    abstract int likeCount();

    abstract String content();

    abstract boolean canReply();

    abstract long articleId();

    abstract Date articleDate();

    public static DialogParams create(Comment comment, long articleId, Date articleDate) {
        return new AutoValue_DialogParams(
                comment.getId(),
                comment.getUser().getId(),
                comment.getUser().getNickname(),
                comment.getDate(),
                comment.getKarma().getCanLike(),
                comment.getKarma().getLikesCount(),
                comment.getContent(),
                comment.canReply(),
                articleId,
                articleDate
        );
    }

}
