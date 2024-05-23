// FourPDA.Interaction.Behaviors.PassBoxUpdateBindingBehavior

using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class PassBoxUpdateBindingBehavior //: SafeBehavior<PasswordBox>
  {
        public PassBoxUpdateBindingBehavior()
        { 
            //this.ListenToPageBackEvent = true; 
        }

    protected /*override*/ void OnSetup()
    {
     // base.OnSetup();
      //this.AssociatedObject.PasswordChanged += new EventHandler(this.OnTextChanged);
    }

    protected /*override*/ void OnCleanup()
    {
      //base.OnCleanup();
      //this.AssociatedObject.PasswordChanged -= new EventHandler(this.OnTextChanged);
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      //if (this.AssociatedObject == null)
      //  return;
      //((FrameworkElement) this.AssociatedObject).GetBindingExpression(PasswordBox.PasswordProperty)?.UpdateSource();
    }
  }
}
