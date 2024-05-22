// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.OrientationManager
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;

#nullable disable
namespace ForPDA.AppServices
{
  public class OrientationManager : INotifyPropertyChanged
  {
    private PhoneApplicationFrame _frame;

    public PhoneApplicationFrame Frame
    {
      get
      {
        if (this._frame == null)
        {
          this._frame = ScreenHelper.Frame;
          this._frame.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(this.FrameOnOrientationChanged);
        }
        return this._frame;
      }
    }

    public PageOrientation Orientation => this.Frame.Orientation;

    private void FrameOnOrientationChanged(
      object sender,
      OrientationChangedEventArgs orientationChangedEventArgs)
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
