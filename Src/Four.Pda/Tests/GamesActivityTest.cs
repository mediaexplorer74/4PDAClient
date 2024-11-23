using Android.Os;
using Androidx.Test;
using Androidx.Test.Filters;
using Androidx.Test.Rule;
using Androidx.Test.Runner;
using Androidx.Test.Uiautomator;
using Org.Junit;
using Org.Junit.Runner;
using Four.Pda.Ui.Article;
using Androidx.Test.Espresso.Assertion.ViewAssertions;
using Androidx.Test.Espresso.Espresso;
using Androidx.Test.Espresso.Action.ViewActions;
using Androidx.Test.Espresso.Matcher.ViewMatchers;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by Klishin.Pavel on 08.02.2016.
    /// </summary>
    public class GamesActivityTest
    {
        private UiDevice device;
        static readonly string APP_ID = BuildConfig.APPLICATION_ID;
        public ActivityTestRule<NewsActivity> activityTestRule = new ActivityTestRule(typeof(NewsActivity_));
        public virtual void StartMainActivityFromHomeScreen()
        {

            // Initialize UiDevice instance
            device = UiDevice.GetInstance(InstrumentationRegistry.GetInstrumentation());
            device.WakeUp();
        }

        public virtual void GamesActivityTest()
        {
            UiObject openDrawerButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageButton").PackageName(APP_ID).Instance(0));
            openDrawerButton.Click();
            device.WaitForWindowUpdate(APP_ID, 100);
            OnView(WithId(R.id.games_category_view)).Perform(Click());
            device.WaitForIdle();
            UiObject openfirstButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageView").PackageName(APP_ID).ResourceId(APP_ID + ":id/image_view").Instance(0));
            openfirstButton.Click();
            device.WaitForWindowUpdate(APP_ID, 100);
            OnView(WithId(R.id.comments_button)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp());
            PressBack();
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp());
            device.WaitForIdle();
            OnView(WithId(R.id.up_button)).Perform(Click());
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeDown()).Perform(SwipeDown());
            device.WaitForWindowUpdate(APP_ID, 100);
            device.WaitForIdle(150);
            UiObject openSecondButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageView").PackageName(APP_ID).ResourceId(APP_ID + ":id/image_view").Instance(1));
            openSecondButton.Click();
            device.WaitForWindowUpdate(APP_ID, 100);
            OnView(WithId(R.id.comments_button)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.drawer_layout)).Perform(SwipeUp()).Perform(SwipeUp()).Perform(SwipeUp());
            PressBack();
        }
    }
}