// FourPDA.Interaction.WeakEventListener`3

using System;

#nullable disable
namespace FourPDA.Interaction
{
  public class WeakEventListener<TInstance, TSource, TEventArgs> where TInstance : class
  {
    private WeakReference _weakInstance;
    private WeakReference _weakSource;
    private Action<TInstance, object, TEventArgs> _onEventAction;
    private Action<WeakEventListener<TInstance, TSource, TEventArgs>, TSource> _onDetachAction;

    public WeakEventListener(TInstance instance, TSource source)
    {
      if ((object) instance == null)
        throw new ArgumentNullException(nameof (instance));
      if ((object) source == null)
        throw new ArgumentNullException(nameof (source));
      this._weakInstance = new WeakReference((object) instance);
      this._weakSource = new WeakReference((object) source);
    }

    public Action<TInstance, object, TEventArgs> OnEventAction
    {
      get => this._onEventAction;
      set
      {
        if (value != null/* && !value.Method.IsStatic*/)
          throw new ArgumentException(
              "OnEventAction method must be static otherwise the event WeakEventListner class does not prevent memory leaks.");
        this._onEventAction = value;
      }
    }

    internal Action<WeakEventListener<TInstance, TSource, TEventArgs>, TSource> OnDetachAction
    {
      get => this._onDetachAction;
      set
      {
        if (value != null/* && !value.Method.IsStatic*/)
          throw new ArgumentException(
              "OnDetachAction method must be static otherwise the event WeakEventListner cannot guarantee to unregister the handler.");
        this._onDetachAction = value;
      }
    }

    public void OnEvent(object source, TEventArgs eventArgs)
    {
      TInstance target = (TInstance) this._weakInstance.Target;
      if ((object) target != null)
      {
        if (this.OnEventAction == null)
          return;
        this.OnEventAction(target, source, eventArgs);
      }
      else
        this.Detach();
    }

    public void Detach()
    {
      TSource target = (TSource) this._weakSource.Target;
      if (this.OnDetachAction == null || (object) target == null)
        return;
      this.OnDetachAction(this, target);
      this.OnDetachAction = (Action<WeakEventListener<TInstance, TSource, TEventArgs>, TSource>) null;
    }
  }
}
