using Android.Content;
using Android.Util;
using Android.View;
using Android.Widget;
using Org.Androidannotations.Annotations;
using Org.Androidannotations.Annotations.Sharedpreferences;
using Four.Pda;
using Four.Pda.Analytics;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.One
{
    /// <summary>
    /// Created by asavinova on 25/03/16.
    /// </summary>
    public class TextZoomPanel : LinearLayout
    {
        private static readonly int MINIMUM = 10;
        private static readonly int MAXIMUM = 500;
        TextView textSizeView;
        View decreaseButton;
        View increaseButton;
        EventBus eventBus;
        Preferences_ preferences;
        public TextZoomPanel(Context context) : base(context)
        {
        }

        public TextZoomPanel(Context context, AttributeSet attrs) : base(context, attrs)
        {
        }

        public TextZoomPanel(Context context, AttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public virtual void SetZoom(int zoom)
        {
            textSizeView.SetText(GetResources().GetString(R.@string.text_zoom_value, zoom));
        }

        virtual void Decrease()
        {
            int zoom = preferences.TextZoom().Get();
            Zoom(zoom - 10);
        }

        virtual void Increase()
        {
            int zoom = preferences.TextZoom().Get();
            Zoom(zoom + 10);
        }

        virtual void Dispose()
        {
            Analytics_.GetInstance_(GetContext()).Article().TextZoomSet(preferences.TextZoom().Get());
            SetVisibility(GONE);
        }

        virtual void Reset()
        {
            Zoom(100);
        }

        private void Zoom(int zoom)
        {
            if (zoom < MINIMUM)
            {
                zoom = MINIMUM;
            }

            if (zoom > MAXIMUM)
            {
                zoom = MAXIMUM;
            }

            decreaseButton.SetVisibility(zoom == MINIMUM ? INVISIBLE : VISIBLE);
            increaseButton.SetVisibility(zoom == MAXIMUM ? INVISIBLE : VISIBLE);
            SetZoom(zoom);
            preferences.TextZoom().Put(zoom);
            eventBus.Post(new SetTextZoomEvent(zoom));
        }
    }
}