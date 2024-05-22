// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.IBusyIndicator
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using System;

#nullable disable
namespace ForPDA.AppServices
{
  public interface IBusyIndicator
  {
    bool IsBusy { get; }

    IDisposable StartJob();

    void EndJob();
  }
}
