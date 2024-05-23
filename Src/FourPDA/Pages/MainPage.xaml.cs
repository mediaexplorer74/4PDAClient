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
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using ExceptionHelper;
using ForPDA.AppServices.ViewModels.MainPivot;

namespace FourPDA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var HomePage = $"FourPDA.HomePage";
            var HomePageType = Type.GetType(HomePage);
            ContentFrame.Navigate(HomePageType);

        }


        // Refresh click handler
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //ToAboutMe();
            ForPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel vm = (ForPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            vm.RefreshData();
        }//AboutApp_Click

        //private void PageLoaded(object sender, EventArgs e)
        //{
        //    AppBar.Setup<MainPivotViewModel>((Page)this).Third((Action<IApplicationBarIconButton, MainPivotViewModel>)
        //        ((btn, vm) => vm.RefreshData()));
        //}



        private void ForumModeToggle_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void UiThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
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
            var pageName = $"FourPDA.{pageTag}";
            var pageType = Type.GetType(pageName);

            ContentFrame.Navigate(pageType);
        }

    }

}
