// FourPDA.Converters.GenericNullConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
//using System.Windows.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class GenericNullConverter : IValueConverter
  {
    public object IsNullValue { get; set; }

    public object NotNullValue { get; set; }

  
    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        return value == null ? this.IsNullValue : this.NotNullValue;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
