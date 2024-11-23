using Android.Os;
using Androidx.Test;
using Androidx.Test.Filters;
using Androidx.Test.Rule;
using Androidx.Test.Runner;
using Androidx.Test.Uiautomator;
using Org.Junit;
using Org.Junit.Runner;
using Four.Pda.Ui.Article;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by Klishin Pavel on 06.04.2016.
    /// </summary>
    public class TiltInterfaceTest
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

        public virtual void TiltDevInterfaceTest()
        {
            device.WaitForIdle();
            device.SetOrientationRight();
            UiObject openDrawerButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageButton").PackageName(APP_ID).Instance(0));
            openDrawerButton.Click();
            UiObject scrollView = device.FindObject(new UiSelector().ClassName("android.widget.ScrollView"));
            scrollView.SwipeUp(3);
            device.WaitForIdle();
            scrollView.SwipeDown(3);
            device.WaitForIdle();
            device.SetOrientationLeft();
            openDrawerButton.Click();
            device.WaitForIdle();
            scrollView.SwipeDown(3);
            device.WaitForIdle();
            device.SetOrientationNatural();
            openDrawerButton.Click();
            device.WaitForIdle();
            scrollView.SwipeDown(3);
            scrollView.SwipeUp(3);
            device.WaitForIdle();
            device.SetOrientationLeft();
            openDrawerButton.Click();
            device.SetOrientationRight();
            device.SetOrientationNatural();
            device.SetOrientationLeft();
            device.SetOrientationRight();
        }
    }
}