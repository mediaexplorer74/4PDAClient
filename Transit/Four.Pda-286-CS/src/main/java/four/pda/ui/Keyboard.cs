using Android.App;
using Android.Content;
using Android.View;
using Android.View.Inputmethod;
using Javax.Inject;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by pavel on 21/05/16.
    /// </summary>
    public class Keyboard
    {
        private InputMethodManager inputMethodManager;
        public Keyboard(Context context)
        {
            this.inputMethodManager = ((InputMethodManager)context.GetSystemService(Context.INPUT_METHOD_SERVICE));
        }

        public virtual void ShowFor(View view)
        {
            view.RequestFocus();
            view.PostDelayed(() => inputMethodManager.ShowSoftInput(view, 0), 100);
        }

        public virtual void Toggle(View view)
        {
            view.PostDelayed(() => inputMethodManager.ToggleSoftInput(InputMethodManager.SHOW_IMPLICIT, 0), 100);
        }

        public virtual void Hide(Activity activity)
        {
            inputMethodManager.HideSoftInputFromWindow(activity.GetWindow().GetDecorView().GetRootView().GetWindowToken(), 0);
        }
    }
}