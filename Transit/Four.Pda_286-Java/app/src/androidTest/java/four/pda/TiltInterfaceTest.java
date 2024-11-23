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

/**
 * Created by Klishin Pavel on 06.04.2016.
 */
@RunWith(AndroidJUnit4.class)
@SmallTest
public class TiltInterfaceTest {

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
	public void tiltDevInterfaceTest() throws RemoteException, UiObjectNotFoundException {
		device.waitForIdle();
		device.setOrientationRight();

		UiObject openDrawerButton = device.findObject(new UiSelector()
				.className("android.widget.ImageButton")
				.packageName(APP_ID)
				.instance(0));
		openDrawerButton.click();

		UiObject scrollView = device.findObject(new UiSelector()
				.className("android.widget.ScrollView"));
		scrollView.swipeUp(3);

		device.waitForIdle();
		scrollView.swipeDown(3);
		device.waitForIdle();

		device.setOrientationLeft();
		openDrawerButton.click();

		device.waitForIdle();
		scrollView.swipeDown(3);
		device.waitForIdle();

		device.setOrientationNatural();
		openDrawerButton.click();
		device.waitForIdle();
		scrollView.swipeDown(3);
		scrollView.swipeUp(3);

		device.waitForIdle();
		device.setOrientationLeft();
		openDrawerButton.click();
		device.setOrientationRight();
		device.setOrientationNatural();
		device.setOrientationLeft();
		device.setOrientationRight();
	}
}
