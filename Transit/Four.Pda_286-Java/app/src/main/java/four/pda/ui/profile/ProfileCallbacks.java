package four.pda.ui.profile;

import android.app.LoaderManager;
import android.content.AsyncTaskLoader;
import android.content.Loader;
import android.os.Bundle;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.R;
import four.pda.client.FourPdaClient;
import four.pda.client.model.Profile;
import four.pda.ui.LoadResult;

/**
 * Created by asavinova on 27/07/16.
 */
public class ProfileCallbacks implements LoaderManager.LoaderCallbacks<LoadResult<Profile>> {

	private static final Logger L = LoggerFactory.getLogger(ProfileCallbacks.class);

	private ProfileActivity activity;
	private FourPdaClient pdaClient;

	public ProfileCallbacks(ProfileActivity activity) {
		this.activity = activity;
		pdaClient = activity.client;
	}

	@Override
	public Loader<LoadResult<Profile>> onCreateLoader(int id, Bundle args) {
		return new AsyncTaskLoader<LoadResult<Profile>>(activity) {
			@Override
			public LoadResult<Profile> loadInBackground() {
				try {
					return new LoadResult<>(pdaClient.getProfile(activity.profileId));
				} catch (Exception e) {
					L.error("Profile request error", e);
					return new LoadResult<>(e);
				}
			}
		};
	}

	@Override
	public void onLoadFinished(Loader<LoadResult<Profile>> loader, LoadResult<Profile> result) {

		if (result.isError()) {
			activity.supportView.showError(activity.getString(R.string.profile_network_error), v -> activity.loadProfile());
			return;
		}

		activity.updateProfile(result.getData());
	}

	@Override
	public void onLoaderReset(Loader<LoadResult<Profile>> loader) {
	}

}
