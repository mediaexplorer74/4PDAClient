// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.InvertedBoolConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class InvertedBoolConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) !System.Convert.ToBoolean(value);
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
