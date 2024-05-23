// FourPDA.Converters.StringEmptinessToVisibilityConverter

using System;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;


#nullable disable
namespace FourPDA.Converters
{
  public class StringEmptinessToVisibilityConverter : IValueConverter
  {
  
    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        return (object)(Visibility)(string.IsNullOrWhiteSpace((string)value) ? 1 : 0);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
