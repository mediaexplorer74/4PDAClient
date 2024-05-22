// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.Behaviors.ListBoxBehavior
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class ListBoxBehavior
  {
    public static readonly DependencyProperty DisableSelectionProperty;
    public static readonly DependencyProperty DisableScrollingProperty = DependencyProperty.RegisterAttached("DisableScrolling", typeof (bool), typeof (ListBoxBehavior), new PropertyMetadata((object) false, new PropertyChangedCallback(ListBoxBehavior.OnScrollDisablingChanged)));

    static ListBoxBehavior()
    {
      ListBoxBehavior.DisableSelectionProperty = DependencyProperty.RegisterAttached("DisableSelection", typeof (bool), typeof (ListBoxBehavior), new PropertyMetadata((object) false, new PropertyChangedCallback(ListBoxBehavior.OnSelectionDisablingChanged)));
    }

    public static void SetDisableSelection(UIElement element, bool value)
    {
      ((DependencyObject) element).SetValue(ListBoxBehavior.DisableSelectionProperty, (object) value);
    }

    public static bool GetDisableSelection(UIElement element)
    {
      return (bool) ((DependencyObject) element).GetValue(ListBoxBehavior.DisableSelectionProperty);
    }

    public static void SetDisableScrolling(RadDataBoundListBox element, bool value)
    {
      ((DependencyObject) element).SetValue(ListBoxBehavior.DisableScrollingProperty, (object) value);
    }

    public static bool GetDisableScrolling(RadDataBoundListBox element)
    {
      return (bool) ((DependencyObject) element).GetValue(ListBoxBehavior.DisableScrollingProperty);
    }

    private static void OnSelectionDisablingChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      ((RadDataBoundListBox) sender).SelectionChanging += (EventHandler<SelectionChangingEventArgs>) ((o, args) => args.Cancel = true);
    }

    private static void OnScrollDisablingChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      RadDataBoundListBox lb = (RadDataBoundListBox) sender;
      ((FrameworkElement) lb).Loaded += (RoutedEventHandler) ((o, args) =>
      {
        ScrollViewer visualDescendant = ElementTreeHelper.FindVisualDescendant<ScrollViewer>((DependencyObject) lb);
        if (visualDescendant == null)
          return;
        visualDescendant.VerticalScrollBarVisibility = (ScrollBarVisibility) 0;
        visualDescendant.HorizontalScrollBarVisibility = (ScrollBarVisibility) 0;
      });
    }
  }
}
