using Android.App;
using Android.Content;
using Android.Os;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Auth
{
    /// <summary>
    /// Created by asavinova on 21/02/16.
    /// </summary>
    class CaptchaCallbacks : LoaderCallbacks<LoadResult<Captcha>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(CaptchaCallbacks));
        private AuthActivity activity;
        public CaptchaCallbacks(AuthActivity activity)
        {
            this.activity = activity;
        }

        public virtual Loader<LoadResult<Captcha>> OnCreateLoader(int id, Bundle args)
        {
            return new AnonymousAsyncTaskLoader(activity);
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(CaptchaCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly CaptchaCallbacks parent;
            public LoadResult<Captcha> LoadInBackground()
            {
                try
                {
                    return new LoadResult(activity.client.GetCaptcha());
                }
                catch (Exception e)
                {
                    L.Error("Captcha request error", e);
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<Captcha>> loader, LoadResult<Captcha> result)
        {
            activity.captchaTextView.SetText("");
            if (result.IsError())
            {
                activity.supportView.ShowError(activity.GetString(R.@string.auth_network_error), (v) => activity.LoadCaptcha());
                return;
            }

            activity.captcha = result.GetData();
            Images.Load(activity.captchaImageView, activity.captcha.GetUrl());
            activity.supportView.Hide();
        }

        public virtual void OnLoaderReset(Loader<LoadResult<Captcha>> loader)
        {
        }
    }
}