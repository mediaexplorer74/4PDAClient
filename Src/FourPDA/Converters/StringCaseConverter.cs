// FourPDA.Converters.StringCaseConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class StringCaseConverter : IValueConverter
  {
  
    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        string str1 = (string)parameter;
        string str2 = (string)value;
        if (string.IsNullOrEmpty(str1))
            throw new InvalidOperationException("Wrong converter parameter");
        switch (str1.ToUpper()[0])
        {
            case 'L':
                return (object)str2.ToLower();
            case 'U':
                return (object)str2.ToUpper();
            default:
                throw new NotSupportedException();
        }
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
