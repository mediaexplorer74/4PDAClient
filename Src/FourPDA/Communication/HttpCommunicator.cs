// Decompiled with JetBrains decompiler
// Type: ForPDA.Communication.HttpCommunicator
// Assembly: ForPDA.Communication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7621B702-77AB-4A6D-A66D-A001F234DCA5
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.Communication.dll

using MSPToolkit.Encodings;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace ForPDA.Communication
{
  public class HttpCommunicator
  {
    private HttpClientHandler _handler;

    public HttpCommunicator() => this.InitHandler();

    private void InitHandler()
    {
      this._handler = new HttpClientHandler();
      this._handler.AllowAutoRedirect = true;
      if (this._handler.SupportsAutomaticDecompression)
        this._handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      this._handler.CookieContainer.Add(new Uri("http://4pda.ru/"), new Cookie("ngx_mb", "1"));
    }

    public async Task<Stream> GetStreamAsync(string uri)
    {
      HttpClient http = new HttpClient((HttpMessageHandler) this._handler);
      HttpResponseMessage response = await http.GetAsync(uri);
      Stream stream = await response.Content.ReadAsStreamAsync();
      return stream;
    }

    public async Task<string> GetStringAsync(string uri, Encoding encoding = null)
    {
      if (encoding == null)
        encoding = (Encoding) new Windows1251Encoding();
      Stream responseStream = await this.GetStreamAsync(uri);
      StreamReader streamReader = new StreamReader(responseStream, encoding);
      string responseString;
      try
      {
        responseString = streamReader.ReadToEnd();
      }
      finally
      {
        streamReader?.Dispose();
      }
      return responseString;
    }
  }
}
