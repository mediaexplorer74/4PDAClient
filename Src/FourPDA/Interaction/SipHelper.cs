// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.SipHelper
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public static class SipHelper
  {
    public static IDisposable EnableCompensationFor(ScrollViewer scrollViewer)
    {
      return (IDisposable) new KeyboardPageCompensation(Application.Current.RootVisual as PhoneApplicationFrame, scrollViewer);
    }
  }
}
