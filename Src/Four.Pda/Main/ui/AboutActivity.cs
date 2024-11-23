using Android.Text;
using Android.Text.Method;
using Android.Widget;
using Androidx.Appcompat.App;
using Androidx.Appcompat.Widget;
using Org.Androidannotations.Annotations;
using Org.Slf4j;
using Four.Pda;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by asavinova on 15/04/15.
    /// </summary>
    public class AboutActivity : AppCompatActivity
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(AboutActivity));
        Toolbar toolbar;
        TextView descriptionTextView;
        TextView versionTextView;
        TextView buildNumberTextView;
        TextView buildTypeTextView;
        TextView vcsBranchTextView;
        TextView vcsCommitTextView;
        TextView userSwapi;
        TextView userVarann;
        virtual void AfterViews()
        {
            L.Debug("Start about activity");
            toolbar.SetTitle(R.@string.about);
            SetSupportActionBar(toolbar);
            GetSupportActionBar().SetDisplayHomeAsUpEnabled(true);
            versionTextView.SetText(GetString(R.@string.about_version, BuildConfig.VERSION_NAME));
            buildNumberTextView.SetText(GetString(R.@string.about_buildNumber, BuildConfig.VERSION_CODE));
            buildTypeTextView.SetText(GetString(R.@string.about_buildType, BuildConfig.BUILD_TYPE.ToLowerCase()));
            vcsBranchTextView.SetText(GetString(R.@string.about_vcsBranch, BuildConfig.VCS_BRANCH));
            vcsCommitTextView.SetText(GetString(R.@string.about_vcsCommit, BuildConfig.VCS_COMMIT));
            Html(descriptionTextView, R.@string.about_description);
            Html(userSwapi, R.@string.about_swapi_4pda);
            Html(userVarann, R.@string.about_varann_4pda);
        }

        private void Html(TextView view, int resId)
        {
            view.SetText(Html.FromHtml(GetString(resId)));
            view.SetMovementMethod(LinkMovementMethod.GetInstance());
        }
    }
}