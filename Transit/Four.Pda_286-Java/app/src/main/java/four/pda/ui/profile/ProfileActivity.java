package four.pda.ui.profile;

import android.webkit.WebView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EActivity;
import org.androidannotations.annotations.Extra;
import org.androidannotations.annotations.ViewById;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.inject.Inject;

import four.pda.App;
import four.pda.R;
import four.pda.client.FourPdaClient;
import four.pda.client.model.Profile;
import four.pda.template.ProfileTemplate;
import four.pda.ui.SupportView;

/**
 * Created by asavinova on 27/07/16.
 */
@EActivity(R.layout.activity_profile)
public class ProfileActivity extends AppCompatActivity {

	private static final Logger L = LoggerFactory.getLogger(ProfileActivity.class);

	private static final int PROFILE_LOADER_ID = 0;

	@Extra long profileId;

	@ViewById Toolbar toolbar;
	@ViewById WebView webView;
	@ViewById SupportView supportView;

	@Inject FourPdaClient client;

	@AfterViews
	void afterViews() {
		L.debug("Start profile activity");

		((App) getApplication()).component().inject(this);

		toolbar.setTitle(R.string.profile_title);
		toolbar.setNavigationIcon(R.drawable.ic_close_white_24dp);
		toolbar.setNavigationOnClickListener(v -> finish());

		loadProfile();
	}

	void loadProfile() {
		supportView.showProgress();
		getLoaderManager().restartLoader(PROFILE_LOADER_ID, null, new ProfileCallbacks(this)).forceLoad();
	}

	void updateProfile(Profile profile) {
		supportView.hide();
		toolbar.setSubtitle(profile.getLogin());
		String formattedInfo = new ProfileTemplate().make(profile.getInfo());
		webView.loadData(formattedInfo, "text/html; charset=utf-8", null);
	}
}
