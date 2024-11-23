using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.View;
using Androidx.Appcompat.App;
using Androidx.Appcompat.Widget;
using Androidx.Recyclerview.Widget;
using Androidx.Swiperefreshlayout.Widget;
using Org.Androidannotations.Annotations;
using Org.Androidannotations.Annotations.Sharedpreferences;
using Java.Util;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using Four.Pda.Ui.Article.Comments.Actions;
using Four.Pda.Ui.Article.Comments.Add;
using Four.Pda.Ui.Auth;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments
{
    /// <summary>
    /// Created by asavinova on 05/12/15.
    /// </summary>
    public class CommentsFragment : BaseFragment
    {
        private static readonly int LOADER_ID = 0;
        private static readonly int ADD_COMMENT_AUTH_REQUEST_CODE = 0;
        long articleId;
        Date articleDate;
        Toolbar toolbar;
        SwipeRefreshLayout refresh;
        RecyclerView recyclerView;
        SupportView supportView;
        Dao dao;
        EventBus eventBus;
        Analytics analytics;
        FourPdaClient client;
        Preferences_ preferences;
        CommentsAdapter adapter;
        private AddCommentEvent addCommentEvent;
        virtual void AfterViews()
        {
            ((App)GetActivity().GetApplication()).Component().Inject(this);
            analytics.Comments().Open();
            toolbar.SetTitle(R.@string.comments_title);
            toolbar.SetNavigationIcon(R.drawable.ic_close_white_24dp);
            toolbar.SetNavigationOnClickListener((v) => GetActivity().OnBackPressed());
            if (GetView() == null)
            {
                throw new InvalidOperationException("View is NULL");
            }

            adapter = new CommentsAdapter(GetActivity());
            adapter.SetViewWidth(GetView().GetWidth());
            GetView().GetViewTreeObserver().AddOnGlobalLayoutListener(new AnonymousOnGlobalLayoutListener(this));
            recyclerView.SetAdapter(adapter);
            recyclerView.SetLayoutManager(new LinearLayoutManager(GetActivity()));
            recyclerView.AddItemDecoration(new SpaceDecorator(GetResources().GetDimensionPixelOffset(R.dimen.offset_normal)));
            refresh.SetOnRefreshListener(this.LoadData());
            refresh.SetColorSchemeResources(R.color.primary);
            refresh.SetProgressViewOffset(false, 0, (int)TypedValue.ApplyDimension(TypedValue.COMPLEX_UNIT_DIP, 24, GetResources().GetDisplayMetrics()));
            LoadData();
        }

        private sealed class AnonymousOnGlobalLayoutListener : OnGlobalLayoutListener
        {
            public AnonymousOnGlobalLayoutListener(CommentsFragment parent)
            {
                this.parent = parent;
            }

            private readonly CommentsFragment parent;
            public void OnGlobalLayout()
            {
                GetView().GetViewTreeObserver().RemoveOnGlobalLayoutListener(this);
                adapter.SetViewWidth(GetView().GetWidth());
                adapter.NotifyDataSetChanged();
            }
        }

        virtual void LoadData()
        {
            refresh.SetRefreshing(true);
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(LOADER_ID, null, new LoadArticleCommentsCallbacks(this)).ForceLoad();
        }

        public override void OnResume()
        {
            base.OnResume();
            eventBus.Register(this);
        }

        public override void OnPause()
        {
            base.OnPause();
            eventBus.Unregister(this);
        }

        public virtual void OnEvent(CommentActionsEvent @event)
        {
            Comment comment = @event.GetComment();
            DialogParams params = DialogParams.Create(comment, articleId, articleDate);
            CommentActionsDialog_.Builder().Params(@params).Build().Show(GetChildFragmentManager(), "show_comment");
        }

        public virtual void OnEvent(AddCommentEvent @event)
        {
            this.addCommentEvent = @event;
            StartActivityForResult(new Intent(GetActivity(), typeof(AuthActivity_)), ADD_COMMENT_AUTH_REQUEST_CODE);
        }

        public virtual void OnEvent(UserLikesSomebodyCommentEvent @event)
        {
            adapter.LikeChanged(@event.GetCommentId(), @event.GetLikesCount());
        }

        virtual void OnResult(int resultCode)
        {
            if (Activity.RESULT_OK == resultCode)
            {
                ShowAddCommentDialog();
            }
        }

        virtual void ShowAddCommentDialog()
        {
            if (!preferences.IsAcceptedCommentRules().Get())
            {
                new Builder(GetActivity()).SetMessage(R.@string.add_comment_text_hint).SetPositiveButton(R.@string.first_comment_dialog_ok, (dialog, which) =>
                {
                    preferences.IsAcceptedCommentRules().Put(true);
                    ShowAddCommentDialog();
                }).Show();
                return;
            }

            AddCommentDialog dialog = AddCommentDialog_.Builder().PostId(articleId).ReplyId(addCommentEvent.GetReplyId()).ReplyAuthor(addCommentEvent.GetReplyAuthor()).Build();
            GetChildFragmentManager().BeginTransaction().Add(dialog, null).CommitAllowingStateLoss();
        }

        public virtual void OnEvent(UpdateCommentsEvent @event)
        {
            adapter.SetCommentsContainer(@event.GetCommentsContainer());
            adapter.NotifyDataSetChanged();
        }

        /// <summary>
        /// http://stackoverflow.com/questions/24618829
        /// </summary>
        private class SpaceDecorator : ItemDecoration
        {
            private readonly int verticalSpace;
            public SpaceDecorator(int verticalSpace)
            {
                this.verticalSpace = verticalSpace;
            }

            public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
            {
                outRect.bottom = verticalSpace;

                // Last element has no margin
                if (parent.GetChildAdapterPosition(view) == parent.GetAdapter().GetItemCount() - 1)
                {
                    outRect.bottom = 0;
                }
            }
        }
    }
}