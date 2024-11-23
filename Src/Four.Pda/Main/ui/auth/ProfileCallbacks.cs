using Android.App;
using Android.Content;
using Android.Os;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Auth
{
    /// <summary>
    /// Created by asavinova on 21/02/16.
    /// </summary>
    class ProfileCallbacks : LoaderCallbacks<LoadResult<Profile>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(ProfileCallbacks));
        private AuthActivity activity;
        private FourPdaClient pdaClient;
        private Auth auth;
        public ProfileCallbacks(AuthActivity activity)
        {
            this.activity = activity;
            pdaClient = activity.client;
            auth = activity.auth;
        }

        public virtual Loader<LoadResult<Profile>> OnCreateLoader(int id, Bundle args)
        {
            return new AnonymousAsyncTaskLoader(activity);
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(ProfileCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly ProfileCallbacks parent;
            public LoadResult<Profile> LoadInBackground()
            {
                try
                {
                    return new LoadResult(pdaClient.GetProfile(auth.GetProfileId()));
                }
                catch (Exception e)
                {
                    L.Error("Profile request error", e);
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<Profile>> loader, LoadResult<Profile> result)
        {
            if (result.IsError())
            {
                activity.supportView.ShowError(activity.GetString(R.@string.auth_network_error), (v) => activity.LoadProfile());
                return;
            }

            activity.auth.SetProfile(result.GetData());
            activity.supportView.Hide();
            activity.SetResult(Activity.RESULT_OK);
            activity.Finish();
        }

        public virtual void OnLoaderReset(Loader<LoadResult<Profile>> loader)
        {
        }
    }
}