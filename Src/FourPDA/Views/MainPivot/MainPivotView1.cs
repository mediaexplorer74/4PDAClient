// Decompiled with JetBrains decompiler
// Type: FourPDA.Views.MainPivot.MainPivotView
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using FluentAppBar;
using ForPDA.AppServices.ViewModels.MainPivot;
using FourPDA.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Views.MainPivot
{
  public class MainPivotView : CultureAwarePage
  {
    internal Grid LayoutRoot;
    private bool _contentLoaded;

    public MainPivotView()
    {
      this.InitializeComponent();
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.PageLoaded);
    }

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
      AppBar.Setup<MainPivotViewModel>((PhoneApplicationPage) this).Third((Action<IApplicationBarIconButton, MainPivotViewModel>) ((btn, vm) => vm.RefreshData()));
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/FourPDA;component/Views/MainPivot/MainPivotView.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
    }
  }
}
