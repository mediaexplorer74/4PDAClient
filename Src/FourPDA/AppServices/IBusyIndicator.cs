// FourPDA.AppServices.IBusyIndicator

using System;

#nullable disable
namespace FourPDA.AppServices
{
  public interface IBusyIndicator
  {
    bool IsBusy { get; }

    IDisposable StartJob();

    void EndJob();
  }
}
