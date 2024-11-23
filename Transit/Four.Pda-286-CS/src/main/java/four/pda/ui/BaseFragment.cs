using Android.View;
using Androidx.Appcompat.Widget;
using Androidx.Core.View;
using Androidx.Drawerlayout.Widget;
using Androidx.Fragment.App;
using Org.Slf4j;
using Four.Pda;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    public class BaseFragment : Fragment
    {
        protected Logger L = LoggerFactory.GetLogger(GetType());
        public override void OnResume()
        {
            base.OnResume();
            L.Debug("Resume");
        }

        public override void OnPause()
        {
            base.OnPause();
            L.Debug("Pause");
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            L.Warn("Low memory");
        }

        protected virtual void ShowMenuIcon()
        {
            View view = GetActivity().FindViewById(R.id.drawer_layout);
            if (view == null)
                return;
            if (view is DrawerLayout)
            {
                Toolbar toolbar = (Toolbar)GetView().FindViewById(R.id.toolbar);
                if (toolbar == null)
                    return;
                toolbar.SetNavigationIcon(R.mipmap.ic_menu_white_24dp);
                toolbar.SetNavigationOnClickListener((v) => ((DrawerLayout)view).OpenDrawer(GravityCompat.START));
            }
        }
    }
}