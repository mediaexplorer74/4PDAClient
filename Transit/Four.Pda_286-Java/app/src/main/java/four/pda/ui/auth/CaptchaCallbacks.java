package four.pda.ui.auth;

import android.app.LoaderManager;
import android.content.AsyncTaskLoader;
import android.content.Loader;
import android.os.Bundle;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.R;
import four.pda.client.model.Captcha;
import four.pda.ui.Images;
import four.pda.ui.LoadResult;

/**
 * Created by asavinova on 21/02/16.
 */
class CaptchaCallbacks implements LoaderManager.LoaderCallbacks<LoadResult<Captcha>> {

	private static final Logger L = LoggerFactory.getLogger(CaptchaCallbacks.class);

	private AuthActivity activity;

	public CaptchaCallbacks(AuthActivity activity) {
		this.activity = activity;
	}

	@Override
	public Loader<LoadResult<Captcha>> onCreateLoader(int id, Bundle args) {
		return new AsyncTaskLoader<LoadResult<Captcha>>(activity) {
			@Override
			public LoadResult<Captcha> loadInBackground() {
				try {
					return new LoadResult<>(activity.client.getCaptcha());
				} catch (Exception e) {
					L.error("Captcha request error", e);
					return new LoadResult<>(e);
				}
			}
		};
	}

	@Override
	public void onLoadFinished(Loader<LoadResult<Captcha>> loader, LoadResult<Captcha> result) {
		activity.captchaTextView.setText("");

		if (result.isError()) {
			activity.supportView.showError(activity.getString(R.string.auth_network_error), v -> activity.loadCaptcha());
			return;
		}

		activity.captcha = result.getData();
		Images.load(activity.captchaImageView, activity.captcha.getUrl());
		activity.supportView.hide();
	}

	@Override
	public void onLoaderReset(Loader<LoadResult<Captcha>> loader) {
	}

}
