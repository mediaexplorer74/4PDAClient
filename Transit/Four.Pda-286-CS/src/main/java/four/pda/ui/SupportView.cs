using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Widget;
using Com.Rey.Material.Widget;
using Org.Androidannotations.Annotations;
using Four.Pda;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by asavinova on 06/05/15.
    /// </summary>
    public class SupportView : FrameLayout
    {
        ProgressView progressView;
        LinearLayout errorPanel;
        TextView errorMessage;
        TextView retryView;
        private int errorTextColor;
        public SupportView(Context context) : base(context)
        {
        }

        public SupportView(Context context, AttributeSet attrs) : base(context, attrs)
        {
            Init(attrs);
        }

        public SupportView(Context context, AttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(attrs);
        }

        private void Init(AttributeSet attrs)
        {
            TypedArray typedArray = GetContext().ObtainStyledAttributes(attrs, R.styleable.SupportView);
            {
                int defaultColor;
                if (android.os.Build.VERSION.SDK_INT >= 23)
                {
                    defaultColor = GetResources().GetColor(android.R.color.primary_text_light, null);
                }
                else
                {
                    defaultColor = GetResources().GetColor(android.R.color.primary_text_light);
                }

                errorTextColor = typedArray.GetColor(R.styleable.SupportView_errorTextColor, defaultColor);
            }

            typedArray.Recycle();
        }

        virtual void AfterViews()
        {
            errorMessage.SetTextColor(errorTextColor);
        }

        public virtual void ShowProgress()
        {
            errorPanel.SetVisibility(GONE);
            progressView.SetVisibility(VISIBLE);
            SetVisibility(VISIBLE);
        }

        public virtual void Hide()
        {
            errorPanel.SetVisibility(GONE);
            progressView.SetVisibility(GONE);
            SetVisibility(GONE);
        }

        public virtual void ShowError(string message, OnClickListener onClickListener)
        {
            progressView.SetVisibility(GONE);
            errorMessage.SetText(message);
            retryView.SetOnClickListener(onClickListener);
            errorPanel.SetVisibility(VISIBLE);
            SetVisibility(VISIBLE);
        }

        public virtual bool IsLoading()
        {
            return progressView.GetVisibility() == VISIBLE;
        }
    }
}