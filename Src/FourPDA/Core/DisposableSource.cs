// FourPDA.Core.DisposableSource

using System;

#nullable disable
namespace FourPDA.Core
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
