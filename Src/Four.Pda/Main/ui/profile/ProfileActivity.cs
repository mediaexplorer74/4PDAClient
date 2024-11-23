using Android.Webkit;
using Androidx.Appcompat.App;
using Androidx.Appcompat.Widget;
using Org.Androidannotations.Annotations;
using Org.Slf4j;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Template;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Profile
{
    /// <summary>
    /// Created by asavinova on 27/07/16.
    /// </summary>
    public class ProfileActivity : AppCompatActivity
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(ProfileActivity));
        private static readonly int PROFILE_LOADER_ID = 0;
        long profileId;
        Toolbar toolbar;
        WebView webView;
        SupportView supportView;
        FourPdaClient client;
        virtual void AfterViews()
        {
            L.Debug("Start profile activity");
            ((App)GetApplication()).Component().Inject(this);
            toolbar.SetTitle(R.@string.profile_title);
            toolbar.SetNavigationIcon(R.drawable.ic_close_white_24dp);
            toolbar.SetNavigationOnClickListener((v) => Finish());
            LoadProfile();
        }

        virtual void LoadProfile()
        {
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(PROFILE_LOADER_ID, null, new ProfileCallbacks(this)).ForceLoad();
        }

        virtual void UpdateProfile(Profile profile)
        {
            supportView.Hide();
            toolbar.SetSubtitle(profile.GetLogin());
            string formattedInfo = new ProfileTemplate().Make(profile.GetInfo());
            webView.LoadData(formattedInfo, "text/html; charset=utf-8", null);
        }
    }
}