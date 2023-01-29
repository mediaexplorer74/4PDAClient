using System;
using System.Diagnostics;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace FourPDAClientApp
{
    public sealed partial class MainPage : Page
    {
        private static readonly Uri HomeUri = 
            new Uri("ms-appx-web:///Html/index.html", UriKind.Absolute);                                                                                                                                  // Included HTML file.
      
        private static readonly Uri 
            WhatsAppUri = new Uri("https://4pda.to", UriKind.Absolute);  
                                                                                                                                          // URI FourPDAClientApp.

        private static readonly String UserAgentPersonal = 
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/70.0.3538.102 Safari/537.36 Edge/18.19041";  // User-agent "const"

        private static readonly Color ThemeLight = Color.FromArgb(255, 230, 230, 230);// UWP light color.
        private static readonly Color ThemeDark = Color.FromArgb(255, 31, 31, 31);   // UWP dark color.

        private static int ZoomFactor = 100;                                                                                                                                                                                                // Zoom percentage (100 default, but might set smaller in future since the text is a bit large in my opinion).

        
        private static FourPDAClientApp.WebViewMode CurrentViewMode 
            = FourPDAClientApp.WebViewMode.Compact; // Launch in compact mode.


        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            Initialize();

            //RnD
            ZoomFactor += 1;
            UpdateWebViewZoom();

            ZoomFactor -= 1;
            UpdateWebViewZoom();

        }

        public void Initialize()
        {
            SetStatusBarColor(Colors.Black);

            this.Background = new SolidColorBrush(ThemeLight);
            
            WebViewControl.Settings.IsJavaScriptEnabled = true;
            
            WebViewControl.Settings.IsIndexedDBEnabled = true;

            StorageManager.Init();
            
            ReadAppData();

            
            UpdateWebViewZoom();

            ChangeViewMode(FourPDAClientApp.WebViewMode.Compact);

            ToFourPDAClientApp();


            //ForcePageOnScreen();

            //ToFourPDAClientApp();
           
        }

        public void StoreUIThemeData()
        {
            StorageManager.WriteSimpleSetting(FourPDAClientApp.SAVE_DATA_DARKMODE, 
                UiThemeToggle.IsChecked.Value);
        }

        public void StoreZoomFactor()
        {
            StorageManager.WriteSimpleSetting(FourPDAClientApp.SAVE_DATA_ZOOMFACTOR, 
                ZoomFactor);
        }

        public void ReadAppData()
        {
            Object isUiThemeToggleChecked = StorageManager.ReadSimpleSetting(
                FourPDAClientApp.SAVE_DATA_DARKMODE);

            if (isUiThemeToggleChecked != null)
                UiThemeToggle.IsChecked = (bool)isUiThemeToggleChecked;

            if (UiThemeToggle.IsChecked.Value)
                UiThemeToggle_Click(null, null);

            Object textZoomFactor = StorageManager.ReadSimpleSetting(
                FourPDAClientApp.SAVE_DATA_ZOOMFACTOR);

            if (textZoomFactor != null)
                ZoomFactor = (int)textZoomFactor;

            ChangeUIThemeThroughWhatsApp(UiThemeToggle.IsChecked.Value);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ToFourPDAClientApp();
            //ForcePageOnScreen();            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //
        }

        private void Browser_NavigationCompleted(WebView sender, 
            WebViewNavigationCompletedEventArgs args)
        {
            // RnD
            UpdateWebViewZoom();
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            ToAboutMe();
        }

        private void UiThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = UiThemeToggle.IsChecked.Value 
                    ? ElementTheme.Dark 
                    : ElementTheme.Light; // Color.FromArgb(255,54,192,255);
            }

            //RnD
            SetStatusBarColor(UiThemeToggle.IsChecked.Value 
                ? (Color)this.Resources["SystemAccentColor"] 
                : ThemeDark);
            
            this.Background = new SolidColorBrush(
                UiThemeToggle.IsChecked.Value ? ThemeDark : ThemeLight);
            
            ChangeUIThemeThroughWhatsApp(UiThemeToggle.IsChecked.Value);
            
            StoreUIThemeData();
        }

        private void RefreshFourPDAClientApp_Click(object sender, RoutedEventArgs e)
        {
            ToFourPDAClientApp();
        }


        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {

            ZoomFactor += 1;
            UpdateWebViewZoom();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomFactor -= 1;
            UpdateWebViewZoom();
        }

        private void SetStatusBarColor(Color foregroundColor)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.ForegroundColor = foregroundColor;
                }
            }
        }

        private void ToAboutMe()
        {
            WebViewControl.Navigate(HomeUri);
        }

        private void ToFourPDAClientApp()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, WhatsAppUri);
            
            //RnD
            //requestMessage.Headers.Add("User-Agent", UserAgentPersonal); // Previously 'UserAgentEdge'
            
            WebViewControl.NavigateWithHttpRequestMessage(requestMessage);
        }

        private void UpdateWebViewZoom()
        {
            ScriptInjector.InvokeScriptOnWebView(WebViewControl,
            ScriptInjector.ChangeTextSizeForEachElement(ZoomFactor));
            
            StoreZoomFactor();
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            if (Chat.IsChecked.Value)
                Chat.IsChecked = false;

            ChangeViewMode(FourPDAClientApp.WebViewMode.Contacts);
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            if (Contacts.IsChecked.Value)
                Contacts.IsChecked = false;

            ChangeViewMode(FourPDAClientApp.WebViewMode.Chat);
        }

        private void CheckAnyChecked()
        {
            if (!Contacts.IsChecked.Value && !Chat.IsChecked.Value)
            {
                ChangeViewMode(FourPDAClientApp.WebViewMode.Compact);
            }
        }

        private void ForcePageOnScreen()
        {
            ScriptInjector.InvokeScriptOnWebView(WebViewControl, 
                ScriptInjector.ChangeMinWidthForEachElement(0));
        }

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

        private void ChangeUIThemeThroughWhatsApp(bool isDarkTheme)
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
            switch(webViewMode)
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
    }

    public class ScriptInjector
    {
        public static string ChangeFontSizeByID(int percentage, string id)
        {
            return $@"document.getElementById('{id}').style.fontSize = '{percentage}%';";
        }

        public static string HideElementByClass(string item)
        {
            return $@"
            const elements = document.getElementsByClassName('{item}');
            for (var i = 0; i < elements.length; i++) elements[i].style.display = 'none';
            ";
        }

        public static string ShowElementByClass(string item)
        {
            return $@"
            const elements = document.getElementsByClassName('{item}');
            for (var i = 0; i < elements.length; i++) elements[i].style.display = '';
            ";
        }

        public static string ModifyElementsWidthByClass(int percentage_width, string item)
        {
            return $@"
            const elements = document.getElementsByClassName('{item}');
            for (var i = 0; i < elements.length; i++) elements[i].style.width='{percentage_width}%';
            ";
        }

        public static string ChangeTextSizeForEachElement(int percentage)
        {
            return $@"
            var elements = document.getElementsByTagName('div');
            for (var i = 0; i < elements.length; i++) 
                elements[i].style.fontSize = '{percentage}%';
            ";
        }

        public static string ChangeMinWidthForEachElement(int minimum_width_px)
        {
            return $@"
            var elements = document.getElementsByTagName('div');
            for (var i = 0; i < elements.length; i++) 
                elements[i].style.minWidth = '{minimum_width_px}px';
            ";
        }

        public static string ChangeBodyClassName(string item)
        {
            return $@"document.body.className = '{item}';";
        }

        public static string ChangeFlexByClass(int percentage, string item)
        {
            return $@"
            const elements = document.getElementsByClassName('{item}');
            for (var i = 0; i < elements.length; i++) elements[i].style.flexBasis='{item}%';
            ";
        }

        public static async void InvokeScriptOnWebView(WebView webView, String script)
        {
            // Do not use await, crashes x86 / x64 app due to invalid javascript syntax somewhere.
            webView.InvokeScriptAsync("eval", new String[] { script });
        }

        public static async void InvokeScriptsOnWebView(WebView webView, 
            params String[] scripts)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                InvokeScriptOnWebView(webView, scripts[i]);
            }
        }
    }

    public static class FourPDAClientApp
    {
        // HTML classes and id's which could be modified using javascript injection.
        // To add more navigate to 4pda.to in Firefox (Developer Edition)
        // on your computer and hit f12 (or right click and hit the inspection button). 
        public static readonly string SETTINGS_BAR_CLASS = "_1QUKR";                   
        public static readonly string NOTIFICATION_CLASS = "_3kqUo";
        public static readonly string FLEX_CONTAINER_CLASS = "v-count";// i2z2ViY8z2V U7p21YIoHNlc";//"h70RQ two";
        public static readonly string AD_CONTAINER_CLASS1 = "ei2Zc";// i2z2ViY8z2V U7p21YIoHNlc";
        public static readonly string AD_CONTAINER_CLASS2 = "ei2Zc i2z2ViY8z2V i2z2z2k4kyhFiHjOXz26ie";
        public static readonly string EFFECTIVE_FLEX_LEFT_CONTAINER_CLASS = "_1xXdX";
        public static readonly string EFFECTIVE_FLEX_RIGHT_CONTAINER_CLASS = "Wu52Z";
        public static readonly string CONTACT_SEARCH_CHAT = "_2EoyP";
        public static readonly string SAVE_DATA_DARKMODE = "theme_dark";
        public static readonly string SAVE_DATA_ZOOMFACTOR = "text_zoomfactor";

        public enum WebViewMode
        {
            Compact,
            Chat,
            Contacts
        }
    }

    public static class StorageManager
    {
        private static Windows.Storage.ApplicationDataContainer localSettings;
        private static Windows.Storage.StorageFolder localFolder;

        public static void Init()
        {
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        public static Object ReadSimpleSetting(string id)
        {
            if (localSettings != null && localFolder != null)
            {
                try
                {
                    return localSettings.Values[id];
                }
                catch (System.NullReferenceException e)
                {
                    Debug.WriteLine("No save data located");
                };
            }
            else DebugWarnNotInitialised();

            return null;
        }

        public static void WriteSimpleSetting(string id, Object toStore)
        {
            if (localSettings != null && localFolder != null)
            {
                localSettings.Values[id] = toStore;
            }
            else DebugWarnNotInitialised();
        }

        private static void DebugWarnNotInitialised()
        {
            Debug.WriteLine(@"Have you called ""StorageManager.Init()""?");
        }
    }
}

