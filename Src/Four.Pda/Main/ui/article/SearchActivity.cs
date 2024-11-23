using Android.Os;
using Androidx.Appcompat.App;
using Org.Androidannotations.Annotations;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Ui.Article.Comments;
using Four.Pda.Ui.Article.One;
using Four.Pda.Ui.Article.Search;
using System.Diagnostics;

namespace Four.Pda.Ui.Article
{
    /// <summary>
    /// Created by asavinova on 02/06/16.
    /// </summary>
    public class SearchActivity : AppCompatActivity
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(SearchActivity));
        EventBus eventBus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (savedInstanceState == null)
            {
                SearchFragment fragment = SearchFragment_.Builder().Build();
                GetSupportFragmentManager().BeginTransaction().Add(R.id.list_container, fragment).Commit();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            eventBus.Register(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            eventBus.Unregister(this);
        }

        public virtual void OnEvent(ShowArticleEvent @event)
        {
            L.Debug("Show article with id {}", @event.GetId());
            ArticleFragment fragment = ArticleFragment_.Builder().Id(@event.GetId()).Date(@event.GetDate()).Title(@event.GetTitle()).Image(@event.GetImage()).AuthorId(@event.GetAuthorId()).AuthorName(@event.GetAuthorName()).LabelName(@event.GetLabelName()).LabelColor(@event.GetLabelColor()).Build();
            GetSupportFragmentManager().BeginTransaction().Replace(R.id.item_container, fragment).AddToBackStack(null).Commit();
        }

        public virtual void OnEvent(ShowArticleCommentsEvent @event)
        {
            L.Debug("Show comments for article with id {}", @event.GetArticleId());
            CommentsFragment fragment = CommentsFragment_.Builder().ArticleId(@event.GetArticleId()).ArticleDate(@event.GetArticleDate()).Build();
            GetSupportFragmentManager().BeginTransaction().Replace(R.id.item_container, fragment).AddToBackStack(null).Commit();
        }

        public override void OnBackPressed()
        {
            if (GetSupportFragmentManager().GetBackStackEntryCount() > 0)
            {
                base.OnBackPressed();
                return;
            }

            Finish();
        }
    }
}