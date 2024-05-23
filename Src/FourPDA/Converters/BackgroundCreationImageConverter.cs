// FourPDA.Converters.BackgroundCreationImageConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
//using Windows.UI.Media.Imaging;
//using System.Windows.Data;
//using System.Windows.Media.Imaging;

#nullable disable
namespace FourPDA.Converters
{
  public class BackgroundCreationImageConverter : IValueConverter
  {
   

    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
            if (value == null)
                return (object)null;
            if (value is string)
            {
                if (string.IsNullOrEmpty((string)value))
                    return (object)null;
                return (object)new BitmapImage(new Uri((string)value))
                {
                    CreateOptions = (BitmapCreateOptions)18
                };
            }
            return (object)(value as Uri) != null ? (object)new BitmapImage((Uri)value)
            {
                CreateOptions = (BitmapCreateOptions)18
            } : throw new NotSupportedException();
        }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
