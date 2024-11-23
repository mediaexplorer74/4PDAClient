package four.pda.ui;

import android.text.Html;
import android.text.method.LinkMovementMethod;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EActivity;
import org.androidannotations.annotations.ViewById;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.BuildConfig;
import four.pda.R;


/**
 * Created by asavinova on 15/04/15.
 */
@EActivity(R.layout.activity_about)
public class AboutActivity extends AppCompatActivity {

	private static final Logger L = LoggerFactory.getLogger(AboutActivity.class);

	@ViewById Toolbar toolbar;

	@ViewById TextView descriptionTextView;

	@ViewById TextView versionTextView;
	@ViewById TextView buildNumberTextView;
	@ViewById TextView buildTypeTextView;
	@ViewById TextView vcsBranchTextView;
	@ViewById TextView vcsCommitTextView;

	@ViewById(R.id.swapi_4pda) TextView userSwapi;
	@ViewById(R.id.varann_4pda) TextView userVarann;

	@AfterViews
	void afterViews() {
		L.debug("Start about activity");

		toolbar.setTitle(R.string.about);
		setSupportActionBar(toolbar);
		getSupportActionBar().setDisplayHomeAsUpEnabled(true);


		versionTextView.setText(getString(R.string.about_version, BuildConfig.VERSION_NAME));
		buildNumberTextView.setText(getString(R.string.about_buildNumber, BuildConfig.VERSION_CODE));
		buildTypeTextView.setText(getString(R.string.about_buildType, BuildConfig.BUILD_TYPE.toLowerCase()));
		vcsBranchTextView.setText(getString(R.string.about_vcsBranch, BuildConfig.VCS_BRANCH));
		vcsCommitTextView.setText(getString(R.string.about_vcsCommit, BuildConfig.VCS_COMMIT));

		html(descriptionTextView, R.string.about_description);
		html(userSwapi, R.string.about_swapi_4pda);
		html(userVarann, R.string.about_varann_4pda);
	}

	private void html(TextView view, int resId) {
		view.setText(Html.fromHtml(getString(resId)));
		view.setMovementMethod(LinkMovementMethod.getInstance());
	}

}
