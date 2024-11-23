package four.pda.ui.article.comments.actions;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import androidx.loader.app.LoaderManager;
import androidx.loader.content.AsyncTaskLoader;
import androidx.loader.content.Loader;

import four.pda.R;
import four.pda.client.FourPdaClient;
import four.pda.client.model.Comment;
import four.pda.ui.LoadResult;

/**
 * Created by pavel on 07/06/16.
 */
class LikeCommentLoaderCallbacks implements LoaderManager.LoaderCallbacks<LoadResult<Void>> {

    private CommentActionsDialog dialog;
    private Context context;
	private FourPdaClient client;

	public LikeCommentLoaderCallbacks(CommentActionsDialog dialog, Context context, FourPdaClient client) {
        this.dialog = dialog;
        this.context = context;
		this.client = client;
	}

	@Override
	public Loader<LoadResult<Void>> onCreateLoader(int id, Bundle args) {
		return new AsyncTaskLoader<LoadResult<Void>>(context) {
			@Override
            public LoadResult<Void> loadInBackground() {
                try {
                    client.likeArticleComment(dialog.params.articleId(), dialog.params.id());
                    return new LoadResult<>((Void) null);
                } catch (Exception e) {
                    return new LoadResult<>(e);
                }
            }
		};
	}

	@Override
	public void onLoadFinished(Loader<LoadResult<Void>> loader, LoadResult<Void> result) {

        dialog.likeButton.setVisibility(View.VISIBLE);
        dialog.likeProgressView.setVisibility(View.INVISIBLE);

        if (result.isError()) {
            Toast.makeText(context, R.string.article_comment_like_error, Toast.LENGTH_SHORT).show();
            return;
        }

        dialog.params = new AutoValue_DialogParams(
                dialog.params.id(),
                dialog.params.authorId(),
                dialog.params.authorName(),
                dialog.params.date(),
                Comment.CanLike.ALREADY_LIKED,
                dialog.params.likeCount() + 1,
                dialog.params.content(),
                dialog.params.canReply(),
                dialog.params.articleId(),
                dialog.params.articleDate());

        dialog.updateLikes();

        dialog.eventBus.post(new UserLikesSomebodyCommentEvent(dialog.params.id(), dialog.params.likeCount()));

	}

	@Override
	public void onLoaderReset(Loader<LoadResult<Void>> loader) {
	}

}
