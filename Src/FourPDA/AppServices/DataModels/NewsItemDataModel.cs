// ForPDA.AppServices.DataModels.NewsItemDataModel

using Caliburn.Micro;
using System;
using System.ComponentModel;

#nullable disable
namespace ForPDA.AppServices.DataModels
{
  public class NewsItemDataModel : PropertyChangedBase
  {
    private string Title_BackingField;
    public string Title
        {
            get
            {
                return this.Title_BackingField;
            }
            
            set
            {
                if (string.Equals(this.Title_BackingField, value, StringComparison.Ordinal))
                    return;
                this.Title_BackingField = value;
                this.NotifyOfPropertyChange(nameof(Title));
            }
        }

        private string Body_BackingField;
        public string Body
    {
      get => this.Body_BackingField;
      set
      {
        if (string.Equals(this.Body_BackingField, value, StringComparison.Ordinal))
          return;
        this.Body_BackingField = value;
        this.NotifyOfPropertyChange(nameof (Body));
      }
    }

    private string Timestamp_BackingField;
    public string Timestamp
    {
      get => this.Timestamp_BackingField;
      set
      {
        if (string.Equals(this.Timestamp_BackingField, value, StringComparison.Ordinal))
          return;
        this.Timestamp_BackingField = value;
        this.NotifyOfPropertyChange(nameof (Timestamp));
      }
    }

    private string Uri_BackingField;
    public string Uri
    {
      get => this.Uri_BackingField;
      set
      {
        if (string.Equals(this.Uri_BackingField, value, StringComparison.Ordinal))
          return;
        this.Uri_BackingField = value;
        this.NotifyOfPropertyChange(nameof(Uri));
      }
    }

    //public event PropertyChangedEventHandler PropertyChanged;
  }
}
