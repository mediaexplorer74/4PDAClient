// FourPDA.Converters.CollectionEmptinessToVisibilityConverter


using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class CollectionEmptinessToVisibilityConverter : IValueConverter
  {  

    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        IEnumerable source = (IEnumerable)value;
        return (object)(Visibility)(source != null && source.Cast<object>().Any<object>() ? 0 : 1);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
