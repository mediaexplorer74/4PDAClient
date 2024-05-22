// Decompiled with JetBrains decompiler
// Type: FourPDA.Views.NewsDetailsPage
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using ForPDA.AppServices;
using FourPDA.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using System.Windows.Navigation;

#nullable disable
namespace FourPDA.Views
{
  public class NewsDetailsPage : CultureAwarePage, IBrowserView
  {
    internal Grid LayoutRoot;
    internal WebBrowser Browser;
    private bool _contentLoaded;

    public event Action<NavigationEventArgs> NavigationCompleted = param0 => { };

    public NewsDetailsPage() => this.InitializeComponent();

    public void LoadUri(string uri) => this.Browser.Source = new Uri(uri);

    public void LoadContent(string htmlContent) => this.Browser.NavigateToString(htmlContent);

    private void WebBrowser_OnNavigated(object sender, NavigationEventArgs e)
    {
      this.NavigationCompleted(e);
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/FourPDA;component/Views/NewsDetailsPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.Browser = (WebBrowser) ((FrameworkElement) this).FindName("Browser");
    }
  }
}
