// Decompiled with JetBrains decompiler
// Type: ForPDA.Communication.Html.NewsHtmlProcessor
// Assembly: ForPDA.Communication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7621B702-77AB-4A6D-A66D-A001F234DCA5
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.Communication.dll

#nullable disable
namespace ForPDA.Communication.Html
{
  public class NewsHtmlProcessor : BaseHtmlWrapper
  {
    private const string DARK_CSS = "body { \r\n    font-family:Segoe WP;\r\n    font-size:1.5em;\r\n    background-color:black;\r\n    color:white;\r\n}\r\n";
    private const string LIGHT_CSS = "body { \r\n    font-family:Segoe WP;\r\n    font-size:1.5em;\r\n    background-color:white;\r\n    color:black;\r\n}\r\n";

    public NewsHtmlProcessor(string htmlString)
      : base(htmlString)
    {
    }

    public string WrapDetails(bool useDarkCss)
    {
      return this.WrapToHtmlWithCss(this.RemoveLinks(this.ExtractBlock("<h2><a href=", "<div class=\"postmetadata\">")), useDarkCss ? "body { \r\n    font-family:Segoe WP;\r\n    font-size:1.5em;\r\n    background-color:black;\r\n    color:white;\r\n}\r\n" : "body { \r\n    font-family:Segoe WP;\r\n    font-size:1.5em;\r\n    background-color:white;\r\n    color:black;\r\n}\r\n");
    }
  }
}
