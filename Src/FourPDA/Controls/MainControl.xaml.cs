using ExceptionHelper;
using FourPDA.Views;
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

// Only for Win SDK builds >= 14393
//using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
//using NavigationViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs;


namespace FourPDA.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainControl : UserControl//Page
    {
        FourPDA.ViewModels.MainPageViewModel vm = default;
        public MainControl()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            this.InitializeComponent();
            //((FrameworkElement) this).Loaded += new EventHandler(this.PageLoaded);

            // ---------------------------------------------------------
            // Experimental
            //vm = (FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            //vm.RefreshData();
            // ---------------------------------------------------------

            var HomePage = $"FourPDA.HomePage";
            var HomePageType = Type.GetType(HomePage);

            //ContentFrame...
            //rootFrame.Navigate(HomePageType);
        }//



        // News click handler
        private void News_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            //FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel vm 
            //vm = (FourPDA.AppServices.ViewModels.MainPivot.NewsViewModel)DataContext;
            //vm.RefreshData();
            rootFrame.Navigate(typeof(NewsPageView));
        }//News_Click


        // Refresh click handler
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //ToAboutMe();
            //FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel vm 
            //vm = (FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel)DataContext;
            //vm.RefreshData();
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
            Frame rootFrame = Window.Current.Content as Frame;

            //NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            string pageName = $"FourPDA.{pageTag}";

            Type pageType = Type.GetType(pageName);

            //ContentFrame.Navigate(pageType);
            rootFrame.Navigate(pageType);
        }


    }
}

