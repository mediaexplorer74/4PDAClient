using Android.Os;
using Androidx.Test;
using Androidx.Test.Filters;
using Androidx.Test.Rule;
using Androidx.Test.Runner;
using Androidx.Test.Uiautomator;
using Junit.Framework;
using Org.Junit;
using Org.Junit.Runner;
using Four.Pda.Ui.Article;
using Androidx.Test.Espresso.Espresso;
using Androidx.Test.Espresso.Action.ViewActions;
using Androidx.Test.Espresso.Matcher.ViewMatchers;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by Seva Powerman on 27.02.2016.
    /// </summary>
    public class LoginActivityTest
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

        public virtual void LoginActivityTest()
        {
            UiObject openDrawerButton = device.FindObject(new UiSelector().ClassName("android.widget.ImageButton").PackageName(APP_ID).Instance(0));
            openDrawerButton.Click();
            device.WaitForWindowUpdate(APP_ID, 100);
            OnView(WithId(R.id.login_view)).Perform(Click());
            device.WaitForIdle();
            CloseSoftKeyboard();
            device.WaitForIdle();
            UiObject loginField = device.FindObject(new UiSelector().ClassName("android.widget.EditText").ResourceId(APP_ID + ":id/login_view"));
            Assert.AssertTrue(loginField.Exists());
            UiObject passwordField = device.FindObject(new UiSelector().ClassName("android.widget.EditText").ResourceId(APP_ID + ":id/password_view"));
            Assert.AssertTrue(passwordField.Exists());
            UiObject capchaPic = device.FindObject(new UiSelector().ClassName("android.widget.ImageView").ResourceId(APP_ID + ":id/captcha_image_view"));
            Assert.AssertTrue(capchaPic.Exists());
            UiObject capchaText = device.FindObject(new UiSelector().ClassName("android.widget.EditText").ResourceId(APP_ID + ":id/captcha_text_view"));
            Assert.AssertTrue(capchaText.Exists());
            UiObject capchaEnterButton = device.FindObject(new UiSelector().ClassName("android.widget.Button").ResourceId(APP_ID + ":id/enter_view"));
            Assert.AssertTrue(capchaEnterButton.Exists());
        }
    }
}