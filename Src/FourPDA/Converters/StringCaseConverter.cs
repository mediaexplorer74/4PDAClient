// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.StringCaseConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class StringCaseConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string str1 = (string) parameter;
      string str2 = (string) value;
      if (string.IsNullOrEmpty(str1))
        throw new InvalidOperationException("Wrong converter parameter");
      switch (str1.ToUpper()[0])
      {
        case 'L':
          return (object) str2.ToLower();
        case 'U':
          return (object) str2.ToUpper();
        default:
          throw new NotSupportedException();
      }
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
