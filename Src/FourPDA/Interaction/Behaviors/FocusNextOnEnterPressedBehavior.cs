// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.Behaviors.FocusNextOnEnterPressedBehavior
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using Microsoft.Phone.Controls;
using System;
using System.Linq;
using System.Windows;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class FocusNextOnEnterPressedBehavior : SafeBehavior<Control>
  {
    public static readonly DependencyProperty NextControlNameProperty = DependencyProperty.Register(nameof (NextControlName), typeof (string), typeof (FocusNextOnEnterPressedBehavior), (PropertyMetadata) null);

    public string NextControlName
    {
      get => (string) this.GetValue(FocusNextOnEnterPressedBehavior.NextControlNameProperty);
      set => this.SetValue(FocusNextOnEnterPressedBehavior.NextControlNameProperty, (object) value);
    }

    public FocusNextOnEnterPressedBehavior() => this.ListenToPageBackEvent = true;

    protected override void OnSetup()
    {
      base.OnSetup();
      ((UIElement) this.AssociatedObject).KeyUp += new KeyEventHandler(this.OnKeyUp);
    }

    protected override void OnCleanup()
    {
      base.OnCleanup();
      ((UIElement) this.AssociatedObject).KeyUp -= new KeyEventHandler(this.OnKeyUp);
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key != 3)
        return;
      PhoneApplicationPage phoneApplicationPage = ((DependencyObject) this.AssociatedObject).Ancestors<PhoneApplicationPage>().First<PhoneApplicationPage>();
      if (phoneApplicationPage == null)
        return;
      if (string.IsNullOrEmpty(this.NextControlName))
      {
        ((Control) phoneApplicationPage).Focus();
      }
      else
      {
        if (!(((FrameworkElement) phoneApplicationPage).FindName(this.NextControlName) is Control name))
          throw new InvalidOperationException(string.Format("Control '{0}' couldn't be found", (object) this.NextControlName));
        name.Focus();
      }
    }
  }
}
