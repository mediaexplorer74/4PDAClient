package four.pda.ui.article.comments;

import android.os.Bundle;

import androidx.loader.app.LoaderManager;
import androidx.loader.content.AsyncTaskLoader;
import androidx.loader.content.Loader;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.R;
import four.pda.client.model.CommentsContainer;
import four.pda.ui.LoadResult;

/**
 * Created by asavinova on 11/03/16.
 */
class LoadArticleCommentsCallbacks implements LoaderManager.LoaderCallbacks<LoadResult<CommentsContainer>> {

	private static final Logger L = LoggerFactory.getLogger(LoadArticleCommentsCallbacks.class);

	private CommentsFragment fragment;

	public LoadArticleCommentsCallbacks(CommentsFragment fragment) {
		this.fragment = fragment;
	}

	@Override
	public Loader<LoadResult<CommentsContainer>> onCreateLoader(final int id, Bundle args) {
		return new AsyncTaskLoader<LoadResult<CommentsContainer>>(fragment.getActivity()) {
			@Override
			public LoadResult<CommentsContainer> loadInBackground() {
				try {
					return new LoadResult<>(fragment.client.getArticleComments(fragment.articleDate, fragment.articleId));
				} catch (Exception e) {
					L.error("Article comments request error", e);
					return new LoadResult<>(e);
				}
			}
		};
	}

	@Override
	public void onLoadFinished(Loader<LoadResult<CommentsContainer>> loader, LoadResult<CommentsContainer> result) {
		fragment.refresh.setRefreshing(false);

		if (result.getException() == null) {
			fragment.adapter.setCommentsContainer(result.getData());
			fragment.adapter.notifyDataSetChanged();
			fragment.supportView.hide();
			return;
		}

		fragment.supportView.showError(fragment.getString(R.string.comments_network_error), v -> fragment.loadData());
	}

	@Override
	public void onLoaderReset(Loader<LoadResult<CommentsContainer>> loader) {
	}

}
