// FourPDA.Interaction.SipHelper

using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public static class SipHelper
  {
    public static IDisposable EnableCompensationFor(ScrollViewer scrollViewer)
    {
            return default;//(IDisposable) 
             //   new KeyboardPageCompensation(Application.Current as Frame, scrollViewer);
    }
  }
}
