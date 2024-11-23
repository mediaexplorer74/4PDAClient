using Android.App;
using Android.Content;
using Android.Os;
using Android.View;
using Android.Widget;
using Androidx.Fragment.App;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Com.Franmontiel.Persistentcookiejar;
using Org.Androidannotations.Annotations;
using Org.Slf4j;
using Java.Io;
using Java.Util;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui.Auth;
using Four.Pda.Ui.Profile;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    public class DrawerFragment : Fragment
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(DrawerFragment));
        private static readonly int LOGOUT_LOADER_ID = 0;
        private static readonly int LOGIN_REQUEST_CODE = 0;
        View allCategoryView;
        View newsCategoryView;
        View reviewsCategoryView;
        View articlesCategoryView;
        View softwareCategoryView;
        View gamesCategoryView;
        View loginView;
        View logoutView;
        View logoView;
        View profilePanel;
        ImageView profilePhotoView;
        TextView profileLoginView;
        Analytics analytics;
        Auth auth;
        EventBus eventBus;
        FourPdaClient client;
        PersistentCookieJar cookieJar;
        private IList<ChangeCategoryListener> listeners = new List();
        private Dictionary<View, CategoryType> map = new HashMap();
        public virtual void AddListener(ChangeCategoryListener listener)
        {
            listeners.Add(listener);
        }

        public virtual void RemoveListener(ChangeCategoryListener listener)
        {
            listeners.Remove(listener);
        }

        virtual void AfterViews()
        {
            ((App)GetActivity().GetApplication()).Component().Inject(this);
            map.Put(allCategoryView, CategoryType.ALL);
            map.Put(newsCategoryView, CategoryType.NEWS);
            map.Put(gamesCategoryView, CategoryType.GAMES);
            map.Put(reviewsCategoryView, CategoryType.REVIEWS);
            map.Put(articlesCategoryView, CategoryType.ARTICLES);
            map.Put(softwareCategoryView, CategoryType.SOFTWARE);
            foreach (View view in map.KeySet())
            {
                view.SetOnClickListener((view1) =>
                {
                    CategoryType category = map[view1];
                    analytics.Drawer().CategoryClicked(category);
                    foreach (ChangeCategoryListener listener in listeners)
                    {
                        listener.OnChange(category);
                    }
                });
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            eventBus.Register(this);
            UpdateProfile();
        }

        public override void OnPause()
        {
            base.OnPause();
            eventBus.Unregister(this);
        }

        public virtual void SetCategorySelected(CategoryType categoryType)
        {
            foreach (Map.Entry<View, CategoryType> entry in map.EntrySet())
            {
                entry.GetKey().SetSelected(entry.GetValue() == categoryType);
            }
        }

        public virtual void UpdateProfile()
        {
            bool isAuthorized = auth.IsAuthorized();
            loginView.SetVisibility(isAuthorized ? View.GONE : View.VISIBLE);
            logoutView.SetVisibility(isAuthorized ? View.VISIBLE : View.GONE);
            logoView.SetVisibility(isAuthorized ? View.GONE : View.VISIBLE);
            profilePanel.SetVisibility(isAuthorized ? View.VISIBLE : View.GONE);
            if (isAuthorized)
            {
                Profile profile = auth.GetProfile();
                profileLoginView.SetText(profile.GetLogin());
                Images.Load(profilePhotoView, profile.GetPhoto());
            }
        }

        virtual void LoginClicked()
        {
            analytics.Drawer().LoginClicked();
            StartActivityForResult(new Intent(GetActivity(), typeof(AuthActivity_)), LOGIN_REQUEST_CODE);
        }

        virtual void OnResult(int resultCode)
        {
            if (Activity.RESULT_OK == resultCode)
            {
                UpdateProfile();
            }
        }

        virtual void LogoutClicked()
        {
            analytics.Drawer().LogoutClicked();
            GetLoaderManager().RestartLoader(LOGOUT_LOADER_ID, null, new LogoutCallbacks()).ForceLoad();
        }

        virtual void FeedbackClicked()
        {
            analytics.Drawer().FeedbackClicked();
            Intent intent = new Intent(Intent.ACTION_SEND);
            intent.SetType("text/email");
            intent.PutExtra(Intent.EXTRA_EMAIL, new string[] { "4pda@gigahub.org" });
            intent.PutExtra(Intent.EXTRA_SUBJECT, GetString(R.@string.feedback_subject));
            StartActivity(Intent.CreateChooser(intent, GetString(R.@string.feedback_chooser_title)));
        }

        virtual void AboutClicked()
        {
            analytics.Drawer().AboutClicked();
            AboutActivity_.Intent(GetActivity()).Start();
        }

        virtual void ProfileClicked()
        {
            analytics.Drawer().ProfileClicked();
            ProfileActivity_.Intent(GetActivity()).ProfileId(auth.GetProfileId()).Start();
        }

        public virtual void OnEvent(UpdateProfileEvent @event)
        {
            UpdateProfile();
        }

        public interface ChangeCategoryListener
        {
            void OnChange(CategoryType type);
        }

        class LogoutCallbacks : LoaderCallbacks<LoadResult<bool>>
        {
            public virtual Loader<LoadResult<bool>> OnCreateLoader(int loaderId, Bundle args)
            {
                return new AnonymousAsyncTaskLoader(GetActivity());
            }

            private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
            {
                public AnonymousAsyncTaskLoader(LogoutCallbacks parent)
                {
                    this.parent = parent;
                }

                private readonly LogoutCallbacks parent;
                public LoadResult<bool> LoadInBackground()
                {
                    try
                    {
                        return new LoadResult(client.Logout());
                    }
                    catch (IOException e)
                    {
                        L.Error("Logout request error", e);
                        return new LoadResult(e);
                    }
                }
            }

            public virtual void OnLoadFinished(Loader<LoadResult<bool>> loader, LoadResult<bool> result)
            {
                if (result.GetException() == null && result.GetData())
                {
                    auth.Logout();
                    cookieJar.Clear();
                }
                else
                {
                }
            }

            //TODO Нужно ли запрашивать заново в случае ошибки?
            public virtual void OnLoaderReset(Loader<LoadResult<bool>> loader)
            {
            }
        }
    }
}