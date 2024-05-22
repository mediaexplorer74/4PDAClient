// Decompiled with JetBrains decompiler
// Type: FourPDA.Controls.GlobalBusyIndicator
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using ForPDA.AppServices;
using ForPDA.Core;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Controls
{
  public class GlobalBusyIndicator : DependencyObject, IBusyIndicator
  {
    public static readonly DependencyProperty BusyIndicatorProperty = DependencyProperty.RegisterAttached("BusyIndicator", typeof (IBusyIndicator), typeof (GlobalBusyIndicator), new PropertyMetadata((PropertyChangedCallback) null));
    private static ProgressIndicator _progressIndicator;
    private readonly PhoneApplicationPage _page;
    private int _isBusyCounter;

    public static IBusyIndicator Create()
    {
      PhoneApplicationPage content = ((ContentControl) Application.Current.RootVisual).Content as PhoneApplicationPage;
      if (!(((DependencyObject) content).GetValue(GlobalBusyIndicator.BusyIndicatorProperty) is IBusyIndicator busyIndicator))
      {
        busyIndicator = (IBusyIndicator) new GlobalBusyIndicator(content);
        ((DependencyObject) content).SetValue(GlobalBusyIndicator.BusyIndicatorProperty, (object) busyIndicator);
      }
      return busyIndicator;
    }

    private GlobalBusyIndicator(PhoneApplicationPage page)
    {
      this._page = page;
      if (GlobalBusyIndicator._progressIndicator == null)
        GlobalBusyIndicator._progressIndicator = new ProgressIndicator();
      ((DependencyObject) this._page).SetValue(SystemTray.ProgressIndicatorProperty, (object) GlobalBusyIndicator._progressIndicator);
    }

    public bool IsBusy => this._isBusyCounter > 0;

    public IDisposable StartJob()
    {
      ++this._isBusyCounter;
      this.UpdateIndicatorVisibility();
      return (IDisposable) new DisposableSource((Action) (() => this.EndJob()));
    }

    public void EndJob()
    {
      if (this._isBusyCounter > 0)
        --this._isBusyCounter;
      this.UpdateIndicatorVisibility();
    }

    private void UpdateIndicatorVisibility()
    {
      bool flag = this._isBusyCounter > 0;
      GlobalBusyIndicator._progressIndicator.IsVisible = GlobalBusyIndicator._progressIndicator.IsIndeterminate = flag;
    }
  }
}
