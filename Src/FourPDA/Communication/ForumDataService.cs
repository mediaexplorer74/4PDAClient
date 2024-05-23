// ForPDA.Communication.ForumDataService

using ForPDA.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
//using System.Web;


#nullable disable
namespace ForPDA.Communication
{
  public class ForumDataService
  {
    private string FORUM_BASE_URI = "http://4pda.to/forum/lofiversion/index.php";
    private string SUBFORUM_URI_FMT = "http://4pda.to/forum/index.php?showforum={0}";
    private readonly HttpCommunicator _httpCommunicator;
    private ForumModel _rootForum;

    public ForumDataService(HttpCommunicator httpCommunicator)
    {
      this._httpCommunicator = httpCommunicator;
    }

    public async Task<ForumModel> LoadForumHierarchyAsync()
    {
      if (this._rootForum != null)
        return this._rootForum;
      string forumHtml = await this._httpCommunicator.GetStringAsync(this.FORUM_BASE_URI);
      this._rootForum = this.ParseForum(forumHtml);
      return this._rootForum;
    }

    public async Task<List<ForumTopicModel>> LoadTopicsAsync(string forumId)
    {
      string uri = string.Format(this.SUBFORUM_URI_FMT, (object) forumId);
      string topicsHtml = await this._httpCommunicator.GetStringAsync(uri);
      return this.ParseTopics(topicsHtml);
    }

    internal List<ForumTopicModel> ParseTopics(string topicsHtml)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(topicsHtml);
      HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//div[@class='topic_title']");
      if (htmlNodeCollection == null)
        return new List<ForumTopicModel>();
      List<ForumTopicModel> topics = new List<ForumTopicModel>();
      IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>) htmlNodeCollection).GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          HtmlNode current = enumerator.Current;
          HtmlNode nextSibling = current.NextSibling;
        //TODO
        topics.Add(new ForumTopicModel(/*HttpUtility.HtmlDecode(current.SelectSingleNode("a[1]").InnerHtml)*/default)
        {
            AuthorName = default,//HttpUtility.HtmlDecode(nextSibling.SelectSingleNode("a[last()]").InnerText),
            LastMessageTime = default//HttpUtility.HtmlDecode(((string) nextSibling.LastChild.InnerText).Trim())
        });
        }
      }
      finally
      {
        enumerator?.Dispose();
      }
      return topics;
    }

    internal ForumModel ParseForum(string htmlString)
    {
      RootForumModel forum = new RootForumModel();
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(htmlString);
      List<ForumModel>.Enumerator enumerator = ForumDataService.ParseRootForumNodes(htmlDocument.GetElementbyId("ipbcontent").ChildNodes.FindFirst("ul"), forum.Id).GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          ForumModel current = enumerator.Current;
          forum.Children.Add(current);
        }
      }
      finally
      {
        enumerator.Dispose();
      }
      return (ForumModel) forum;
    }

    private static List<ForumModel> ParseRootForumNodes(HtmlNode forumNodes, string parentId)
    {
      List<ForumModel> rootForumNodes = new List<ForumModel>();
      ForumModel forumModel = (ForumModel) null;
      IEnumerator<HtmlNode> enumerator1 = forumNodes.ChildNodes.Where<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name != "#text")).GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
        {
          HtmlNode current1 = enumerator1.Current;
          if (current1.Name == "ul")
          {
            List<ForumModel>.Enumerator enumerator2 = ForumDataService.ParseForumNodes(current1, forumModel.Id).GetEnumerator();
            try
            {
              while (enumerator2.MoveNext())
              {
                ForumModel current2 = enumerator2.Current;
                forumModel.Children.Add(current2);
              }
            }
            finally
            {
              enumerator2.Dispose();
            }
          }
          else if (current1.Name == "li")
          {
            forumModel = ForumDataService.CreateForumFromNode(current1, parentId);
            rootForumNodes.Add(forumModel);
          }
        }
      }
      finally
      {
        enumerator1?.Dispose();
      }
      return rootForumNodes;
    }

    private static List<ForumModel> ParseForumNodes(HtmlNode forumNodes, string parentId)
    {
      List<ForumModel> forumNodes1 = new List<ForumModel>();
      ForumModel forumModel = (ForumModel) null;
      IEnumerator<HtmlNode> enumerator1 = forumNodes.ChildNodes.Where<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name != "#text")).GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
        {
          HtmlNode current1 = enumerator1.Current;
          if (current1.ChildNodes.Count == 1)
          {
            List<ForumModel>.Enumerator enumerator2 = ForumDataService.ParseForumNodes(current1.ChildNodes.Single<HtmlNode>(), forumModel.Id).GetEnumerator();
            try
            {
              while (enumerator2.MoveNext())
              {
                ForumModel current2 = enumerator2.Current;
                forumModel.Children.Add(current2);
              }
            }
            finally
            {
              enumerator2.Dispose();
            }
          }
          else
          {
            forumModel = ForumDataService.CreateForumFromNode(current1, parentId);
            forumNodes1.Add(forumModel);
          }
        }
      }
      finally
      {
        enumerator1?.Dispose();
      }
      return forumNodes1;
    }

    private static ForumModel CreateForumFromNode(HtmlNode child, string parentId)
    {
      HtmlNode firstChild = child.FirstChild;
      return new ForumModel(Regex.Match(firstChild.OuterHtml, "\\?f(\\d+)\\.html").Groups[1].Value, firstChild.InnerText, parentId);
    }
  }
}
