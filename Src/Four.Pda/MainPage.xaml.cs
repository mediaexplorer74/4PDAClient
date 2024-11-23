using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Four.Pda
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}

/*
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
            component = DaggerFourPdaComponent.Builder()
                .ClientModule(new ClientModule(this)).Build();
        }

        private void TuneLogs()
        {
            Fabric fabric = new Builder(this)
                .Kits(new Crashlytics()).Logger(new SilentLogger()).Build();
            Fabric.With(fabric);
        }

        public virtual FourPdaComponent Component()
        {
            return component;
        }
    }
}
 */