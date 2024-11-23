package four.pda.ui.article.comments.actions;

/**
 * Событие генерируется когда пользователь лайкнул комментарий к статье.
 */
public class UserLikesSomebodyCommentEvent {

    private final long commentId;
    private final int likesCount;

    public UserLikesSomebodyCommentEvent(long commentId, int likesCount) {
        this.commentId = commentId;
        this.likesCount = likesCount;
    }

    public long getCommentId() {
        return commentId;
    }

    public int getLikesCount() {
        return likesCount;
    }

}
