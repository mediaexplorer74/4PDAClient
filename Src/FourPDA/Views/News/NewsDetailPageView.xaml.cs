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
using FourPDA.AppServices;
using FourPDA.Controls;
using FourPDA.Common;
using System.Diagnostics;
using System.Windows;


namespace FourPDA.Views
{
    
    public sealed partial class NewsDetailPageView : Page
    {
        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public NewsDetailPageView()
        {
            this.InitializeComponent();

            //!
            this.navigationHelper = new NavigationHelper(this);

        }

        public event Action<NavigationEventArgs> NavigationCompleted = param0 => { };

        public void LoadUri(string uri)
        {
            //this.Browser.Source = new Uri(uri);
        }

        public void LoadContent(string htmlContent)
        {
            //this.Browser.NavigateToString(htmlContent);
        }

        private void WebBrowser_OnNavigated(object sender, NavigationEventArgs e)
        {
            this.NavigationCompleted(e);
        }
    }
}

