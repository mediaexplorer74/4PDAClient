using Android.Os;
using Android.Widget;
using Androidx.Appcompat.App;
using Androidx.Core.View;
using Androidx.Drawerlayout.Widget;
using Androidx.Fragment.App;
using Org.Androidannotations.Annotations;
using Org.Androidannotations.Annotations.Sharedpreferences;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Ui;
using Four.Pda.Ui.Article.Comments;
using Four.Pda.Ui.Article.List;
using Four.Pda.Ui.Article.One;
using System.Diagnostics;

namespace Four.Pda.Ui.Article
{
    /// <summary>
    /// Created by asavinova on 10/04/15.
    /// </summary>
    public class NewsActivity : AppCompatActivity, ChangeCategoryListener
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(NewsActivity));
        DrawerFragment drawer;
        DrawerLayout drawerLayout;
        Preferences_ preferences;
        EventBus eventBus;
        CategoryType category = CategoryType.ALL;
        private long backButtonLastPressedTime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (savedInstanceState == null)
            {
                L.Debug("Add list fragment with category {}", category);
                ListFragment fragment = ListFragment_.Builder().Category(category).Build();
                GetSupportFragmentManager().BeginTransaction().Add(R.id.list_container, fragment).Commit();
            }

            GetSupportFragmentManager().AddOnBackStackChangedListener(() =>
            {
                Fragment fragment = GetSupportFragmentManager().FindFragmentById(R.id.list_container);
                if (fragment == null)
                {
                    return;
                }

                category = ((ListFragment)fragment).GetCategory();
                drawer.SetCategorySelected(category);
            });
        }

        virtual void AfterViews()
        {
            if (preferences.IsFirstRun().Get())
            {
                if (drawerLayout != null)
                {
                    drawerLayout.OpenDrawer(GravityCompat.START);
                }

                preferences.IsFirstRun().Put(false);
            }

            drawer.SetCategorySelected(category);
        }

        protected override void OnResume()
        {
            base.OnResume();
            drawer.AddListener(this);
            eventBus.Register(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            drawer.RemoveListener(this);
            eventBus.Unregister(this);
        }

        public override void OnChange(CategoryType newCategory)
        {
            L.Debug("Category changed to {}", newCategory.Name());
            if (drawerLayout != null)
            {
                drawerLayout.CloseDrawer(GravityCompat.START);
            }

            Fragment itemFragment = GetSupportFragmentManager().FindFragmentById(R.id.item_container);
            if (category == newCategory)
            {

                // Если категория та же, то убираем фрагмент со статьей и все
                if (itemFragment != null)
                {
                    GetSupportFragmentManager().BeginTransaction().Remove(itemFragment).AddToBackStack(null).Commit();
                }

                return;
            }

            ListFragment listFragment = ListFragment_.Builder().Category(newCategory).Build();
            FragmentTransaction transaction = GetSupportFragmentManager().BeginTransaction().Replace(R.id.list_container, listFragment);
            if (itemFragment != null)
            {
                transaction.Remove(itemFragment);
            }

            transaction.AddToBackStack(null).Commit();
            category = newCategory;
        }

        // Если категория та же, то убираем фрагмент со статьей и все
        public virtual void OnEvent(ShowArticleEvent @event)
        {
            L.Debug("Show article with id {}", @event.GetId());
            ArticleFragment fragment = ArticleFragment_.Builder().Id(@event.GetId()).Date(@event.GetDate()).Title(@event.GetTitle()).Image(@event.GetImage()).AuthorId(@event.GetAuthorId()).AuthorName(@event.GetAuthorName()).LabelName(@event.GetLabelName()).LabelColor(@event.GetLabelColor()).Build();
            GetSupportFragmentManager().BeginTransaction().Replace(R.id.item_container, fragment).AddToBackStack(null).Commit();
        }

        // Если категория та же, то убираем фрагмент со статьей и все
        public virtual void OnEvent(ShowArticleCommentsEvent @event)
        {
            L.Debug("Show comments for article with id {}", @event.GetArticleId());
            CommentsFragment fragment = CommentsFragment_.Builder().ArticleId(@event.GetArticleId()).ArticleDate(@event.GetArticleDate()).Build();
            GetSupportFragmentManager().BeginTransaction().Replace(R.id.item_container, fragment).AddToBackStack(null).Commit();
        }

        // Если категория та же, то убираем фрагмент со статьей и все
        public override void OnBackPressed()
        {
            if (GetSupportFragmentManager().GetBackStackEntryCount() > 0)
            {
                base.OnBackPressed();
                return;
            }

            if (System.CurrentTimeMillis() - backButtonLastPressedTime > 2000)
            {
                Toast.MakeText(this, R.@string.exit_message, Toast.LENGTH_SHORT).Show();
                backButtonLastPressedTime = System.CurrentTimeMillis();
                return;
            }

            Finish();
        }
    }
}