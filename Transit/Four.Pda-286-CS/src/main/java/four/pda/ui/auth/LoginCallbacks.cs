using Android.App;
using Android.Content;
using Android.Os;
using Org.Slf4j;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Exceptions;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Auth
{
    /// <summary>
    /// Created by asavinova on 21/02/16.
    /// </summary>
    class LoginCallbacks : LoaderCallbacks<LoadResult<long>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(LoginCallbacks));
        private AuthActivity activity;
        public LoginCallbacks(AuthActivity activity)
        {
            this.activity = activity;
        }

        public virtual Loader<LoadResult<long>> OnCreateLoader(int id, Bundle args)
        {
            return new AnonymousAsyncTaskLoader(activity);
        }

        private sealed class AnonymousAsyncTaskLoader : AsyncTaskLoader
        {
            public AnonymousAsyncTaskLoader(LoginCallbacks parent)
            {
                this.parent = parent;
            }

            private readonly LoginCallbacks parent;
            public LoadResult<long> LoadInBackground()
            {
                try
                {
                    LoginParams params = new LoginParams();
                    @params.SetLogin(activity.loginView.GetText().ToString());
                    @params.SetPassword(activity.passwordView.GetText().ToString());
                    @params.SetCaptcha(activity.captchaTextView.GetText().ToString());
                    if (activity.captcha != null)
                    {
                        @params.SetCaptchaSig(activity.captcha.GetSig());
                        @params.SetCaptchaTime(activity.captcha.GetTime());
                    }

                    return new LoadResult(activity.client.Login(@params));
                }
                catch (Exception e)
                {
                    L.Error("Login request error", e);
                    return new LoadResult(e);
                }
            }
        }

        public virtual void OnLoadFinished(Loader<LoadResult<long>> loader, LoadResult<long> result)
        {
            if (result.IsError())
            {
                if (result.GetException() is LoginException)
                {
                    LoginException exception = (LoginException)result.GetException();
                    StringBuilder errors = new StringBuilder();
                    foreach (string e in exception.GetErrors())
                    {
                        errors.Append(e);
                        errors.Append(" ");
                    }

                    activity.supportView.ShowError(errors.ToString().Trim(), (v) => activity.LoadCaptcha());
                    return;
                }

                activity.supportView.ShowError(activity.GetString(R.@string.auth_network_error), (v) => activity.SignIn());
                return;
            }

            long memberId = result.GetData();
            activity.auth.Login(memberId);
            activity.LoadProfile();
            activity.supportView.Hide();
        }

        public virtual void OnLoaderReset(Loader<LoadResult<long>> loader)
        {
        }
    }
}