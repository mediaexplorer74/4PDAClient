// FourPDA.AppServices.Controllers.NewsController

using FourPDA.AppServices.DataModels;
using FourPDA.Communication.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace FourPDA.AppServices.Controllers
{
  public class NewsController
  {
    public TopicDataModel CreateDataModel(NewsTopicModel model)
    {
      return new TopicDataModel()
      {
        Name = model.Name,
        AuthorName = model.AuthorName,
        LastMessageTime = model.LastMessageTime
      };
    }

    public NewsDataModel CreateDataModel(NewsModel model)
    {
      return this.CreateDataModel(model, (string) null);
    }

    public NewsDataModel CreateDataModel(NewsModel model, string parentTitle)
    {
      return new NewsDataModel()
      {
        Id = model.Id,
        Title = NewsController.MakeTitle(model, parentTitle),
        Body = model.Name, // temp
        HasChildren = Enumerable.Any<NewsModel>((IEnumerable<NewsModel>) model.Children)
      };
    }

    private static string MakeTitle(NewsModel model, string parentTitle)
    {
      return parentTitle != null 
                && ((string) model.Name).StartsWith(parentTitle)
                ? Regex.Replace(model.Name, 
                    string.Format("^{0}[\\s-]*", (object) parentTitle), string.Empty) 
                : model.Name;
    }
  }
}
