// FourPDA.Converters.GenericBoolConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class GenericBoolConverter : IValueConverter
  {
    public object TrueValue { get; set; }

    public object FalseValue { get; set; }


    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        return !(bool)value ? this.FalseValue : this.TrueValue;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
