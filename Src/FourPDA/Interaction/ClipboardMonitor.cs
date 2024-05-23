// FourPDA.Interaction.ClipboardMonitor

using System;
using System.Windows;
using Windows.UI.Xaml;
//using System.Windows.Threading;

#nullable disable
namespace FourPDA.Interaction
{
  internal class ClipboardMonitor : IDisposable
  {
    private readonly DispatcherTimer _timer;

    public event Action ClipboardTextChanged = () => { };

    public ClipboardMonitor()
    {
      //this._timer = new DispatcherTimer();
      //this._timer.Interval = TimeSpan.FromMilliseconds(250.0);
      //this._timer.Tick += new EventHandler(this.TimerOnTick);
      //this._timer.Start();
    }

    private void TimerOnTick(object sender, EventArgs eventArgs)
    {
      try
      {
       // if (!Clipboard.ContainsText())
       //   return;
        this.ClipboardTextChanged();
        this._timer.Stop();
      }
      catch (Exception ex)
      {
      }
    }

    public void Dispose()
    {
      //this._timer.Stop();
      //this._timer.Tick -= new EventHandler(this.TimerOnTick);
    }
  }
}
