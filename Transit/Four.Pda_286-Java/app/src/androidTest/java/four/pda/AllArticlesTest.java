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
public class AllArticlesTest {

	private UiDevice device;
	static final String APP_ID = BuildConfig.APPLICATION_ID;
	static final String WORKING_DIR = Environment.getExternalStorageDirectory().getAbsolutePath();

	@Rule
	public ActivityTestRule<NewsActivity> activityTestRule = new ActivityTestRule(NewsActivity_.class);

	@Before
	public void startMainActivityFromHomeScreen() throws RemoteException {
		// Initialize UiDevice instance
		device = UiDevice.getInstance(InstrumentationRegistry.getInstrumentation());
		device.wakeUp();
	}

	@Test
	public void allArticlesActivityTest() throws UiObjectNotFoundException, InterruptedException {
		//Открываем Navigaton Drawer методами UiAutomator:
		UiObject openDrawerButton = device.findObject(new UiSelector()
				.className("android.widget.ImageButton")
				.packageName(APP_ID)
				.instance(0));
		openDrawerButton.click();
		device.waitForWindowUpdate(APP_ID, 300);
		//Делаем первый скриншот
		device.takeScreenshot(new File(WORKING_DIR + "/screenOne.png"));
		//Открываем категорию "Новости"
		onView(withId(R.id.all_category_view))
				.perform(click());
		device.waitForWindowUpdate(APP_ID, 300);
		//Делаем второй скриншот
		device.takeScreenshot(new File(WORKING_DIR + "/screenTwo.png"));
		//Открываем первую статью из списка
		UiObject openfirstButton = device.findObject(new UiSelector()
				.className("android.widget.ImageView").packageName(APP_ID)
				.resourceId(APP_ID + ":id/image_view")
				.instance(0));
		openfirstButton.click();
		//Ждем пока окошко загрузиться
		device.waitForWindowUpdate(APP_ID, 100);
		//Убеждаемся что кнопка "комментарии" есть на экране
		onView(withId(R.id.comments_button))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		//Свайпаем туда-сюда
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
		//Нажимаем на Floating Button для возврата в начало списка
		onView(withId(R.id.up_button)).perform(click());
		onView(withId(R.id.drawer_layout))
				.perform(swipeDown())
				.perform(swipeDown());
		device.waitForWindowUpdate(APP_ID, 100);
		device.waitForIdle(150);
		//Открываем вторую статью
		UiObject openSecondButton = device.findObject(new UiSelector()
				.className("android.widget.ImageView").packageName(APP_ID)
				.resourceId(APP_ID + ":id/image_view")
				.instance(1));
		openSecondButton.click();
		//Ждем пока окошко загрузиться
		device.waitForWindowUpdate(APP_ID, 100);
		//Делаем третий скриншот
		device.waitForWindowUpdate(APP_ID, 200);
		device.takeScreenshot(new File(WORKING_DIR + "/screenThree.png"));
		//Убеждаемся что кнопка "комментарии" есть на экране
		onView(withId(R.id.comments_button))
				.check(matches(isDisplayed()))
				.check(matches(isClickable()));
		//Свайпаем туда-сюда
		onView(withId(R.id.drawer_layout))
				.perform(swipeUp())
				.perform(swipeUp())
				.perform(swipeUp());
		//Возвращаемся назад
		pressBack();

	}

}
