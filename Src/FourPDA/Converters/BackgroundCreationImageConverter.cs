// Decompiled with JetBrains decompiler
// Type: FourPDA.Converters.BackgroundCreationImageConverter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

#nullable disable
namespace FourPDA.Converters
{
  public class BackgroundCreationImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return (object) null;
      if (value is string)
      {
        if (string.IsNullOrEmpty((string) value))
          return (object) null;
        return (object) new BitmapImage(new Uri((string) value))
        {
          CreateOptions = (BitmapCreateOptions) 18
        };
      }
      return (object) (value as Uri) != null ? (object) new BitmapImage((Uri) value)
      {
        CreateOptions = (BitmapCreateOptions) 18
      } : throw new NotSupportedException();
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
