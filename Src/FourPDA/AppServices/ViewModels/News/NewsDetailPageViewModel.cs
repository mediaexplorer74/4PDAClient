// FourPDA.AppServices.ViewModels.NewsDetailsPageViewModel

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

    //RnD
    public NewsItemDataModel Parameter { get; set; }


    //Temp
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


    private string newsuri;
    public string NewsUri
    {
      get => this.newsuri;
      set
      {
        if (string.Equals(this.newsuri, value, StringComparison.Ordinal))
          return;
        this.newsuri = value;
        this.NotifyOfPropertyChange(nameof (NewsUri));
      }
    }


    private bool isloaded;

    public bool IsLoaded
    {
      get => this.isloaded;
      set
      {
        if (this.isloaded == value)
          return;
        this.isloaded = value;
        this.NotifyOfPropertyChange(nameof (IsLoaded));
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


/*
    protected override void OnViewLoaded(object view)
    {
      base.OnViewLoaded(view);

      //this._view = (IBrowserView) view;
      
      this.LoadPageAsync();
    }
*/



    private async void LoadPageAsync()
    {
      //using (this._busyIndicator.StartJob())
      {
        //string html = await this._newsDataService.LoadNewsHtmlPage(
        //    this.NewsUri, ScreenHelper.IsDarkTheme);
        //this._view.LoadContent(html);
        this.IsLoaded = true;
      }
    }

    // public event PropertyChangedEventHandler PropertyChanged;

    protected override void OnActivate()
    {
        //this.LoadDataAsync(); // ?
        //NewsItems = new ObservableCollection<NewsItemDataModel>
        //{
        //    new NewsItemDataModel {Title = "The Avengers", Body = "Joss Whedon"},
        //    new NewsItemDataModel {Title = "Transformers", Body = "Michael Bay"},
        //    new NewsItemDataModel {Title = "X-Men Days of the future past", Body = "Bryan Synger"}
        //};
        Title = Parameter.Title;
     }
  }
}
