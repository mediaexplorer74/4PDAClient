// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.GenericNullConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class GenericNullConverter : IValueConverter
  {
    public object IsNullValue { get; set; }

    public object NotNullValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value == null ? this.IsNullValue : this.NotNullValue;
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
