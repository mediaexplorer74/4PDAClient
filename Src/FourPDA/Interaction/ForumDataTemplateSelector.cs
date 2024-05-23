// FourPDA.Interaction.ForumDataTemplateSelector

using ForPDA.AppServices.DataModels;
using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public class ForumDataTemplateSelector : DataTemplateSelector
  {
    public DataTemplate TopicTemplate { get; set; }

    public DataTemplate ForumTemplate { get; set; }

    public /*override*/ DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      switch (item)
      {
        case TopicDataModel _:
          return this.TopicTemplate;
        case ForumDataModel _:
          return this.ForumTemplate;
        default:
          throw new NotSupportedException();
      }
    }
  }
}
