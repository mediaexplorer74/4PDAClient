// ForPDA.Communication.NewsDataService

using ForPDA.Communication.Html;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Web.Syndication;//using System.ServiceModel.Syndication;
//using MSPToolkit.Encodings;

#nullable disable
namespace ForPDA.Communication
{
  public class NewsDataService
  {
    private const string RSS_URL = "http://4pda.to/feed";
    private const string SITE_URL = "http://4pda.to/";
    private readonly HttpClient _http = new HttpClient();

    public async Task<SyndicationFeed> LoadFeedsAsync()
    {
      HttpResponseMessage response = await this._http.GetAsync("http://4pda.to/feed");
      Stream stream = await response.Content.ReadAsStreamAsync();
      SyndicationFeed syndicationFeed;
      try
      {
        StreamReader streamReader = new StreamReader(stream, Encoding.UTF8); // 1251 ?
        try
        {
          XmlReader reader = XmlReader.Create((TextReader) streamReader);
          try
          {
            //RnD
            syndicationFeed = default;//SyndicationFeed.Load(reader);
          }
          finally
          {
            ((IDisposable) reader)?.Dispose();
          }
        }
        finally
        {
          streamReader?.Dispose();
        }
      }
      finally
      {
        ((IDisposable) stream)?.Dispose();
      }
      return syndicationFeed;
    }

    public async Task<string> LoadNewsHtmlPage(string uri, bool useDarkCss)
    {
      HttpResponseMessage response = await new HttpClient().GetAsync(uri);
      Stream stream = await response.Content.ReadAsStreamAsync();
      string str;
      try
      {
        StreamReader streamReader = new StreamReader(stream, Encoding.UTF8); // 1251
        try
        {
          str = new NewsHtmlProcessor(streamReader.ReadToEnd()).WrapDetails(useDarkCss);
        }
        finally
        {
          streamReader?.Dispose();
        }
      }
      finally
      {
        ((IDisposable) stream)?.Dispose();
      }
      return str;
    }
  }
}
