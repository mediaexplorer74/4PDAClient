// ForPDA.AppServices.ViewModels.NewsDetailsPageViewModel

using Caliburn.Micro;
using ForPDA.Communication;
using System;
using System.ComponentModel;

#nullable disable
namespace ForPDA.AppServices.ViewModels
{
  public class NewsDetailsPageViewModel : Screen
  {
    private readonly NewsDataService _newsDataService;
    private readonly IBusyIndicator _busyIndicator;
    private IBrowserView _view;

    public NewsDetailsPageViewModel(NewsDataService newsDataService, IBusyIndicator busyIndicator)
    {
      this._newsDataService = newsDataService;
      this._busyIndicator = busyIndicator;
    }

    private string NewsUri_BackingField;
    public string NewsUri
    {
      get => this.NewsUri_BackingField;
      set
      {
        if (string.Equals(this.NewsUri_BackingField, value, StringComparison.Ordinal))
          return;
        this.NewsUri_BackingField = value;
        this.NotifyOfPropertyChange(nameof (NewsUri));
      }
    }

    private bool IsLoaded_BackingField;

    public bool IsLoaded
    {
      get => this.IsLoaded_BackingField;
      set
      {
        if (this.IsLoaded_BackingField == value)
          return;
        this.IsLoaded_BackingField = value;
        this.NotifyOfPropertyChange(nameof (IsLoaded));
      }
    }

    protected override void OnViewLoaded(object view)
    {
      //base.OnViewLoaded(view);
      this._view = (IBrowserView) view;
      this.LoadPageAsync();
    }

    private async void LoadPageAsync()
    {
      using (this._busyIndicator.StartJob())
      {
        string html = await this._newsDataService.LoadNewsHtmlPage(this.NewsUri, ScreenHelper.IsDarkTheme);
        this._view.LoadContent(html);
        this.IsLoaded = true;
      }
    }

    //public event PropertyChangedEventHandler PropertyChanged;
  }
}
