// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.IBrowserView
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using System;
using System.Windows.Navigation;

#nullable disable
namespace ForPDA.AppServices
{
  public interface IBrowserView
  {
    void LoadUri(string uri);

    void LoadContent(string htmlContent);

    event Action<NavigationEventArgs> NavigationCompleted;
  }
}
