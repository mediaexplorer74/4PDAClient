// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.SimpleDataTemplateSelector
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public class SimpleDataTemplateSelector : ContentControl
  {
    public SimpleDataTemplateSelector()
    {
      ((Control) this).HorizontalContentAlignment = (HorizontalAlignment) 3;
      ((FrameworkElement) this).HorizontalAlignment = (HorizontalAlignment) 3;
    }

    public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      return (DataTemplate) null;
    }

    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      base.OnContentChanged(oldContent, newContent);
      this.ContentTemplate = this.SelectTemplate(newContent, (DependencyObject) this);
    }
  }
}
