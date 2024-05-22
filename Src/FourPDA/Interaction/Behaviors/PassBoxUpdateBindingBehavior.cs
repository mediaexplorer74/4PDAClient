// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.Behaviors.PassBoxUpdateBindingBehavior
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Windows;
using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction.Behaviors
{
  public class PassBoxUpdateBindingBehavior : SafeBehavior<RadPasswordBox>
  {
    public PassBoxUpdateBindingBehavior() => this.ListenToPageBackEvent = true;

    protected override void OnSetup()
    {
      base.OnSetup();
      this.AssociatedObject.PasswordChanged += new EventHandler(this.OnTextChanged);
    }

    protected override void OnCleanup()
    {
      base.OnCleanup();
      this.AssociatedObject.PasswordChanged -= new EventHandler(this.OnTextChanged);
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      if (this.AssociatedObject == null)
        return;
      ((FrameworkElement) this.AssociatedObject).GetBindingExpression(RadPasswordBox.PasswordProperty)?.UpdateSource();
    }
  }
}
