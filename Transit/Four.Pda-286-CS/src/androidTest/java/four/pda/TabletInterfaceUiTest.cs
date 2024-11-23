using Android.Os;
using Androidx.Test;
using Androidx.Test.Filters;
using Androidx.Test.Rule;
using Androidx.Test.Runner;
using Androidx.Test.Uiautomator;
using Org.Junit;
using Org.Junit.Runner;
using Four.Pda.Ui.Article;
using Androidx.Test.Espresso.Espresso;
using Androidx.Test.Espresso.Action.ViewActions;
using Androidx.Test.Espresso.Assertion.ViewAssertions;
using Androidx.Test.Espresso.Matcher.ViewMatchers;
using System.Diagnostics;

namespace Four.Pda
{
    public class TabletInterfaceUiTest
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

        public virtual void TabletInterfaceTest()
        {
            device.WaitForIdle();
            device.SetOrientationRight();
            device.WaitForWindowUpdate(APP_ID, 100);
            OnView(WithId(R.id.all_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.news_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.articles_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.reviews_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.software_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.games_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.about_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.login_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.toolbar)).Check(Matches(IsDisplayed()));
        }

        public virtual void TabletInterfaceDrawerSwipeTest()
        {
            TabletInterfaceTest();
            for (int i = 0; i <= 41; i++)
            {
                OnView(WithId(R.id.recycler_view)).Perform(SwipeUp());
                if (i == 10)
                    device.SetOrientationNatural();
                if (i == 20)
                    device.SetOrientationLeft();
                if (i == 30)
                    device.SetOrientationRight();
                if (i == 40)
                    device.SetOrientationNatural();
            }
        }
    }
}