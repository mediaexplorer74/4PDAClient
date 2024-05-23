// FourPDA.Converters.MoneyConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class MoneyConverter : IValueConverter
  {
    private static readonly NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
    {
      NumberGroupSeparator = ' '.ToString(),
      NumberDecimalDigits = 0
    };


    public static object GetMoneyString(object value)
    {
      if (value == null)
        return (object) null;
      try
      {
        return (object) MoneyConverter.GetMoneyString(System.Convert.ToInt32(value));
      }
      catch (FormatException ex)
      {
        return value;
      }
    }

    public static string GetMoneyString(int intVal)
    {
      return string.Format((IFormatProvider) MoneyConverter.numberFormatInfo, "{0:N}", (object) intVal);
    }    

    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        return MoneyConverter.GetMoneyString(value);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
