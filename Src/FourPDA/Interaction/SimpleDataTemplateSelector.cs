// FourPDA.Interaction.SimpleDataTemplateSelector

using System.Windows;
using Windows.UI.Xaml;
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
