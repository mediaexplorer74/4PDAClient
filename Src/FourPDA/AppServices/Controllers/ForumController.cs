// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.Controllers.ForumController
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using ForPDA.AppServices.DataModels;
using ForPDA.Communication.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace ForPDA.AppServices.Controllers
{
  public class ForumController
  {
    public TopicDataModel CreateDataModel(ForumTopicModel model)
    {
      return new TopicDataModel()
      {
        Name = model.Name,
        AuthorName = model.AuthorName,
        LastMessageTime = model.LastMessageTime
      };
    }

    public ForumDataModel CreateDataModel(ForumModel model)
    {
      return this.CreateDataModel(model, (string) null);
    }

    public ForumDataModel CreateDataModel(ForumModel model, string parentTitle)
    {
      return new ForumDataModel()
      {
        Id = model.Id,
        Title = ForumController.MakeTitle(model, parentTitle),
        HasChildren = Enumerable.Any<ForumModel>((IEnumerable<ForumModel>) model.Children)
      };
    }

    private static string MakeTitle(ForumModel model, string parentTitle)
    {
      return parentTitle != null && ((string) model.Name).StartsWith(parentTitle) ? Regex.Replace(model.Name, string.Format("^{0}[\\s-]*", (object) parentTitle), string.Empty) : model.Name;
    }
  }
}
