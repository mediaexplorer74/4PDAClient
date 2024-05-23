// ForPDA.AppServices.ScreenHelper


//using Microsoft.Phone.Controls;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace ForPDA.AppServices
{
  public static class ScreenHelper
  {
    public static Frame Frame { get; private set; }

    public static void Init(Frame frame) => ScreenHelper.Frame = frame;

    public static bool IsDarkTheme
        {
            get
            {
                return default;//(Visibility)Application.Current.Resources[(object)"PhoneLightThemeVisibility"] == 1;
            }
        }
    }
}
