// Decompiled with JetBrains decompiler
// Type: FourPDA.Views.Forum.ForumPage
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using ForPDA.AppServices.ViewModels.Forum;
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
  public class ForumPage : PhoneApplicationPage
  {
    internal Grid LayoutRoot;
    internal RadTransitionControl Title;
    internal Grid ContentPanel;
    private bool _contentLoaded;

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

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/FourPDA;component/Views/Forum/ForumPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.Title = (RadTransitionControl) ((FrameworkElement) this).FindName("Title");
      this.ContentPanel = (Grid) ((FrameworkElement) this).FindName("ContentPanel");
    }
  }
}
