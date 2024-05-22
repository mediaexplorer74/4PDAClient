// Decompiled with JetBrains decompiler
// Type: ForPDA.Core.DisposableSource
// Assembly: ForPDA.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F063FDB-43B6-4F39-AEFE-6C4BC0FF6CDF
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.Core.dll

using System;

#nullable disable
namespace ForPDA.Core
{
  public class DisposableSource : IDisposable
  {
    private readonly Action _diposeAction;

    public DisposableSource(Action disposeAction)
    {
      this._diposeAction = disposeAction != null ? disposeAction : throw new ArgumentNullException(nameof (disposeAction));
    }

    public void Dispose() => this._diposeAction();
  }
}
