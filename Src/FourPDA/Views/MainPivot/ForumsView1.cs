// Decompiled with JetBrains decompiler
// Type: FourPDA.Views.MainPivot.ForumsView
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Views.MainPivot
{
  public class ForumsView : UserControl
  {
    internal Grid LayoutRoot;
    private bool _contentLoaded;

    public ForumsView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/FourPDA;component/Views/MainPivot/ForumsView.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
    }
  }
}
