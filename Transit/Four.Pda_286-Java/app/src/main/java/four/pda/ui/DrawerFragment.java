package four.pda.ui;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.fragment.app.Fragment;
import androidx.loader.app.LoaderManager;
import androidx.loader.content.AsyncTaskLoader;
import androidx.loader.content.Loader;

import com.franmontiel.persistentcookiejar.PersistentCookieJar;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.Click;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.OnActivityResult;
import org.androidannotations.annotations.ViewById;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.inject.Inject;

import four.pda.App;
import four.pda.Auth;
import four.pda.EventBus;
import four.pda.R;
import four.pda.analytics.Analytics;
import four.pda.client.CategoryType;
import four.pda.client.FourPdaClient;
import four.pda.client.model.Profile;
import four.pda.ui.auth.AuthActivity_;
import four.pda.ui.profile.ProfileActivity_;

/**
 * @author Anna Savinova
 * @author Pavel Savinov
 */
@EFragment(R.layout.drawer)
public class DrawerFragment extends Fragment {

	private static final Logger L = LoggerFactory.getLogger(DrawerFragment.class);

	private static final int LOGOUT_LOADER_ID = 0;
	private static final int LOGIN_REQUEST_CODE = 0;

	@ViewById View allCategoryView;
	@ViewById View newsCategoryView;
	@ViewById View reviewsCategoryView;
	@ViewById View articlesCategoryView;
	@ViewById View softwareCategoryView;
	@ViewById View gamesCategoryView;
	@ViewById View loginView;
	@ViewById View logoutView;
	@ViewById View logoView;
	@ViewById View profilePanel;
	@ViewById ImageView profilePhotoView;
	@ViewById TextView profileLoginView;

	@Bean Analytics analytics;

	@Inject Auth auth;
	@Inject EventBus eventBus;
	@Inject FourPdaClient client;
	@Inject PersistentCookieJar cookieJar;

	private List<ChangeCategoryListener> listeners = new ArrayList<>();
	private Map<View, CategoryType> map = new HashMap<>();

	public void addListener(ChangeCategoryListener listener) {
		listeners.add(listener);
	}

	public void removeListener(ChangeCategoryListener listener) {
		listeners.remove(listener);
	}

	@AfterViews
	void afterViews() {
		((App) getActivity().getApplication()).component().inject(this);

		map.put(allCategoryView, CategoryType.ALL);
		map.put(newsCategoryView, CategoryType.NEWS);
		map.put(gamesCategoryView, CategoryType.GAMES);
		map.put(reviewsCategoryView, CategoryType.REVIEWS);
		map.put(articlesCategoryView, CategoryType.ARTICLES);
		map.put(softwareCategoryView, CategoryType.SOFTWARE);

		for (View view : map.keySet()) {
			view.setOnClickListener(view1 -> {
				CategoryType category = map.get(view1);

				analytics.drawer().categoryClicked(category);

				for (ChangeCategoryListener listener : listeners) {
					listener.onChange(category);
				}
			});
		}
	}

	@Override
	public void onResume() {
		super.onResume();
		eventBus.register(this);
		updateProfile();
	}

	@Override
	public void onPause() {
		super.onPause();
		eventBus.unregister(this);
	}

	public void setCategorySelected(CategoryType categoryType) {
		for (Map.Entry<View, CategoryType> entry : map.entrySet()) {
			entry.getKey().setSelected(entry.getValue() == categoryType);
		}
	}

	public void updateProfile() {
		boolean isAuthorized = auth.isAuthorized();

		loginView.setVisibility(isAuthorized ? View.GONE : View.VISIBLE);
		logoutView.setVisibility(isAuthorized ? View.VISIBLE : View.GONE);

		logoView.setVisibility(isAuthorized ? View.GONE : View.VISIBLE);
		profilePanel.setVisibility(isAuthorized ? View.VISIBLE : View.GONE);

		if (isAuthorized) {
			Profile profile = auth.getProfile();
			profileLoginView.setText(profile.getLogin());
			Images.load(profilePhotoView, profile.getPhoto());
		}
	}

	@Click(R.id.login_view)
	void loginClicked() {
		analytics.drawer().loginClicked();
		startActivityForResult(new Intent(getActivity(), AuthActivity_.class), LOGIN_REQUEST_CODE);
	}

	@OnActivityResult(LOGIN_REQUEST_CODE)
	void onResult(int resultCode) {
		if (Activity.RESULT_OK == resultCode) {
			updateProfile();
		}
	}

	@Click(R.id.logout_view)
	void logoutClicked() {
		analytics.drawer().logoutClicked();
		getLoaderManager().restartLoader(LOGOUT_LOADER_ID, null, new LogoutCallbacks()).forceLoad();
	}

	@Click(R.id.feedback_view)
	void feedbackClicked() {
		analytics.drawer().feedbackClicked();
		Intent intent = new Intent(Intent.ACTION_SEND);
		intent.setType("text/email");
		intent.putExtra(Intent.EXTRA_EMAIL, new String[]{"4pda@gigahub.org"});
		intent.putExtra(Intent.EXTRA_SUBJECT, getString(R.string.feedback_subject));
		startActivity(Intent.createChooser(intent, getString(R.string.feedback_chooser_title)));
	}

	@Click(R.id.about_view)
	void aboutClicked() {
		analytics.drawer().aboutClicked();
		AboutActivity_.intent(getActivity()).start();
	}

	@Click(R.id.profile_panel)
	void profileClicked() {
		analytics.drawer().profileClicked();
		ProfileActivity_.intent(getActivity())
				.profileId(auth.getProfileId())
				.start();
	}

	public void onEvent(UpdateProfileEvent event) {
		updateProfile();
	}

	public interface ChangeCategoryListener {
		void onChange(CategoryType type);
	}

	class LogoutCallbacks implements LoaderManager.LoaderCallbacks<LoadResult<Boolean>> {

		@Override
		public Loader<LoadResult<Boolean>> onCreateLoader(int loaderId, final Bundle args) {
			return new AsyncTaskLoader<LoadResult<Boolean>>(getActivity()) {
				@Override
				public LoadResult<Boolean> loadInBackground() {
					try {
						return new LoadResult<>(client.logout());
					} catch (IOException e) {
						L.error("Logout request error", e);
						return new LoadResult<>(e);
					}
				}
			};
		}

		@Override
		public void onLoadFinished(Loader<LoadResult<Boolean>> loader, LoadResult<Boolean> result) {
			if (result.getException() == null && result.getData()) {
				auth.logout();
				cookieJar.clear();
			} else {
				//TODO Нужно ли запрашивать заново в случае ошибки?
			}
		}

		@Override
		public void onLoaderReset(Loader<LoadResult<Boolean>> loader) {
		}

	}
}
