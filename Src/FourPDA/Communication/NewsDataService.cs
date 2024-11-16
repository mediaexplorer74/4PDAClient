// FourPDA.Communication.NewsDataService

using FourPDA.Communication.Html;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Web.Syndication;//using System.ServiceModel.Syndication;
//using MSPToolkit.Encodings;

#nullable disable
namespace FourPDA.Communication
{
  public class NewsDataService
  {
    private const string RSS_URL = "http://4pda.to/feed";
    private const string SITE_URL = "http://4pda.to/";
    private readonly HttpClient httpClient = new HttpClient();

    public async Task<SyndicationFeed> LoadFeedsAsync()
    {
        //HttpResponseMessage response = default;
        string responseString = "";

        try
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            //response = await this.httpClient.GetAsync(RSS_URL);
            
            responseString = await httpClient.GetStringAsync(RSS_URL);
        }
        catch (Exception ex) 
        {
            Debug.WriteLine("[ex] httpClient.GetStringAsync(RSS_URL) ex: " + ex.Message);
        }
      
        /*    
        Stream stream = default;
        try
        {
                HttpContent rc = response.Content;
                stream = await rc.ReadAsStreamAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] ReadAsStreamAsync ex: " + ex.Message);
        }
            
      
       //XmlReader reader = XmlReader.Create(RSS_URL);
        //var feed = await Task.Run(() => SyndicationFeed.Load(reader));
        */

        // Windows.Web.Syndication
      SyndicationFeed syndicationFeed = default;
      try
      {
        //StreamReader streamReader = new StreamReader(
        //    stream, 
        //    /*Encoding.UTF8*/
        //    Encoding.ASCII); // 1251 ?
        try
        {
          //XmlReader reader = XmlReader.Create((TextReader) streamReader);
          try
          {
            //string rs = reader.ReadContentAsString();           


            syndicationFeed = new SyndicationFeed();
            syndicationFeed.Load(/*rs*/responseString);
          }
          finally
          {
            //((IDisposable) reader)?.Dispose();
          }
        }
        finally
        {
          //streamReader?.Dispose();
        }
      }
      finally
      {
        //((IDisposable) stream)?.Dispose();
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
