// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.AnimationHelper
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public static class AnimationHelper
  {
    public static void SetTileAnimation(Page page, RadDataBoundListBox radDataBoundListBox)
    {
      new AnimationHelper.AnimatedObject(page, radDataBoundListBox).Setup();
    }

    private class AnimatedObject
    {
      private readonly Page _page;
      private readonly RadDataBoundListBox _list;

      public AnimatedObject(Page page, RadDataBoundListBox list)
      {
        this._page = page;
        this._list = list;
      }

      public void Setup()
      {
        ((DependencyObject) this._page).SetValue(RadTileAnimation.ContainerToAnimateProperty, (object) this._list);
        ((DependencyObject) this._page).SetValue(RadSlideContinuumAnimation.HeaderElementProperty, (object) this._list);
        ((FrameworkElement) this._page).Loaded += new RoutedEventHandler(this.OnLoaded);
        ((FrameworkElement) this._page).Unloaded += new RoutedEventHandler(this.OnUnloaded);
        this._list.SelectionChanging += new EventHandler<SelectionChangingEventArgs>(this.OnListSelectionChanged);
      }

      private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
      {
        ((FrameworkElement) this._page).Unloaded -= new RoutedEventHandler(this.OnUnloaded);
        InteractionEffectManager.AllowedTypes.Remove(typeof (RadDataBoundListBoxItem));
      }

      private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
      {
        ((FrameworkElement) this._page).Loaded -= new RoutedEventHandler(this.OnLoaded);
        InteractionEffectManager.AllowedTypes.Add(typeof (RadDataBoundListBoxItem));
      }

      private void OnListSelectionChanged(object sender, SelectionChangingEventArgs e)
      {
        if (e.AddedItems.Count == 0)
          return;
        RadDataBoundListBoxItem containerForItem = this._list.GetContainerForItem(e.AddedItems[0]) as RadDataBoundListBoxItem;
        ((DependencyObject) this._page).SetValue(RadTileAnimation.ElementToDelayProperty, (object) containerForItem);
      }
    }
  }
}
