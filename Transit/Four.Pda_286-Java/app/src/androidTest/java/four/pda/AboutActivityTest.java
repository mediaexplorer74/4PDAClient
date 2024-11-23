package four.pda;

import android.os.Environment;
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

import java.io.File;

import four.pda.ui.AboutActivity;
import four.pda.ui.AboutActivity_;

import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.assertion.ViewAssertions.matches;
import static androidx.test.espresso.matcher.ViewMatchers.isClickable;
import static androidx.test.espresso.matcher.ViewMatchers.isDisplayed;
import static androidx.test.espresso.matcher.ViewMatchers.withId;
import static androidx.test.espresso.matcher.ViewMatchers.withText;

/**
 * Created by Klishin.Pavel on 08.02.2016.
 */
@SmallTest
@RunWith(AndroidJUnit4.class)
public class AboutActivityTest {

	private UiDevice device;
	static final String APP_ID = BuildConfig.APPLICATION_ID;
	static final String WORKING_DIR = Environment.getExternalStorageDirectory().getAbsolutePath();

	@Rule
	public ActivityTestRule<AboutActivity> activityTestRule = new ActivityTestRule(AboutActivity_.class);

	@Before
	public void startMainActivityFromHomeScreen() throws RemoteException {
		// Initialize UiDevice instance
		device = UiDevice.getInstance(InstrumentationRegistry.getInstrumentation());
		device.wakeUp();
	}

	@Test
	public void elementsPresented() throws UiObjectNotFoundException {

		device.waitForWindowUpdate(APP_ID, 100);
		if (APP_ID.matches("four.pda.debug")) {
			device.takeScreenshot(new File(WORKING_DIR + "/screenFour.png"));
		}

		//Проверяем наличие всех элементов
		onView(withId(R.id.description_text_view))
				.check(matches(isDisplayed()));

		onView(withId(R.id.version_text_view))
				.check(matches(isDisplayed()));

		onView(withId(R.id.build_number_text_view))
				.check(matches(isDisplayed()));

		onView(withId(R.id.build_type_text_view))
				.check(matches(isDisplayed()));

		//Свайпаем вверх, чтобы увидеть остальные элементы
		UiObject aboutActivityField = device.findObject(new UiSelector()
				.className("android.widget.LinearLayout")
				.packageName(APP_ID));
		aboutActivityField.swipeUp(2);
		onView(withText("swapii@gmail.com"))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withText("varann@gmail.com"))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.swapi_4pda))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.varann_4pda))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
	}

}
