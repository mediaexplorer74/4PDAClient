// FourPDA.Converters.GenericSwitchConverter


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;


#nullable disable
namespace FourPDA.Converters
{
  [ContentProperty] // ("Cases")
    public class GenericSwitchConverter : DependencyObject, IValueConverter
  {
    public GenericSwitchConverter() => this.Cases = new List<GenericCase>();

    public object Other { get; set; }

    public List<GenericCase> Cases { get; set; }



        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            string str = value.ToString();
            foreach (GenericCase genericCase in this.Cases)
            {
                if (genericCase.Case.Equals(str, (StringComparison)3))
                    return genericCase.Value;
            }
            return this.Other;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
