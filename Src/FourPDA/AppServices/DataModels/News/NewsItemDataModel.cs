// FourPDA.AppServices.DataModels.NewsItemDataModel

using Caliburn.Micro;
using System;
using System.ComponentModel;

#nullable disable
namespace FourPDA.AppServices.DataModels
{
  public class NewsItemDataModel : PropertyChangedBase
  {
    private string title;
    public string Title
    {
        get
        {
            return this.title;
        }
            
        set
        {
            if (string.Equals(this.title, value, StringComparison.Ordinal))
                return;
            this.title = value;
            this.NotifyOfPropertyChange(nameof(Title));
        }
    }

    private string body;
    public string Body
    {
      get => this.body;
      set
      {
        if (string.Equals(this.body, value, StringComparison.Ordinal))
          return;
        this.body = value;
        this.NotifyOfPropertyChange(nameof(Body));
      }
    }

    private string uri;
    public string Uri
    {
        get => this.uri;
        set
        {
            if (string.Equals(this.uri, value, StringComparison.Ordinal))
                return;
            this.uri = value;
            this.NotifyOfPropertyChange(nameof(Uri));
        }
    }

    private string timestamp;
    public string Timestamp
    {
      get => this.timestamp;
      set
      {
        if (string.Equals(this.timestamp, value, StringComparison.Ordinal))
          return;
        this.timestamp = value;
        this.NotifyOfPropertyChange(nameof(Timestamp));
      }
    }
  }
}
