// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.Behaviors.TextBoxUpdateBindingBehavior
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class TextBoxUpdateBindingBehavior : SafeBehavior<TextBox>
  {
    public TextBoxUpdateBindingBehavior() => this.ListenToPageBackEvent = true;

    protected override void OnSetup()
    {
      base.OnSetup();
      this.AssociatedObject.TextChanged += new TextChangedEventHandler(this.OnTextChanged);
    }

    protected override void OnCleanup()
    {
      base.OnCleanup();
      this.AssociatedObject.TextChanged -= new TextChangedEventHandler(this.OnTextChanged);
    }

    private void OnTextChanged(object sender, RoutedEventArgs e)
    {
      if (this.AssociatedObject == null)
        return;
      ((FrameworkElement) this.AssociatedObject).GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
    }
  }
}
