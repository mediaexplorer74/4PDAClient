using Android.Os;
using Android.View;
using Android.Widget;
using Androidx.Annotation;
using Androidx.Appcompat.App;
using Androidx.Appcompat.Widget;
using Com.Google.Android.Material.Textfield;
using Org.Androidannotations.Annotations;
using Org.Slf4j;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Auth
{
    /// <summary>
    /// Created by asavinova on 19/02/16.
    /// </summary>
    public class AuthActivity : AppCompatActivity
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(AuthActivity));
        private static readonly int CAPTCHA_LOADER_ID = 0;
        private static readonly int LOGIN_LOADER_ID = 1;
        private static readonly int PROFILE_LOADER_ID = 2;
        Toolbar toolbar;
        TextInputEditText loginView;
        TextInputEditText passwordView;
        TextInputEditText captchaTextView;
        ImageView captchaImageView;
        SupportView supportView;
        Auth auth;
        FourPdaClient client;
        Captcha captcha;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ((App)GetApplication()).Component().Inject(this);
            if (auth.IsAuthorized())
            {
                SetResult(RESULT_OK);
                Finish();
                return;
            }

            SetContentView(R.layout.signin);
        }

        virtual void AfterViews()
        {
            L.Debug("Start login activity");
            toolbar.SetTitle(R.@string.auth_title);
            toolbar.SetNavigationIcon(R.drawable.ic_close_white_24dp);
            toolbar.SetNavigationOnClickListener((v) => Finish());
            toolbar.InflateMenu(R.menu.auth);
            toolbar.SetOnMenuItemClickListener(new MenuListener());
            LoadCaptcha();
        }

        virtual void LoadCaptcha()
        {
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(CAPTCHA_LOADER_ID, null, new CaptchaCallbacks(this)).ForceLoad();
        }

        virtual void LoadProfile()
        {
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(PROFILE_LOADER_ID, null, new ProfileCallbacks(this)).ForceLoad();
        }

        virtual void SignIn()
        {
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(LOGIN_LOADER_ID, null, new LoginCallbacks(this)).ForceLoad();
        }

        private class MenuListener : OnMenuItemClickListener
        {
            public virtual bool OnMenuItemClick(MenuItem item)
            {
                switch (item.GetItemId())
                {
                    case R.id.sign_in:
                        SignIn();
                        break;
                }

                return false;
            }
        }
    }
}