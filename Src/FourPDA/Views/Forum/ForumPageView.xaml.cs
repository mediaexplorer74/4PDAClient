// FourPDA.Views.ForumPageView

using FourPDA.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using System;
using System.Diagnostics;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core; // * Smart Navigation *
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.UI.Interop;


namespace FourPDA.Views
{

    // ForumPageView class 
    public sealed partial class ForumPageView : Page
    {
        private static readonly Uri HomeUri =
            new Uri("ms-appx-web:///Html/index.html", UriKind.Absolute);                                                                                                                                  // Included HTML file.

        private static readonly Uri
            FourPDAAppUri = new Uri("https://4pda.to/forum", UriKind.Absolute);

        // TODO: Delete it later!
        private static readonly Uri
           FourPDAForumUri = new Uri("https://4pda.to/forum", UriKind.Absolute);

        private static readonly String UserAgentPersonal =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/70.0.3538.102 Safari/537.36 Edge/18.19041";  // User-agent "const"

        private static readonly Color ThemeLight = Color.FromArgb(255, 230, 230, 230);
        private static readonly Color ThemeDark = Color.FromArgb(255, 31, 31, 31);

        private static int ZoomFactor = 100;                                                                                                                                                                                                // Zoom percentage (100 default, but might set smaller in future since the text is a bit large in my opinion).


        private static FourPDAClientApp.WebViewMode CurrentViewMode
            = FourPDAClientApp.WebViewMode.Compact; // Launch in compact mode.


        //MainPage
        public ForumPageView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            WebInitialize();

            //RnD
            ZoomFactor += 1;
            UpdateWebViewZoom();

            ZoomFactor -= 1;
            UpdateWebViewZoom();
        }//MainPage


        // Initialize
        public void WebInitialize()
        {
            WebViewControl.Settings.IsJavaScriptEnabled = true;
            WebViewControl.Settings.IsIndexedDBEnabled = true;

            StorageManager.Init();
            ReadAppData();

            RemoveClutterFromPage();

            UpdateWebViewZoom();

        }//Initialize


        // StoreUIThemeData
        public void StoreUIThemeData()
        {
            //StorageManager.WriteSimpleSetting(FourPDAClientApp.SAVE_DATA_DARKMODE,
            //    UiThemeToggle.IsChecked.Value);
        }//StoreUIThemeData


        // StoreForumModeData
        public void StoreForumModeData()
        {
            //StorageManager.WriteSimpleSetting(FourPDAClientApp.SAVE_DATA_FORUMMODE,
            //    ForumModeToggle.IsChecked.Value);
        }//StoreForumModeData


        // StoreZoomFactor
        public void StoreZoomFactor()
        {
            StorageManager.WriteSimpleSetting(FourPDAClientApp.SAVE_DATA_ZOOMFACTOR,
                ZoomFactor);
        }//StoreZoomFactor


        // ReadAppData
        // Reads settings from storage and init(s) Light/Dark mode and Zoom factor
        public void ReadAppData()
        {
            // Light/Dark mode
            Object isUiThemeToggleChecked = StorageManager.ReadSimpleSetting(
                FourPDAClientApp.SAVE_DATA_DARKMODE);

            //if (isUiThemeToggleChecked != null)
            //    UiThemeToggle.IsChecked = (bool)isUiThemeToggleChecked;

            //if (UiThemeToggle.IsChecked.Value)
            //{
            //    UiThemeToggle_Click(null, null);
            //}


            // Forum mode
            Object isForumModeChecked = StorageManager.ReadSimpleSetting(
                FourPDAClientApp.SAVE_DATA_FORUMMODE);

            //if (isForumModeChecked != null)
            //    ForumModeToggle.IsChecked = (bool)isForumModeChecked;

            //if (ForumModeToggle.IsChecked.Value)
            //{
            //    ForumModeToggle_Click(null, null);
            //}
            //else
            //{
                ToFourPDAClientApp();
            //}


            // Zoom factor
            Object textZoomFactor = StorageManager.ReadSimpleSetting(
                FourPDAClientApp.SAVE_DATA_ZOOMFACTOR);

            if (textZoomFactor != null)
                ZoomFactor = (int)textZoomFactor;

            //ChangeUIThemeThroughApp(UiThemeToggle.IsChecked.Value);
        }//ReadAppData


