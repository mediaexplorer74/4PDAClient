// FourPDA.Interaction.AnimationHelper

using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public static class AnimationHelper
  {
    public static void SetTileAnimation(Page page, ListBox radDataBoundListBox)
    {
      new AnimationHelper.AnimatedObject(page, radDataBoundListBox).Setup();
    }

    private class AnimatedObject
    {
      private readonly Page _page;
      private readonly ListBox _list;

      public AnimatedObject(Page page, ListBox list)
      {
        this._page = page;
        this._list = list;
      }

      public void Setup()
      {
        //((DependencyObject) this._page).SetValue(RadTileAnimation.ContainerToAnimateProperty, (object) this._list);
        //((DependencyObject) this._page).SetValue(RadSlideContinuumAnimation.HeaderElementProperty, (object) this._list);
        ((FrameworkElement) this._page).Loaded += new RoutedEventHandler(this.OnLoaded);
        ((FrameworkElement) this._page).Unloaded += new RoutedEventHandler(this.OnUnloaded);
        //this._list.SelectionChanging += new EventHandler<SelectionChangingEventArgs>(this.OnListSelectionChanged);
      }

      private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
      {
        ((FrameworkElement) this._page).Unloaded -= new RoutedEventHandler(this.OnUnloaded);
        //InteractionEffectManager.AllowedTypes.Remove(typeof (ListBoxItem));
      }

      private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
      {
        ((FrameworkElement) this._page).Loaded -= new RoutedEventHandler(this.OnLoaded);
        //InteractionEffectManager.AllowedTypes.Add(typeof (ListBoxItem));
      }

      private void OnListSelectionChanged(object sender, EventArgs e)
      {
        //if (e.AddedItems.Count == 0)
        //  return;
        //ListBoxItem containerForItem = this._list.GetContainerForItem(e.AddedItems[0]) as RadDataBoundListBoxItem;
        //((DependencyObject) this._page).SetValue(RadTileAnimation.ElementToDelayProperty, (object) containerForItem);
      }
    }
  }
}
