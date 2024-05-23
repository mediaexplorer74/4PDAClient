// ForPDA.AppServices.OrientationManager

//using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace ForPDA.AppServices
{
  public class OrientationManager : INotifyPropertyChanged
  {
    private Frame _frame;

    public Frame Frame
    {
      get
      {
        if (this._frame == null)
        {
          this._frame = ScreenHelper.Frame;
          //this._frame.OrientationChanged += 
          //              new EventHandler<OrientationChangedEventArgs>(this.FrameOnOrientationChanged);
        }
        return this._frame;
      }
    }

   // public PageOrientation Orientation => this.Frame.Orientation;

    private void FrameOnOrientationChanged(
      object sender,
      EventArgs orientationChangedEventArgs)
    {
      this.OnPropertyChanged("Orientation");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
