// Decompiled with JetBrains decompiler
// Type: ForPDA.Communication.Html.BaseHtmlWrapper
// Assembly: ForPDA.Communication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7621B702-77AB-4A6D-A66D-A001F234DCA5
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.Communication.dll

using System;
using System.Text.RegularExpressions;

#nullable disable
namespace ForPDA.Communication.Html
{
  public class BaseHtmlWrapper
  {
    private const string HTML_PATTERN_FMT = "<html><head> <meta name='viewport' content='width=500px' /><style type=\"text/css\">{0}</style></head><body>{1}</body></html>";
    protected readonly string HtmlString;

    public BaseHtmlWrapper(string htmlString) => this.HtmlString = htmlString;

    protected string ExtractBlock(string startContent, string endContent)
    {
      int startIndex = this.HtmlString.IndexOf(startContent, StringComparison.OrdinalIgnoreCase);
      int num = this.HtmlString.IndexOf(endContent, startIndex, StringComparison.OrdinalIgnoreCase);
      return this.HtmlString.Substring(startIndex, num - startIndex);
    }

    private string ToExtendedASCII(string html)
    {
      string extendedAscii = "";
      foreach (char ch in html.ToCharArray())
      {
        int int32 = Convert.ToInt32(ch);
        extendedAscii = int32 <= (int) sbyte.MaxValue ? extendedAscii + (object) ch : extendedAscii + string.Format("&#{0};", (object) int32);
      }
      return extendedAscii;
    }

    protected string WrapToHtmlWithCss(string contentString, string css)
    {
      return string.Format("<html><head> <meta name='viewport' content='width=500px' /><style type=\"text/css\">{0}</style></head><body>{1}</body></html>", (object) css, (object) this.ToExtendedASCII(contentString));
    }

    protected string RemoveLinks(string contentString)
    {
      return Regex.Replace(contentString, "<a[^>]*>(.*?)</a>", "$1");
    }
  }
}
