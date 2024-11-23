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
using Androidx.Test.Espresso.Assertion.ViewAssertions;
using Androidx.Test.Espresso.Matcher.ViewMatchers;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by Klishin.Pavel on 01.02.2016.
    /// </summary>
    public class BaseFragmentTest
    {
        private UiDevice device;
        static readonly string APP_ID = BuildConfig.APPLICATION_ID;
        //TODO Add SLF4J logging
        //TODO Add Screenshots
        public ActivityTestRule<NewsActivity> activityTestRule = new ActivityTestRule(typeof(NewsActivity_));
        public virtual void StartMainActivityFromHomeScreen()
        {

            // Initialize UiDevice instance
            device = UiDevice.GetInstance(InstrumentationRegistry.GetInstrumentation());
            device.WakeUp();
        }

        public virtual void MainDrawerActivityTest()
        {

            //Открываем Navigaton Drawer методами UiAutomator:
            UiObject openDrawerButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageButton").PackageName(APP_ID).Instance(0));
            openDrawerButton.Click();

            //Убеждаемся, что все пункты в наличии через Espresso:
            OnView(WithId(R.id.all_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.news_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.articles_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.reviews_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.software_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.games_category_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.about_view)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
        }
    }
}