        // OnNavigatedTo 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ToFourPDAClientApp();
            //ForcePageOnScreen();
            base.OnNavigatedTo(e);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                = AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                //Debug.WriteLine("Special Back button Requested");
                if (WebViewControl.CanGoBack)
                {
                    WebViewControl.GoBack();
                    a.Handled = true;
                }
            };

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += (s, a) =>
                {
                    //Debug.WriteLine("Hardware Back button Requested");
                    if (WebViewControl.CanGoBack)
                    {
                        WebViewControl.GoBack();
                    }
                    a.Handled = true;
                };
            }
        }

        // BackButton handler
        private void BackButton_Tapped(object sender, BackRequestedEventArgs e)
        {
            //Debug.WriteLine("BACK button pressed: " + e.ToString());

            if (WebViewControl.CanGoBack)
            {
                WebViewControl.GoBack();
                //Debug.WriteLine("BaseUri: " + WebViewControl.BaseUri.ToString());
            }
        }


        // OnNavigatedFrom
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // do nothing
        }


        // Browser_NavigationCompleted
        private void Browser_NavigationCompleted(WebView sender,
            WebViewNavigationCompletedEventArgs args)
        {
            // to do more things
            UpdateWebViewZoom();
        }//Browser_NavigationCompleted


        // About click handler
        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            //ToAboutMe();
        }//AboutApp_Click

        // Settings click handler
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            //TODO

        }//Settings_Click


        //Home_Click
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPageView));

        }//Home_Click

        // News click handler
        private void News_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(NewsPageView));

        }//News_Click


        // Refresh click handler
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //

        }//Refresh_Click


        // UiThemeToggle_Click
        private void UiThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = ElementTheme.Dark;//UiThemeToggle.IsChecked.Value
                    //? ElementTheme.Dark
                    //: ElementTheme.Light; // Color.FromArgb(255,54,192,255);
            }

            //RnD
            try
            {
                SetStatusBarColor((Color)this.Resources["SystemAccentColor"]/*UiThemeToggle.IsChecked.Value
                    ? (Color)this.Resources["SystemAccentColor"]
                    : ThemeDark*/);
            }
            catch { }

            try
            {
                this.Background = new SolidColorBrush(ThemeDark
                    /*UiThemeToggle.IsChecked.Value ? ThemeDark : ThemeLight*/);
            }
            catch { }

            ChangeUIThemeThroughApp(/*UiThemeToggle.IsChecked.Value*/true);

            StoreUIThemeData();

        }//UiThemeToggle_Click


        // ForumModeToggle_Click
        private void ForumModeToggle_Click(object sender, RoutedEventArgs e)
        {

            //if (ForumModeToggle.IsChecked.Value)
            //{
                ToForumMode();
            //}
            //else
            //{
            //    ToFourPDAClientApp();
            //}

            StoreForumModeData();
        }//ForumModeToggle_Click


       



        // ZoomIn handler
        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ZoomFactor += 1;
            UpdateWebViewZoom();
        }

        // ZoomOut handler
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomFactor -= 1;
            UpdateWebViewZoom();
        }

        // SetStatusBarColor
        private void SetStatusBarColor(Color foregroundColor)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                try
                {
                    StatusBar statusBar = StatusBar.GetForCurrentView();
                    if (statusBar != null)
                    {
                        statusBar.ForegroundColor = foregroundColor;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] SetStatusBarColor error: " + ex.Message);
                }
            }
        }

        // ToAboutMe
        private void ToAboutMe()
        {
            WebViewControl.Navigate(HomeUri);
        }


        // ToFourPDAClientApp
        private void ToFourPDAClientApp()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, FourPDAAppUri);
            //requestMessage.Headers.Add("User-Agent", UserAgentPersonal);             
            WebViewControl.NavigateWithHttpRequestMessage(requestMessage);
        }//ToFourPDAClientApp


        // ToForumMode
        private void ToForumMode()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, FourPDAForumUri);
            //requestMessage.Headers.Add("User-Agent", UserAgentPersonal); 
            WebViewControl.NavigateWithHttpRequestMessage(requestMessage);
        }//ToForumMode


        // UpdateWebViewZoom
        private void UpdateWebViewZoom()
        {
            ScriptInjector.InvokeScriptOnWebView
            (
                WebViewControl,
                ScriptInjector.ChangeTextSizeForEachElement(ZoomFactor)
            );

            StoreZoomFactor();
        }//UpdateWebViewZoom


        // Back_Click
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //if (Chat.IsChecked.Value)
            //    Chat.IsChecked = false;

            //ChangeViewMode(FourPDAClientApp.WebViewMode.Contacts);

            //Debug.WriteLine("Toolbar BACK button pressed: " + e.ToString());

            if (WebViewControl.CanGoBack)
            {
                //Frame.GoBack();
                WebViewControl.GoBack();

                //Debug.WriteLine("BaseUri: " + WebViewControl.BaseUri.ToString());
            }
        }//Back_Click


        // Forward_Click
        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            //if (Contacts.IsChecked.Value)
            //    Contacts.IsChecked = false;
            //
            //ChangeViewMode(FourPDAClientApp.WebViewMode.Chat);

            // Log it
            Debug.WriteLine("Toolbar FORWARD button pressed: " + e.ToString());

            if (WebViewControl.CanGoForward)
            {
                WebViewControl.GoForward();
                //Debug.WriteLine("BaseUri: " + WebViewControl.BaseUri.ToString());
            }
        }//Forward_Click


        // CheckAnyChecked
        //private void CheckAnyChecked()
        //{
        //if (!Contacts.IsChecked.Value && !Chat.IsChecked.Value)
        //{
        //    ChangeViewMode(FourPDAClientApp.WebViewMode.Compact);
        //}
        //}//CheckAnyChecked

        //private void ForcePageOnScreen()
        //{
        //    ScriptInjector.InvokeScriptOnWebView(WebViewControl, 
        //        ScriptInjector.ChangeMinWidthForEachElement(0));
        //}

        private void RemoveClutterFromPage()
        {
            ScriptInjector.InvokeScriptsOnWebView
            (
                WebViewControl,
                ScriptInjector.HideElementByClass(FourPDAClientApp.NOTIFICATION_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.FLEX_CONTAINER_CLASS),
                //ScriptInjector.ModifyElementsWidthByClass(100, FourPDAClientApp.FLEX_CONTAINER_CLASS),
                //ScriptInjector.HideElementByClass(FourPDAClientApp.AD_CONTAINER_CLASS2),
                //ScriptInjector.ModifyElementsWidthByClass(100, FourPDAClientApp.AD_CONTAINER_CLASS2),
                ScriptInjector.HideElementByClass(FourPDAClientApp.SETTINGS_BAR_CLASS)
            );
        }

        private void ChangeUIThemeThroughApp(bool isDarkTheme)
        {
            ScriptInjector.InvokeScriptOnWebView(WebViewControl,
                ScriptInjector.ChangeBodyClassName(isDarkTheme ? "web dark" : "web"));
        }

        private void ShowContactsWindow()
        {
            ScriptInjector.InvokeScriptsOnWebView(WebViewControl,
                ScriptInjector.ShowElementByClass(FourPDAClientApp.EFFECTIVE_FLEX_LEFT_CONTAINER_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.EFFECTIVE_FLEX_RIGHT_CONTAINER_CLASS)
            );
        }

        private void ShowChatWindow()
        {
            ScriptInjector.InvokeScriptsOnWebView(WebViewControl,
                ScriptInjector.ShowElementByClass(FourPDAClientApp.EFFECTIVE_FLEX_RIGHT_CONTAINER_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.EFFECTIVE_FLEX_LEFT_CONTAINER_CLASS)
            );
        }

        private void ShowCompactWindow()
        {
            /*
            ScriptInjector.InvokeScriptsOnWebView(WebViewControl,
                ScriptInjector.ShowElementByClass(FourPDAClientApp.EFFECTIVE_FLEX_RIGHT_CONTAINER_CLASS),
                ScriptInjector.ShowElementByClass(FourPDAClientApp.EFFECTIVE_FLEX_LEFT_CONTAINER_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.SETTINGS_BAR_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.CONTACT_SEARCH_CHAT)
            );
            */
            ScriptInjector.InvokeScriptsOnWebView
            (
                WebViewControl,
                ScriptInjector.HideElementByClass(FourPDAClientApp.NOTIFICATION_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.FLEX_CONTAINER_CLASS),
                ScriptInjector.HideElementByClass(FourPDAClientApp.SETTINGS_BAR_CLASS)
            );
        }

        private void ChangeViewMode(FourPDAClientApp.WebViewMode webViewMode)
        {
            switch (webViewMode)
            {
                case FourPDAClientApp.WebViewMode.Contacts:
                    ShowContactsWindow();
                    break;
                case FourPDAClientApp.WebViewMode.Chat:
                    ShowChatWindow();
                    break;
                case FourPDAClientApp.WebViewMode.Compact:
                    ShowCompactWindow();
                    break;
            }

            if (webViewMode != FourPDAClientApp.WebViewMode.Compact)
            {
                //RnD
                //RemoveClutterFromPage();
                //CheckAnyChecked();
                //ForcePageOnScreen();
            }
            else
            {
                RemoveClutterFromPage();
                //CheckAnyChecked();
                //ForcePageOnScreen();
            }

            UpdateWebViewZoom();

            CurrentViewMode = webViewMode;
        }
    }//MainPage class end


   



    /*
     public sealed partial class ForumPageView : Page
     {



         public ForumPageView()
         {
             this.InitializeComponent();
         }

         public ForumPageViewModel ForumPageViewModel
         {
             get => (ForumPageViewModel)((FrameworkElement)this).DataContext;
         }

         protected override void OnBackKeyPress(EventArgs e)
         {
             //base.OnBackKeyPress(e);
             if (!this.ForumPageViewModel.CanReturnBack)
                 return;
             //this.Title.IsBackTransition = true;
             //this.Title.NewContentTransitionEnded += new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
             this.ForumPageViewModel.GoBack();
             //e.Cancel = true;
         }

         private void Title_NewContentTransitionEnded(object sender, EventArgs e)
         {
             //this.Title.NewContentTransitionEnded -= new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
             //this.Title.IsBackTransition = false;
         }
     }*/
}

/*
using FourPDA.AppServices.ViewModels.Forum;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Views.Forum
{
  public sealed partial class ForumPage : PhoneApplicationPage
  {
   
    public ForumPage() => this.InitializeComponent();

    public ForumPageViewModel ViewModel
    {
      get => (ForumPageViewModel) ((FrameworkElement) this).DataContext;
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      base.OnBackKeyPress(e);
      if (!this.ViewModel.CanReturnBack)
        return;
      this.Title.IsBackTransition = true;
      this.Title.NewContentTransitionEnded += new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
      this.ViewModel.GoBack();
      e.Cancel = true;
    }

    private void Title_NewContentTransitionEnded(object sender, EventArgs e)
    {
      this.Title.NewContentTransitionEnded -= new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
      this.Title.IsBackTransition = false;
    }  
  }
}

 */
