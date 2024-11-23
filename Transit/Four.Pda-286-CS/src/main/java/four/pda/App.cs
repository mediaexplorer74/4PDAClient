using Android.App;
using Com.Crashlytics.Android;
using Org.Slf4j;
using Four.Pda.Dagger;
using Io.Fabric.Sdk.Android;
using System.Diagnostics;

namespace Four.Pda
{
    public class App : Application
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(App));
        private FourPdaComponent component;
        public override void OnCreate()
        {
            base.OnCreate();
            TuneLogs();
            L.Debug("Start application");
            component = DaggerFourPdaComponent.Builder().ClientModule(new ClientModule(this)).Build();
        }

        private void TuneLogs()
        {
            Fabric fabric = new Builder(this).Kits(new Crashlytics()).Logger(new SilentLogger()).Build();
            Fabric.With(fabric);
        }

        public virtual FourPdaComponent Component()
        {
            return component;
        }
    }
}