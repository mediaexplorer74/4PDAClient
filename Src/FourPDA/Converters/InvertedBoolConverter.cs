// FourPDA.Converters.InvertedBoolConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class InvertedBoolConverter : IValueConverter
  {  

    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        return (object)!System.Convert.ToBoolean(value);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
