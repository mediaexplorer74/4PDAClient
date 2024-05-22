// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.ClipboardMonitor
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Windows;
using System.Windows.Threading;

#nullable disable
namespace FourPDA.Interaction
{
  internal class ClipboardMonitor : IDisposable
  {
    private readonly DispatcherTimer _timer;

    public event Action ClipboardTextChanged = () => { };

    public ClipboardMonitor()
    {
      this._timer = new DispatcherTimer();
      this._timer.Interval = TimeSpan.FromMilliseconds(250.0);
      this._timer.Tick += new EventHandler(this.TimerOnTick);
      this._timer.Start();
    }

    private void TimerOnTick(object sender, EventArgs eventArgs)
    {
      try
      {
        if (!Clipboard.ContainsText())
          return;
        this.ClipboardTextChanged();
        this._timer.Stop();
      }
      catch (Exception ex)
      {
      }
    }

    public void Dispose()
    {
      this._timer.Stop();
      this._timer.Tick -= new EventHandler(this.TimerOnTick);
    }
  }
}
