using Android.Os;
using Androidx.Test;
using Androidx.Test.Filters;
using Androidx.Test.Rule;
using Androidx.Test.Runner;
using Androidx.Test.Uiautomator;
using Org.Junit;
using Org.Junit.Runner;
using Java.Io;
using Four.Pda.Ui.Article;
using Androidx.Test.Espresso.Espresso;
using Androidx.Test.Espresso.Action.ViewActions;
using Androidx.Test.Espresso.Assertion.ViewAssertions;
using Androidx.Test.Espresso.Matcher.ViewMatchers;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by Klishin.Pavel on 08.02.2016.
    /// </summary>
    public class AllArticlesTest
    {
        private UiDevice device;
        static readonly string APP_ID = BuildConfig.APPLICATION_ID;
        static readonly string WORKING_DIR = Environment.GetExternalStorageDirectory().GetAbsolutePath();
        public ActivityTestRule<NewsActivity> activityTestRule = new ActivityTestRule(typeof(NewsActivity_));
        public virtual void StartMainActivityFromHomeScreen()
        {

            // Initialize UiDevice instance
            device = UiDevice.GetInstance(InstrumentationRegistry.GetInstrumentation());
            device.WakeUp();
        }

        public virtual void AllArticlesActivityTest()
        {

            //Открываем Navigaton Drawer методами UiAutomator:
            UiObject openDrawerButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageButton").PackageName(APP_ID).Instance(0));
            openDrawerButton.Click();
            device.WaitForWindowUpdate(APP_ID, 300);

            //Делаем первый скриншот
            device.TakeScreenshot(new File(WORKING_DIR + "/screenOne.png"));

            //Открываем категорию "Новости"
            OnView(WithId(R.id.all_category_view)).Perform(Click());
            device.WaitForWindowUpdate(APP_ID, 300);

            //Делаем второй скриншот
            device.TakeScreenshot(new File(WORKING_DIR + "/screenTwo.png"));

            //Открываем первую статью из списка
            UiObject openfirstButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageView").PackageName(APP_ID).ResourceId(APP_ID + ":id/image_view").Instance(0));
            openfirstButton.Click();

            //Ждем пока окошко загрузиться
            device.WaitForWindowUpdate(APP_ID, 100);

            //Убеждаемся что кнопка "комментарии" есть на экране
            OnView(WithId(R.id.comments_button)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));

            //Свайпаем туда-сюда
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp());
            PressBack();
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp());
            device.WaitForIdle();

            //Нажимаем на Floating Button для возврата в начало списка
            OnView(WithId(R.id.up_button)).Perform(Click());
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeDown()).Perform(SwipeDown());
            device.WaitForWindowUpdate(APP_ID, 100);
            device.WaitForIdle(150);

            //Открываем вторую статью
            UiObject openSecondButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageView").PackageName(APP_ID).ResourceId(APP_ID + ":id/image_view").Instance(1));
            openSecondButton.Click();

            //Ждем пока окошко загрузиться
            device.WaitForWindowUpdate(APP_ID, 100);

            //Делаем третий скриншот
            device.WaitForWindowUpdate(APP_ID, 200);
            device.TakeScreenshot(new File(WORKING_DIR + "/screenThree.png"));

            //Убеждаемся что кнопка "комментарии" есть на экране
            OnView(WithId(R.id.comments_button)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));

            //Свайпаем туда-сюда
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp());

            //Возвращаемся назад
            PressBack();
        }
    }
}