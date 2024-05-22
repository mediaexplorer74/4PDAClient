// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.KeyboardPageCompensation
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Windows.UI.Xaml.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public class KeyboardPageCompensation : DependencyObject, IDisposable
  {
    private const double LANDSCAPE_SHIFT = 259.0;
    private const double KEYBOARD_BAR_HEIGHT = 69.0;
    private const double PORTRAIT_SHIFT = 339.0;
    private const double APP_BAR_HEIGHT = 72.0;
    public static readonly DependencyProperty TranslateYProperty = DependencyProperty.Register("TranslateY", typeof (double), typeof (KeyboardPageCompensation), new PropertyMetadata((object) 0.0, new System.Windows.PropertyChangedCallback(KeyboardPageCompensation.PropertyChangedCallback)));
    private readonly string[] _keyboardsWithBar = new string[5]
    {
      "Text",
      "Maps",
      "Search",
      "Formula",
      "Chat"
    };
    private FrameworkElement _focusedElement;
    private bool _keyboardIsOpen;
    private readonly PhoneApplicationFrame _frame;
    private readonly ScrollViewer _scrollViewer;
    private TranslateTransform _translateTransform;
    private double _currentKeyboardHeight;
    private VerticalAlignment _verticalAlignment;
    private ClipboardMonitor _clipboardMonitor;
    private double _preFocusHeight;
    private bool _hasOpaqueAppbar;

    public KeyboardPageCompensation(PhoneApplicationFrame frame, ScrollViewer scrollViewer)
    {
      PhoneApplicationPage content = ((ContentControl) frame).Content as PhoneApplicationPage;
      this._hasOpaqueAppbar = content.ApplicationBar != null && content.ApplicationBar.Opacity < 1.0;
      this._frame = frame;
      this._scrollViewer = scrollViewer;
      ((FrameworkElement) this._scrollViewer).SizeChanged += new SizeChangedEventHandler(this.ScrollViewerSizeChanged);
      this._verticalAlignment = ((FrameworkElement) this._scrollViewer).VerticalAlignment;
      this.BindToTranslateY();
      ((Control) ((ContentControl) frame).Content).Focus();
      this._focusedElement = this.GetFocusedElement();
      ((UIElement) this._focusedElement).LostFocus += new RoutedEventHandler(this.FocusedElementOnLostFocus);
      bool flag;
      try
      {
        flag = !Clipboard.ContainsText();
      }
      catch (Exception ex)
      {
        flag = true;
      }
      if (!flag)
        return;
      this._clipboardMonitor = new ClipboardMonitor();
      this._clipboardMonitor.ClipboardTextChanged += new Action(this.ChangeScrollHeight);
    }

    private void ChangeScrollHeight()
    {
      double keyboardHeight = this.GetKeyboardHeight();
      double num = keyboardHeight - this._currentKeyboardHeight;
      this._currentKeyboardHeight = keyboardHeight;
      ScrollViewer scrollViewer = this._scrollViewer;
      ((FrameworkElement) scrollViewer).Height = ((FrameworkElement) scrollViewer).Height - num;
    }

    private void BindToTranslateY()
    {
      if (!(((UIElement) this._frame).RenderTransform is TransformGroup renderTransform))
        return;
      this._translateTransform = (TranslateTransform) ((PresentationFrameworkCollection<Transform>) renderTransform.Children)[0];
      BindingOperations.SetBinding((DependencyObject) this, KeyboardPageCompensation.TranslateYProperty, (BindingBase) new Binding("Y")
      {
        Source = (object) this._translateTransform
      });
    }

    private bool IsSIPOwner(FrameworkElement control)
    {
      switch (control)
      {
        case TextBox _:
        case PasswordBox _:
          return true;
        default:
          return control is RadPasswordBox;
      }
    }

    private void FocusedElementOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement focusedElement1 = this.GetFocusedElement();
      FrameworkElement focusedElement2 = this._focusedElement;
      ((UIElement) focusedElement2).LostFocus -= new RoutedEventHandler(this.FocusedElementOnLostFocus);
      this._focusedElement = focusedElement1;
      ((UIElement) this._focusedElement).LostFocus += new RoutedEventHandler(this.FocusedElementOnLostFocus);
      if (this.IsSIPOwner(focusedElement1))
      {
        this.ResizePage();
      }
      else
      {
        if (!this.IsSIPOwner(focusedElement2))
          return;
        this.ResetScrollViewerHeight();
      }
    }

    private void ResetScrollViewerHeight()
    {
      if (!this._keyboardIsOpen)
        return;
      this.AnimateScrollViewerHeightTo(this._preFocusHeight, (Action) (() =>
      {
        ((FrameworkElement) this._scrollViewer).Height = double.NaN;
        ((FrameworkElement) this._scrollViewer).VerticalAlignment = this._verticalAlignment;
      }));
      this._keyboardIsOpen = false;
    }

    private void ResizePage()
    {
      if (this._keyboardIsOpen)
      {
        this.ChangeScrollHeight();
      }
      else
      {
        this._currentKeyboardHeight = this.GetKeyboardHeight();
        this._preFocusHeight = ((FrameworkElement) this._scrollViewer).ActualHeight;
        double newHeight = (((ContentControl) this._frame).Content as FrameworkElement).ActualHeight - ((UIElement) this._scrollViewer).TransformToVisual((UIElement) this._frame).Transform(new Point(0.0, 0.0)).Y - this._currentKeyboardHeight;
        ((FrameworkElement) this._scrollViewer).VerticalAlignment = (VerticalAlignment) 0;
        this.AnimateScrollViewerHeightTo(newHeight);
      }
      this._keyboardIsOpen = true;
    }

    private void AnimateScrollViewerHeightTo(double newHeight, Action clb = null)
    {
      PowerEase powerEase1 = new PowerEase();
      powerEase1.Power = 3.0;
      ((EasingFunctionBase) powerEase1).EasingMode = newHeight > ((FrameworkElement) this._scrollViewer).ActualHeight ? (EasingMode) 1 : (EasingMode) 0;
      PowerEase powerEase2 = powerEase1;
      Storyboard storyboard = new Storyboard();
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.From = new double?(((FrameworkElement) this._scrollViewer).ActualHeight);
      doubleAnimation1.To = new double?(newHeight);
      doubleAnimation1.EasingFunction = (IEasingFunction) powerEase2;
      ((Timeline) doubleAnimation1).Duration = new Duration(TimeSpan.FromMilliseconds(250.0));
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTarget((Timeline) storyboard, (DependencyObject) this._scrollViewer);
      Storyboard.SetTargetProperty((Timeline) storyboard, new PropertyPath("Height", new object[0]));
      ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
      ((Timeline) storyboard).Completed += (EventHandler) ((param0, param1) =>
      {
        if (clb == null)
          return;
        clb();
      });
      storyboard.Begin();
    }

    private void ScrollViewerSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (!this.IsSIPOwner(this._focusedElement))
        return;
      GeneralTransform visual = ((UIElement) this._focusedElement).TransformToVisual((UIElement) ((ContentControl) this._scrollViewer).Content);
      Point point1 = visual.Transform(new Point(0.0, this._focusedElement.ActualHeight));
      Point point2 = visual.Transform(new Point(0.0, 0.0));
      double num1 = point1.Y - this._scrollViewer.VerticalOffset;
      if (point2.Y - this._scrollViewer.VerticalOffset < 0.0)
      {
        this._scrollViewer.ScrollToVerticalOffset(point2.Y);
      }
      else
      {
        double num2 = num1 - e.NewSize.Height;
        if (this._hasOpaqueAppbar)
          num2 += 72.0;
        if (num2 <= 0.0)
          return;
        this._scrollViewer.ScrollToVerticalOffset(this._scrollViewer.VerticalOffset + num2);
      }
    }

    private double GetKeyboardHeight()
    {
      return (this._frame.Orientation != 5 ? 259.0 : 339.0) + this.GetKeyboardBarHeight();
    }

    private double GetKeyboardBarHeight()
    {
      if (this._focusedElement is TextBox focusedElement)
      {
        if (focusedElement.InputScope != null)
        {
          if (((IEnumerable<string>) this._keyboardsWithBar).Contains<string>(((InputScopeName) focusedElement.InputScope.Names[0]).NameValue.ToString()))
            return 69.0;
        }
      }
      try
      {
        if (Clipboard.ContainsText())
          return 69.0;
      }
      catch
      {
        return 0.0;
      }
      return 0.0;
    }

    private FrameworkElement GetFocusedElement()
    {
      FrameworkElement focusedElement = (FrameworkElement) FocusManager.GetFocusedElement();
      if (focusedElement == null)
      {
        ((Control) this._scrollViewer).Focus();
        focusedElement = (FrameworkElement) this._scrollViewer;
      }
      return focusedElement;
    }

    private static void PropertyChangedCallback(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      ((KeyboardPageCompensation) dependencyObject)._translateTransform.Y = 0.0;
    }

    public void Dispose()
    {
      ((FrameworkElement) this._scrollViewer).SizeChanged -= new SizeChangedEventHandler(this.ScrollViewerSizeChanged);
      if (this._focusedElement != null)
        ((UIElement) this._focusedElement).LostFocus -= new RoutedEventHandler(this.FocusedElementOnLostFocus);
      if (this._clipboardMonitor != null)
      {
        this._clipboardMonitor.ClipboardTextChanged -= new Action(this.ChangeScrollHeight);
        this._clipboardMonitor.Dispose();
      }
      this.ClearValue(KeyboardPageCompensation.TranslateYProperty);
      this.ResetScrollViewerHeight();
    }
  }
}
