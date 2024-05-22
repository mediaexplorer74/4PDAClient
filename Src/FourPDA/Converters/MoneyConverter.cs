// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.MoneyConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Globalization;
using System.Windows.Data;

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

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return MoneyConverter.GetMoneyString(value);
    }

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

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
