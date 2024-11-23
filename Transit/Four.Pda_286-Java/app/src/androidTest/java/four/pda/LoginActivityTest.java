package four.pda;

import android.os.RemoteException;
import androidx.test.InstrumentationRegistry;
import androidx.test.filters.SmallTest;
import androidx.test.rule.ActivityTestRule;
import androidx.test.runner.AndroidJUnit4;
import androidx.test.uiautomator.UiDevice;
import androidx.test.uiautomator.UiObject;
import androidx.test.uiautomator.UiObjectNotFoundException;
import androidx.test.uiautomator.UiSelector;

import junit.framework.Assert;

import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import four.pda.ui.article.NewsActivity;
import four.pda.ui.article.NewsActivity_;

import static androidx.test.espresso.Espresso.closeSoftKeyboard;
import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.action.ViewActions.click;
import static androidx.test.espresso.matcher.ViewMatchers.withId;


/**
 * Created by Seva Powerman on 27.02.2016.
 */
@RunWith(AndroidJUnit4.class)
@SmallTest
public class LoginActivityTest {

	private UiDevice device;
	static final String APP_ID = BuildConfig.APPLICATION_ID;

	@Rule
	public ActivityTestRule<NewsActivity> activityTestRule = new ActivityTestRule(NewsActivity_.class);

	@Before
	public void startMainActivityFromHomeScreen() throws RemoteException {
		// Initialize UiDevice instance
		device = UiDevice.getInstance(InstrumentationRegistry.getInstrumentation());
		device.wakeUp();
	}

	@Test
	public void loginActivityTest() throws UiObjectNotFoundException, InterruptedException {

		UiObject openDrawerButton = device.findObject(new UiSelector()
				.className("android.widget.ImageButton")
				.packageName(APP_ID)
				.instance(0));
		openDrawerButton
				.click();
		device.waitForWindowUpdate(APP_ID, 100);

		onView(withId(R.id.login_view)).perform(click());
		device.waitForIdle();

		closeSoftKeyboard();
		device.waitForIdle();

		UiObject loginField = device.findObject(new UiSelector()
				.className("android.widget.EditText")
				.resourceId(APP_ID + ":id/login_view"));
		Assert.assertTrue(loginField.exists());

		UiObject passwordField = device.findObject(new UiSelector()
				.className("android.widget.EditText")
				.resourceId(APP_ID + ":id/password_view"));
		Assert.assertTrue(passwordField.exists());

		UiObject capchaPic = device.findObject(new UiSelector()
				.className("android.widget.ImageView")
				.resourceId(APP_ID + ":id/captcha_image_view"));
		Assert.assertTrue(capchaPic.exists());

		UiObject capchaText = device.findObject(new UiSelector()
				.className("android.widget.EditText")
				.resourceId(APP_ID + ":id/captcha_text_view"));
		Assert.assertTrue(capchaText.exists());

		UiObject capchaEnterButton = device.findObject(new UiSelector()
				.className("android.widget.Button")
				.resourceId(APP_ID + ":id/enter_view"));
		Assert.assertTrue(capchaEnterButton.exists());
	}
}
