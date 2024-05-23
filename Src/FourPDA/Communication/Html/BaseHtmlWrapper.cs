// ForPDA.Communication.Html.BaseHtmlWrapper

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
