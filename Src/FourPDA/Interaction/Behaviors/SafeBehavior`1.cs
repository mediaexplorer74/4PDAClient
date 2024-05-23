// FourPDA.Interaction.Behaviors.SafeBehavior`1
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

//using Microsoft.Xaml.Interactivity;
using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
//using System.Windows.Interactivity;
//using System.Windows.Navigation;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
    /*
  public abstract class SafeBehavior<T> : Behavior<T> where T : FrameworkElement
  {
    protected Frame ParentPage;
    private Uri pageSource;

    protected SafeBehavior() => this.IsCleanedUp = true;

    protected bool ListenToPageBackEvent { get; set; }

    protected override void OnAttached()
    {
      base.OnAttached();
      this.InitBehavior();
    }

    protected void InitBehavior()
    {
      if (!this.IsCleanedUp)
        return;
      this.IsCleanedUp = false;
      this.AssociatedObject.Loaded += new RoutedEventHandler(this.AssociatedObjectLoaded);
      this.AssociatedObject.Unloaded += new RoutedEventHandler(this.AssociatedObjectUnloaded);
    }

    private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
    {
      if (this.ParentPage == null && this.ListenToPageBackEvent)
      {
                this.ParentPage = default;//Application.Current as Frame;
                this.pageSource = default;//((Frame) this.ParentPage).CurrentSource;
        ((Frame) this.ParentPage).Navigated += new NavigatedEventHandler(new WeakEventListener<SafeBehavior<T>, Frame, NavigationEventArgs>(this, this.ParentPage)
        {
          OnEventAction = (Action<SafeBehavior<T>, object, NavigationEventArgs>) ((behavior, o, arg3) => behavior.ParentPageNavigated(o, arg3))
        }.OnEvent);
      }
      this.OnSetup();
    }

    private void ParentPageNavigated(object sender, NavigationEventArgs e)
    {
      if (this.IsNavigatingBackToBehaviorPage(e) && this.IsCleanedUp)
        this.InitBehavior();
      this.OnParentPageNavigated(sender, e);
    }

    protected virtual void OnParentPageNavigated(object sender, NavigationEventArgs e)
    {
    }

    protected virtual void OnSetup()
    {
    }

    protected bool IsNavigatingBackToBehaviorPage(NavigationEventArgs e)
    {
      return e.NavigationMode == NavigationMode.Back && e.Uri.Equals((object) this.pageSource);
    }

    protected bool IsCleanedUp { get; private set; }

    private void Cleanup()
    {
      if (this.IsCleanedUp)
        return;
      this.AssociatedObject.Loaded -= new RoutedEventHandler(this.AssociatedObjectLoaded);
      this.AssociatedObject.Unloaded -= new RoutedEventHandler(this.AssociatedObjectUnloaded);
      this.OnCleanup();
      this.IsCleanedUp = true;
    }

    protected override void OnDetaching()
    {
      this.Cleanup();
      base.OnDetaching();
    }

    private void AssociatedObjectUnloaded(object sender, RoutedEventArgs e) => this.Cleanup();

    protected virtual void OnCleanup()
    {
    }
   
  }*/
}
