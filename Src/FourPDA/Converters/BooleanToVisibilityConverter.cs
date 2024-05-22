// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.BooleanToVisibilityConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public bool Invert { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool flag = (bool) value;
      if (this.Invert)
        flag = !flag;
      return (object) (Visibility) (flag ? 0 : 1);
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
