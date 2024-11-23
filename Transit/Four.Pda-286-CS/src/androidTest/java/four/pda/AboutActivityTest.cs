using Android.Os;
using Androidx.Test;
using Androidx.Test.Filters;
using Androidx.Test.Rule;
using Androidx.Test.Runner;
using Androidx.Test.Uiautomator;
using Org.Junit;
using Org.Junit.Runner;
using Java.Io;
using Four.Pda.Ui;
using Androidx.Test.Espresso.Espresso;
using Androidx.Test.Espresso.Assertion.ViewAssertions;
using Androidx.Test.Espresso.Matcher.ViewMatchers;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by Klishin.Pavel on 08.02.2016.
    /// </summary>
    public class AboutActivityTest
    {
        private UiDevice device;
        static readonly string APP_ID = BuildConfig.APPLICATION_ID;
        static readonly string WORKING_DIR = Environment.GetExternalStorageDirectory().GetAbsolutePath();
        public ActivityTestRule<AboutActivity> activityTestRule = new ActivityTestRule(typeof(AboutActivity_));
        public virtual void StartMainActivityFromHomeScreen()
        {

            // Initialize UiDevice instance
            device = UiDevice.GetInstance(InstrumentationRegistry.GetInstrumentation());
            device.WakeUp();
        }

        public virtual void ElementsPresented()
        {
            device.WaitForWindowUpdate(APP_ID, 100);
            if (APP_ID.Matches("four.pda.debug"))
            {
                device.TakeScreenshot(new File(WORKING_DIR + "/screenFour.png"));
            }


            //Проверяем наличие всех элементов
            OnView(WithId(R.id.description_text_view)).Check(Matches(IsDisplayed()));
            OnView(WithId(R.id.version_text_view)).Check(Matches(IsDisplayed()));
            OnView(WithId(R.id.build_number_text_view)).Check(Matches(IsDisplayed()));
            OnView(WithId(R.id.build_type_text_view)).Check(Matches(IsDisplayed()));

            //Свайпаем вверх, чтобы увидеть остальные элементы
            UiObject aboutActivityField = device.FindObject(new UiSelector().ClassName("android.widget.LinearLayout").PackageName(APP_ID));
            aboutActivityField.SwipeUp(2);
            OnView(WithText("swapii@gmail.com")).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithText("varann@gmail.com")).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.swapi_4pda)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
            OnView(WithId(R.id.varann_4pda)).Check(Matches(IsDisplayed())).Check(Matches(IsClickable()));
        }
    }
}