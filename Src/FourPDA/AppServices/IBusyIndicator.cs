// ForPDA.AppServices.IBusyIndicator

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
