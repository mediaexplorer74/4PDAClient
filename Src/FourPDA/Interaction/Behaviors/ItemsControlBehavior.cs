// FourPDA.Interaction.Behaviors.ItemsControlBehavior

using System.Collections.Specialized;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class ItemsControlBehavior //: SafeBehavior<ItemsControl>
  {
    private ScrollViewer _scrollViewer;
    private double _initialHeight;

    private void OnCollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
      this.ApplyBehavior();
    }

    private void ApplyBehavior()
    {
      if (this._scrollViewer == null)
        return;
      //((UIElement) this.AssociatedObject).UpdateLayout();
      //if (((FrameworkElement) this.AssociatedObject).ActualHeight > this._initialHeight)
      //  this._scrollViewer.VerticalScrollBarVisibility = (ScrollBarVisibility) 1;
      //else
      //  this._scrollViewer.VerticalScrollBarVisibility = (ScrollBarVisibility) 0;
    }

    protected /*override*/ void OnSetup()
    {
      //base.OnSetup();
      //((INotifyCollectionChanged) this.AssociatedObject.Items).CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
      //this._initialHeight = ((FrameworkElement) this.AssociatedObject).ActualHeight;
      //this._scrollViewer = ((FrameworkElement) this.AssociatedObject).Parent as ScrollViewer;
      this.ApplyBehavior();
    }

    protected /*override*/ void OnCleanup()
    {
      //base.OnCleanup();
      //((INotifyCollectionChanged) this.AssociatedObject.Items).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
    }
  }
}
