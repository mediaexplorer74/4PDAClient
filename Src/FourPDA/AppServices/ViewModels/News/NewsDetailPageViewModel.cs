// FourPDA.ViewModels.NewsDetailPageViewModel

using Caliburn.Micro;
using FourPDA.AppServices;
using FourPDA.AppServices.DataModels;
using FourPDA.Communication;
using System;
using System.ComponentModel;

#nullable disable
namespace FourPDA.ViewModels
{
  public class NewsDetailPageViewModel : Screen
  {
    private readonly Caliburn.Micro.INavigationService _navigationService;

    private readonly NewsDataService _newsDataService;

    private readonly IBusyIndicator _busyIndicator;
    
    private IBrowserView _view;

    public NewsItemDataModel Parameter { get; set; }


    private string title;
    public string Title
    {
        get { return title; }
        set
        {
            title = value;
            NotifyOfPropertyChange(() => Title);
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
        this.NotifyOfPropertyChange(nameof (Uri));
      }
    }


    private bool isnotifying;

    public bool IsNotifying
    {
      get => this.isnotifying;
      set
      {
        if (this.isnotifying == value)
          return;
        this.isnotifying = value;
        this.NotifyOfPropertyChange(nameof (IsNotifying));
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



    // construct data services & nav. service
    public NewsDetailPageViewModel
    (
        NewsDataService newsDataService,
        IBusyIndicator busyIndicator,
        Caliburn.Micro.INavigationService navigationService
    )
    {
        this._newsDataService = newsDataService;

        this._busyIndicator = busyIndicator;

        this._navigationService = navigationService;
    }


   

    protected override void OnActivate()
    {
        
        Title = Parameter.Title;
        Uri = Parameter.Uri;
        Body = Parameter.Body;
        IsNotifying = Parameter.IsNotifying;
        Timestamp = Parameter.Timestamp;

     }
  }
}

/*
 
    private async void LoadPageAsync()
    {
      //using (this._busyIndicator.StartJob())
      {
        //string html = await this._newsDataService.LoadNewsHtmlPage(
        //    this.Uri, ScreenHelper.IsDarkTheme);
        //this._view.LoadContent(html);
        this.IsNotifying = true;
      }
    }


    protected override void OnViewLoaded(object view)
    {
      base.OnViewLoaded(view);

      //this._view = (IBrowserView) view;
      
      this.LoadPageAsync();
    }
*/

