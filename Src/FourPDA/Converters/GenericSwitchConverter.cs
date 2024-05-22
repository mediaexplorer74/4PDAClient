// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.GenericSwitchConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

#nullable disable
namespace FourPDA.Converters
{
  [ContentProperty("Cases")]
  public class GenericSwitchConverter : DependencyObject, IValueConverter
  {
    public GenericSwitchConverter() => this.Cases = new List<GenericCase>();

    public object Other { get; set; }

    public List<GenericCase> Cases { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string str = value.ToString();
      foreach (GenericCase genericCase in this.Cases)
      {
        if (genericCase.Case.Equals(str, (StringComparison) 3))
          return genericCase.Value;
      }
      return this.Other;
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
