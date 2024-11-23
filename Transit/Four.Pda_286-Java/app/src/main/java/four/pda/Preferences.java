package four.pda;

import org.androidannotations.annotations.sharedpreferences.DefaultBoolean;
import org.androidannotations.annotations.sharedpreferences.DefaultInt;
import org.androidannotations.annotations.sharedpreferences.SharedPref;

/**
 * Created by asavinova on 13/04/15.
 */
@SharedPref(SharedPref.Scope.UNIQUE)
public interface Preferences {

	@DefaultBoolean(true)
	boolean isFirstRun();

	long profileId();
	String profileLogin();
	String profilePhoto();

	@DefaultInt(100)
	int textZoom();

	boolean isAcceptedCommentRules();

}
