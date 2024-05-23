// FourPDA.Converters.BooleanToVisibilityConverter

using System;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public bool Invert { get; set; }


    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        bool flag = (bool)value;
        if (this.Invert)
            flag = !flag;
        return (object)(Visibility)(flag ? 0 : 1);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
