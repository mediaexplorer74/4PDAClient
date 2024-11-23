using Android.Content;
using Android.Util;
using Android.View;
using Androidx.Viewpager.Widget;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    public class SafeViewPager : ViewPager
    {
        public SafeViewPager(Context context) : base(context)
        {
        }

        public SafeViewPager(Context context, AttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            try
            {
                return base.OnTouchEvent(ev);
            }
            catch (ArgumentException ex)
            {
                ex.PrintStackTrace();
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            try
            {
                return base.OnInterceptTouchEvent(ev);
            }
            catch (ArgumentException ex)
            {
                ex.PrintStackTrace();
            }

            return false;
        }
    }
}