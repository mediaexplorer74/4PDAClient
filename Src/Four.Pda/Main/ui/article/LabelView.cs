using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.View;
using Android.Widget;
using Org.Apache.Commons.Lang3;
using System.Diagnostics;

namespace Four.Pda.Ui.Article
{
    /// <remarks>@authorPavel Savinov (swapii@gmail.com)</remarks>
    public class LabelView : TextView
    {
        public LabelView(Context context) : base(context)
        {
            Init();
        }

        public LabelView(Context context, AttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public LabelView(Context context, AttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        private void Init()
        {
            float density = GetResources().GetDisplayMetrics().density;
            int verticalPadding = (int)(4 * density);
            int horizontalPadding = (int)(8 * density);
            SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            SetTextColor(Color.WHITE);
        }

        public virtual void SetLabel(string name, string color)
        {
            bool isVisible = StringUtils.IsNotBlank(name);
            SetVisibility(isVisible ? View.VISIBLE : View.GONE);
            if (!isVisible)
            {
                return;
            }

            SetText(name);
            SetBackgroundColor(LabelColor.GetColorValueByName(color));
        }
    }
}