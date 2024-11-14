using ExceptionHelper;
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
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs;


namespace FourPDA.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPageView : Page
    {
        FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel vm = default;
        public MainPageView()
        {
            this.InitializeComponent();
            //((FrameworkElement) this).Loaded += new EventHandler(this.PageLoaded);

            // ---------------------------------------------------------
            // Experimental
            //vm = (FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            //vm.RefreshData();
            // ---------------------------------------------------------

            var HomePage = $"FourPDA.HomePage";
            var HomePageType = Type.GetType(HomePage);

            ContentFrame.Navigate(HomePageType);
        }//


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
        //    AppBar.Setup<MainPivotViewModel>((Page)this).Third((Action<IApplicationBarIconButton, MainPivotViewModel>)
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

        private void MainNav_OnItemInvoked(NavigationView sender, 
            NavigationViewItemInvokedEventArgs args)
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
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            string pageName = $"FourPDA.{pageTag}";

            Type pageType = Type.GetType(pageName);

            ContentFrame.Navigate(pageType);
        }


    }
}

