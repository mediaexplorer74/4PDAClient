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

import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import four.pda.ui.article.NewsActivity;
import four.pda.ui.article.NewsActivity_;

import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.Espresso.pressBack;
import static androidx.test.espresso.action.ViewActions.click;
import static androidx.test.espresso.action.ViewActions.swipeDown;
import static androidx.test.espresso.action.ViewActions.swipeUp;
import static androidx.test.espresso.assertion.ViewAssertions.matches;
import static androidx.test.espresso.matcher.ViewMatchers.isClickable;
import static androidx.test.espresso.matcher.ViewMatchers.isDisplayed;
import static androidx.test.espresso.matcher.ViewMatchers.withId;

/**
 * Created by Klishin.Pavel on 08.02.2016.
 */
@RunWith(AndroidJUnit4.class)
@SmallTest
public class SoftwareActivityTest {

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
	public void softwareActivityTest() throws UiObjectNotFoundException, InterruptedException, RemoteException {

		UiObject openDrawerButton = device.findObject(new UiSelector()
				.className("android.widget.ImageButton")
				.packageName(APP_ID)
				.instance(0));
		openDrawerButton
				.click();
		device.waitForWindowUpdate(APP_ID, 100);

		onView(withId(R.id.software_category_view)).perform(click());

		UiObject openfirstButton = device.findObject(new UiSelector()
				.className("android.widget.ImageView").packageName(APP_ID)
				.resourceId(APP_ID + ":id/image_view")
				.instance(0));
		openfirstButton
				.click();

		device.waitForWindowUpdate(APP_ID, 100);

		onView(withId(R.id.comments_button))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));

		onView(withId(R.id.drawer_layout))
				.perform(swipeUp())
				.perform(swipeUp())
				.perform(swipeUp())
				.perform(swipeUp());
		pressBack();
		onView(withId(R.id.drawer_layout))
				.perform(swipeUp())
				.perform(swipeUp())
				.perform(swipeUp())
				.perform(swipeUp());
		device.waitForIdle();

		onView(withId(R.id.up_button))
				.perform(click());
		onView(withId(R.id.drawer_layout))
				.perform(swipeDown())
				.perform(swipeDown());
		device.waitForWindowUpdate("four.pda", 100);
		device.waitForIdle(150);

		UiObject openSecondButton = device.findObject(new UiSelector()
				.className("android.widget.ImageView").packageName(APP_ID)
				.resourceId(APP_ID + ":id/image_view")
				.instance(1));
		openSecondButton
				.click();

		device.waitForWindowUpdate(APP_ID, 100);

		onView(withId(R.id.comments_button))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));

		onView(withId(R.id.drawer_layout))
				.perform(swipeUp())
				.perform(swipeUp())
				.perform(swipeUp());
		pressBack();
	}

}

