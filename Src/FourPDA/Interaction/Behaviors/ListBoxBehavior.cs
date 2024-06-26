﻿// FourPDA.Interaction.Behaviors.ListBoxBehavior

using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class ListBoxBehavior
  {
    public static readonly DependencyProperty DisableSelectionProperty;
    public static readonly DependencyProperty DisableScrollingProperty 
            = DependencyProperty.RegisterAttached("DisableScrolling", typeof (bool), 
                typeof (ListBoxBehavior), new PropertyMetadata((object) false,
                    new PropertyChangedCallback(ListBoxBehavior.OnScrollDisablingChanged)));

    static ListBoxBehavior()
    {
      ListBoxBehavior.DisableSelectionProperty = DependencyProperty.RegisterAttached("DisableSelection",
          typeof (bool), typeof (ListBoxBehavior), new PropertyMetadata((object) false,
          new PropertyChangedCallback(ListBoxBehavior.OnSelectionDisablingChanged)));
    }

    public static void SetDisableSelection(UIElement element, bool value)
    {
      ((DependencyObject) element).SetValue(ListBoxBehavior.DisableSelectionProperty, (object) value);
    }

    public static bool GetDisableSelection(UIElement element)
    {
      return (bool) ((DependencyObject) element).GetValue(ListBoxBehavior.DisableSelectionProperty);
    }

    public static void SetDisableScrolling(ListBox element, bool value)
    {
      ((DependencyObject) element).SetValue(ListBoxBehavior.DisableScrollingProperty, (object) value);
    }

    public static bool GetDisableScrolling(ListBox element)
    {
      return (bool) ((DependencyObject) element).GetValue(ListBoxBehavior.DisableScrollingProperty);
    }

    private static void OnSelectionDisablingChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      //((ListBox) sender).SelectionChanging += (EventHandler<EventArgs>) ((o, args) => args.Cancel = true);
    }

    private static void OnScrollDisablingChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      ListBox lb = (ListBox) sender;
      ((FrameworkElement) lb).Loaded += (RoutedEventHandler) ((o, args) =>
      {
          ScrollViewer visualDescendant = default;// ElementTreeHelper.FindVisualDescendant<ScrollViewer>((DependencyObject) lb);
        if (visualDescendant == null)
          return;
        visualDescendant.VerticalScrollBarVisibility = (ScrollBarVisibility) 0;
        visualDescendant.HorizontalScrollBarVisibility = (ScrollBarVisibility) 0;
      });
    }
  }
}
