using Android.Util;
using Android.View;
using Androidx.Appcompat.Widget;
using Androidx.Recyclerview.Widget;
using Androidx.Swiperefreshlayout.Widget;
using Org.Androidannotations.Annotations;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Client;
using Four.Pda.Ui;
using Four.Pda.Ui.Article;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.List
{
    /// <summary>
    /// Created by asavinova on 10/04/15.
    /// </summary>
    public class ListFragment : BaseFragment, OnRefreshListener
    {
        private static readonly int LOADER_ID = 0;
        CategoryType category;
        View container;
        Toolbar toolbar;
        SwipeRefreshLayout refresh;
        RecyclerView recyclerView;
        View upButton;
        SupportView supportView;
        Dao dao;
        Analytics analytics;
        FourPdaClient client;
        int page = 1;
        ArticlesAdapter adapter;
        private GridLayoutManager layoutManager;
        virtual void AfterViews()
        {
            ((App)GetActivity().GetApplication()).Component().Inject(this);
            toolbar.SetTitle(CategoryTitleMap[category]);
            ShowMenuIcon();
            toolbar.InflateMenu(R.menu.articles_list);
            toolbar.GetMenu().FindItem(R.id.search).SetOnMenuItemClickListener((item) =>
            {
                SearchActivity_.Intent(GetActivity()).Start();
                return true;
            });
            container.GetViewTreeObserver().AddOnGlobalLayoutListener(() =>
            {
                if (container == null)
                    return;
                int spanCount = (int)(container.GetWidth() / container.GetResources().GetDimension(R.dimen.list_item_width));
                if (spanCount > 1)
                {
                    layoutManager.SetSpanCount(spanCount);
                }
            });
            layoutManager = new GridLayoutManager(GetActivity(), 1, LinearLayoutManager.VERTICAL, false);
            recyclerView.SetLayoutManager(layoutManager);
            adapter = new ArticlesAdapter(GetActivity(), null);
            recyclerView.SetAdapter(adapter);
            recyclerView.AddOnScrollListener(new AnonymousOnScrollListener(this));
            refresh.SetOnRefreshListener(this);
            refresh.SetColorSchemeResources(R.color.primary);
            float progressOffset = TypedValue.ApplyDimension(TypedValue.COMPLEX_UNIT_DIP, 24, GetResources().GetDisplayMetrics());
            refresh.SetProgressViewOffset(false, 0, (int)progressOffset);
            LoadData();
        }

        private sealed class AnonymousOnScrollListener : OnScrollListener
        {
            public AnonymousOnScrollListener(ListFragment parent)
            {
                this.parent = parent;
            }

            private readonly ListFragment parent;
            public void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                int visibleItemCount = layoutManager.GetChildCount();
                int totalItemCount = layoutManager.GetItemCount();
                int firstVisibleItemPosition = layoutManager.FindFirstVisibleItemPosition();
                int loadLevel = totalItemCount - visibleItemCount;
                bool shouldLoad = firstVisibleItemPosition >= loadLevel;
                if (!refresh.IsRefreshing() && shouldLoad)
                {
                    LoadData();
                }
            }
        }

        virtual void UpButton()
        {
            analytics.ArticlesList().ScrollUp(layoutManager.FindFirstVisibleItemPosition());
            layoutManager.ScrollToPosition(0);
        }

        virtual void LoadData()
        {
            refresh.SetRefreshing(true);
            int itemCount = adapter.GetItemCount();
            if (itemCount == 0)
            {
                supportView.ShowProgress();
            }

            GetLoaderManager().RestartLoader(LOADER_ID, null, new ListCallbacks(this)).ForceLoad();
        }

        public override void OnRefresh()
        {
            page = 1;
            LoadData();
        }

        public virtual CategoryType GetCategory()
        {
            return category;
        }
    }
}