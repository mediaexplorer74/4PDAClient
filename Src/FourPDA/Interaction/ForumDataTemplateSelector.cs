// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.ForumDataTemplateSelector
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using ForPDA.AppServices.DataModels;
using System;
using System.Windows;
using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA.Interaction
{
  public class ForumDataTemplateSelector : DataTemplateSelector
  {
    public DataTemplate TopicTemplate { get; set; }

    public DataTemplate ForumTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
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
