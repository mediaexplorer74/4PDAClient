package four.pda.ui;

import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.inputmethod.InputMethodManager;

import javax.inject.Inject;

/**
 * Created by pavel on 21/05/16.
 */
public class Keyboard {

	private InputMethodManager inputMethodManager;

	@Inject
	public Keyboard(Context context) {
		this.inputMethodManager = ((InputMethodManager) context.getSystemService(Context.INPUT_METHOD_SERVICE));
	}

	public void showFor(final View view) {
		view.requestFocus();
		view.postDelayed(() -> inputMethodManager.showSoftInput(view, 0), 100);
	}

	public void toggle(View view) {
		view.postDelayed(() -> inputMethodManager.toggleSoftInput(InputMethodManager.SHOW_IMPLICIT, 0), 100);
	}

	public void hide(Activity activity) {
		inputMethodManager.hideSoftInputFromWindow(activity.getWindow().getDecorView().getRootView().getWindowToken(), 0);
	}

}
