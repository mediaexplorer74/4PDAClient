// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.ScreenHelper
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Microsoft.Phone.Controls;
using System.Windows;

#nullable disable
namespace ForPDA.AppServices
{
  public static class ScreenHelper
  {
    public static PhoneApplicationFrame Frame { get; private set; }

    public static void Init(PhoneApplicationFrame frame) => ScreenHelper.Frame = frame;

    public static bool IsDarkTheme
    {
      get => (Visibility) Application.Current.Resources[(object) "PhoneLightThemeVisibility"] == 1;
    }
  }
}
