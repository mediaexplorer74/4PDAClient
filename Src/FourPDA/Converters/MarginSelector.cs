// FourPDA.Converters.MarginSelector
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll


using System;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace FourPDA.Converters
{
  public class MarginSelector : IValueConverter
  {
    public Thickness Portrait { get; set; }

    public Thickness Landscape { get; set; }

    public Thickness LandscapeLeft { get; set; }

    public Thickness LandscapeRight { get; set; }

  
    private bool IsThicknessNull(Thickness thickness)
    {
      return thickness.Bottom == 0.0 && thickness.Left == 0.0 && thickness.Right == 0.0 && thickness.Top == 0.0;
    }

   

        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            //PageOrientation pageOrientation = (PageOrientation)value;
            //if (pageOrientation == 1 || pageOrientation == 9 || pageOrientation == 5)
            //    return (object)this.Portrait;
            //if (pageOrientation == 18)
            //    return (object)(this.IsThicknessNull(this.LandscapeLeft) ? this.Landscape : this.LandscapeLeft);
            //if (pageOrientation == 34)
            //    return (object)(this.IsThicknessNull(this.LandscapeRight) ? this.Landscape : this.LandscapeRight);
            throw new NotSupportedException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
