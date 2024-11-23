package four.pda.ui.article.comments;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Rect;
import android.util.TypedValue;
import android.view.View;
import android.view.ViewTreeObserver;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.widget.Toolbar;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.FragmentArg;
import org.androidannotations.annotations.OnActivityResult;
import org.androidannotations.annotations.ViewById;
import org.androidannotations.annotations.sharedpreferences.Pref;

import java.util.Date;

import javax.inject.Inject;

import four.pda.App;
import four.pda.Dao;
import four.pda.EventBus;
import four.pda.Preferences_;
import four.pda.R;
import four.pda.analytics.Analytics;
import four.pda.client.FourPdaClient;
import four.pda.client.model.Comment;
import four.pda.ui.BaseFragment;
import four.pda.ui.SupportView;
import four.pda.ui.article.comments.actions.CommentActionsDialog_;
import four.pda.ui.article.comments.actions.DialogParams;
import four.pda.ui.article.comments.actions.UserLikesSomebodyCommentEvent;
import four.pda.ui.article.comments.add.AddCommentDialog;
import four.pda.ui.article.comments.add.AddCommentDialog_;
import four.pda.ui.article.comments.add.AddCommentEvent;
import four.pda.ui.auth.AuthActivity_;

/**
 * Created by asavinova on 05/12/15.
 */
@EFragment(R.layout.comments_list)
public class CommentsFragment extends BaseFragment {

	private static final int LOADER_ID = 0;
	private static final int ADD_COMMENT_AUTH_REQUEST_CODE = 0;

	@FragmentArg long articleId;
	@FragmentArg Date articleDate;

	@ViewById Toolbar toolbar;
	@ViewById SwipeRefreshLayout refresh;
	@ViewById RecyclerView recyclerView;
	@ViewById SupportView supportView;

	@Bean Dao dao;
	@Bean EventBus eventBus;
	@Bean Analytics analytics;

	@Inject FourPdaClient client;

	@Pref Preferences_ preferences;

	CommentsAdapter adapter;

	private AddCommentEvent addCommentEvent;

	@AfterViews
	void afterViews() {
		((App) getActivity().getApplication()).component().inject(this);

		analytics.comments().open();

		toolbar.setTitle(R.string.comments_title);
		toolbar.setNavigationIcon(R.drawable.ic_close_white_24dp);
		toolbar.setNavigationOnClickListener(v -> getActivity().onBackPressed());

		if (getView() == null) {
			throw new IllegalStateException("View is NULL");
		}

		adapter = new CommentsAdapter(getActivity());
		adapter.setViewWidth(getView().getWidth());

		getView().getViewTreeObserver().addOnGlobalLayoutListener(new ViewTreeObserver.OnGlobalLayoutListener() {
			@Override
			public void onGlobalLayout() {
				getView().getViewTreeObserver().removeOnGlobalLayoutListener(this);
				adapter.setViewWidth(getView().getWidth());
				adapter.notifyDataSetChanged();
			}
		});

		recyclerView.setAdapter(adapter);
		recyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
		recyclerView.addItemDecoration(new SpaceDecorator(getResources().getDimensionPixelOffset(R.dimen.offset_normal)));

		refresh.setOnRefreshListener(this::loadData);
		refresh.setColorSchemeResources(R.color.primary);
		refresh.setProgressViewOffset(false, 0,
				(int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 24, getResources().getDisplayMetrics()));

		loadData();
	}

	void loadData() {
		refresh.setRefreshing(true);
		supportView.showProgress();

		getLoaderManager().restartLoader(LOADER_ID, null, new LoadArticleCommentsCallbacks(this)).forceLoad();
	}

	@Override
	public void onResume() {
		super.onResume();
		eventBus.register(this);
	}

	@Override
	public void onPause() {
		super.onPause();
		eventBus.unregister(this);
	}

	public void onEvent(CommentActionsEvent event) {
		Comment comment = event.getComment();
		DialogParams params = DialogParams.create(comment, articleId, articleDate);
		CommentActionsDialog_.builder()
				.params(params)
				.build()
				.show(getChildFragmentManager(), "show_comment");
	}

	public void onEvent(AddCommentEvent event) {
		this.addCommentEvent = event;
		startActivityForResult(new Intent(getActivity(), AuthActivity_.class), ADD_COMMENT_AUTH_REQUEST_CODE);
	}

	public void onEvent(UserLikesSomebodyCommentEvent event) {
		adapter.likeChanged(event.getCommentId(), event.getLikesCount());
	}

	@OnActivityResult(ADD_COMMENT_AUTH_REQUEST_CODE)
	void onResult(int resultCode) {
		if (Activity.RESULT_OK == resultCode) {
			showAddCommentDialog();
		}
	}

	void showAddCommentDialog() {

		if (!preferences.isAcceptedCommentRules().get()) {

			new AlertDialog.Builder(getActivity())
					.setMessage(R.string.add_comment_text_hint)
					.setPositiveButton(R.string.first_comment_dialog_ok, (dialog, which) -> {
						preferences.isAcceptedCommentRules().put(true);
						showAddCommentDialog();
					})
					.show();

			return;
		}

		AddCommentDialog dialog = AddCommentDialog_.builder()
				.postId(articleId)
				.replyId(addCommentEvent.getReplyId())
				.replyAuthor(addCommentEvent.getReplyAuthor())
				.build();

		getChildFragmentManager().beginTransaction()
				.add(dialog, null)
				.commitAllowingStateLoss();
	}

	public void onEvent(UpdateCommentsEvent event) {
		adapter.setCommentsContainer(event.getCommentsContainer());
		adapter.notifyDataSetChanged();
	}

	/**
	 * http://stackoverflow.com/questions/24618829
	 */
	private class SpaceDecorator extends RecyclerView.ItemDecoration {

		private final int verticalSpace;

		public SpaceDecorator(int verticalSpace) {
			this.verticalSpace = verticalSpace;
		}

		@Override
		public void getItemOffsets(Rect outRect, View view, RecyclerView parent,
								   RecyclerView.State state) {

			outRect.bottom = verticalSpace;

			// Last element has no margin
			if (parent.getChildAdapterPosition(view) == parent.getAdapter().getItemCount() - 1) {
				outRect.bottom = 0;
			}

		}

	}

}
