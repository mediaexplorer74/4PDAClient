using Android.App;
using Android.Content;
using Android.Os;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Profile
{
    /// <summary>
    /// Created by asavinova on 27/07/16.
    /// </summary>
    public class ProfileCallbacks : LoaderCallbacks<LoadResult<Profile>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(ProfileCallbacks));
        private ProfileActivity activity;
        private FourPdaClient pdaClient;
        public ProfileCallbacks(ProfileActivity activity)
        {
            this.activity = activity;
            pdaClient = activity.client;
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
                    return new LoadResult(pdaClient.GetProfile(activity.profileId));
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
                activity.supportView.ShowError(activity.GetString(R.@string.profile_network_error), (v) => activity.LoadProfile());
                return;
            }

            activity.UpdateProfile(result.GetData());
        }

        public virtual void OnLoaderReset(Loader<LoadResult<Profile>> loader)
        {
        }
    }
}