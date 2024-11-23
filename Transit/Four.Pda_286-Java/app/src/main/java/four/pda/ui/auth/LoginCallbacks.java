package four.pda.ui.auth;

import android.app.LoaderManager;
import android.content.AsyncTaskLoader;
import android.content.Loader;
import android.os.Bundle;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.R;
import four.pda.client.LoginParams;
import four.pda.client.exceptions.LoginException;
import four.pda.ui.LoadResult;

/**
 * Created by asavinova on 21/02/16.
 */
class LoginCallbacks implements LoaderManager.LoaderCallbacks<LoadResult<Long>> {

	private static final Logger L = LoggerFactory.getLogger(LoginCallbacks.class);

	private AuthActivity activity;

	public LoginCallbacks(AuthActivity activity) {
		this.activity = activity;
	}

	@Override
	public Loader<LoadResult<Long>> onCreateLoader(int id, Bundle args) {
		return new AsyncTaskLoader<LoadResult<Long>>(activity) {
			@Override
			public LoadResult<Long> loadInBackground() {
				try {
					LoginParams params = new LoginParams();
					params.setLogin(activity.loginView.getText().toString());
					params.setPassword(activity.passwordView.getText().toString());
					params.setCaptcha(activity.captchaTextView.getText().toString());
					if (activity.captcha != null) {
						params.setCaptchaSig(activity.captcha.getSig());
						params.setCaptchaTime(activity.captcha.getTime());
					}
					return new LoadResult<>(activity.client.login(params));
				} catch (Exception e) {
					L.error("Login request error", e);
					return new LoadResult<>(e);
				}
			}
		};
	}

	@Override
	public void onLoadFinished(Loader<LoadResult<Long>> loader, LoadResult<Long> result) {

		if (result.isError()) {

			if (result.getException() instanceof LoginException) {
				LoginException exception = (LoginException) result.getException();
				StringBuilder errors = new StringBuilder();
				for (String e : exception.getErrors()) {
					errors.append(e);
					errors.append(" ");
				}
				activity.supportView.showError(errors.toString().trim(), v -> activity.loadCaptcha());
				return;
			}

			activity.supportView.showError(activity.getString(R.string.auth_network_error), v -> activity.signIn());
			return;
		}

		Long memberId = result.getData();
		activity.auth.login(memberId);
		activity.loadProfile();
		activity.supportView.hide();
	}

	@Override
	public void onLoaderReset(Loader<LoadResult<Long>> loader) {
	}

}
