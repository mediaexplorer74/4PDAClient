using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using ExceptionHelper;
using FourPDA.AppServices.ViewModels.MainPivot;

// Only for Win SDK builds >= 14393
//using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
//using NavigationViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs;


namespace FourPDA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //Frame rootFrame = Window.Current.Content as Frame;

        FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel vm = default;
        public MainPage()
        {
            this.InitializeComponent();

            // ---------------------------------------------------------
            // Experimental
            //vm = (FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            //vm.RefreshData();
            // ---------------------------------------------------------

            var HomePage = $"FourPDA.HomePage";
            var HomePageType = Type.GetType(HomePage);

            //ContentFrame.Navigate(HomePageType);

        }


        // Refresh click handler
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //ToAboutMe();
            //FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel vm 
            vm = (FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            vm.RefreshData();
        }//Refresh_Click

        //private void PageLoaded(object sender, EventArgs e)
        //{
        //    AppBar.Setup<MainPivotViewModel>((Page)this).Third(
        //    (Action<IApplicationBarIconButton, MainPivotViewModel>)
        //        ((btn, vm) => vm.RefreshData()));
        //}


        private void ForumModeToggle_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void UiThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            //
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            //
        }


        private void News_Click(object sender, RoutedEventArgs e)
        {
            //RnD
            // vm = (FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            // vm.RefreshData();
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(NewsPage));
        }


        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            //
        }


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void MainNav_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            try
            {
                NavigateToPage(args.InvokedItemContainer.Tag);
            }
            catch (System.Exception ex)
            {
                Exceptions.ThrownExceptionError(ex);
            }
        }

        public void NavigateToPage(object pageTag)
        {
            NavigationCacheMode = NavigationCacheMode.Enabled;

            string pageName = $"FourPDA.{pageTag}";

            Type pageType = Type.GetType(pageName);

            //ContentFrame.Navigate(pageType);
            Frame.Navigate(pageType);
        }

    }

}
