// FourPDA.Interaction.Behaviors.TextBoxUpdateBindingBehavior

using System;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class TextBoxUpdateBindingBehavior //: SafeBehavior<TextBox>
  {
        public TextBoxUpdateBindingBehavior()
        {
            //this.ListenToPageBackEvent = true;
        }

        protected /*override*/ void OnSetup()
    {
     // base.OnSetup();
     // this.AssociatedObject.TextChanged += new TextChangedEventHandler(this.OnTextChanged);
    }

    protected /*override*/ void OnCleanup()
    {
     // base.OnCleanup();
     // this.AssociatedObject.TextChanged -= new TextChangedEventHandler(this.OnTextChanged);
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      //if (this.AssociatedObject == null)
      //  return;
      //((FrameworkElement) this.AssociatedObject).GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
    }
  }
}
