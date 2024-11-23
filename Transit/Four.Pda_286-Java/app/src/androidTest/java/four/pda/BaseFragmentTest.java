package four.pda;

import android.os.RemoteException;
import androidx.test.InstrumentationRegistry;
import androidx.test.filters.LargeTest;
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
import static androidx.test.espresso.assertion.ViewAssertions.matches;
import static androidx.test.espresso.matcher.ViewMatchers.isClickable;
import static androidx.test.espresso.matcher.ViewMatchers.isDisplayed;
import static androidx.test.espresso.matcher.ViewMatchers.withId;

/**
 * Created by Klishin.Pavel on 01.02.2016.
 */
@RunWith(AndroidJUnit4.class)
@LargeTest
public class BaseFragmentTest {

	private UiDevice device;
	static final String APP_ID = BuildConfig.APPLICATION_ID;
	//TODO Add SLF4J logging
	//TODO Add Screenshots
	@Rule
	public ActivityTestRule<NewsActivity> activityTestRule = new ActivityTestRule(NewsActivity_.class);

	@Before
	public void startMainActivityFromHomeScreen() throws RemoteException {
		// Initialize UiDevice instance
		device = UiDevice.getInstance(InstrumentationRegistry.getInstrumentation());
		device.wakeUp();

	}

	@Test
	public void mainDrawerActivityTest() throws UiObjectNotFoundException, InterruptedException {

		//Открываем Navigaton Drawer методами UiAutomator:
		UiObject openDrawerButton = device.findObject(new UiSelector()
				.className("android.widget.ImageButton")
				.packageName(APP_ID)
				.instance(0));
		openDrawerButton.click();
		//Убеждаемся, что все пункты в наличии через Espresso:
		onView(withId(R.id.all_category_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.news_category_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.articles_category_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.reviews_category_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.software_category_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.games_category_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		onView(withId(R.id.about_view))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
	}
}
