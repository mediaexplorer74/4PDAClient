// ForPDA.AppServices.ViewModels.MainPivot.NewsViewModel

using Caliburn.Micro;
using ForPDA.AppServices.DataModels;
using ForPDA.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Syndication; //using System.ServiceModel.Syndication;

#nullable disable
namespace ForPDA.AppServices.ViewModels.MainPivot
{
  public class NewsViewModel : Conductor<Screen>.Collection.OneActive
  {
    private readonly NewsDataService _newsDataService;

    public NewsViewModel(NewsDataService newsDataService)
    {
      this._newsDataService = newsDataService;
    }

    private IEnumerable<NewsItemDataModel> NewsItems_BackingField;

       

        public IEnumerable<NewsItemDataModel> NewsItems
    {
      get => this.NewsItems_BackingField;
      set
      {
        if (this.NewsItems_BackingField == value)
          return;
        this.NewsItems_BackingField = value;
        this.NotifyOfPropertyChange(nameof (NewsItems));
      }
    }

    public async Task LoadDataAsync()
    {
      SyndicationFeed feeds = await this._newsDataService.LoadFeedsAsync();
      this.NewsItems = (IEnumerable<NewsItemDataModel>) Enumerable.ToList<NewsItemDataModel>(
          Enumerable.Select<SyndicationItem, NewsItemDataModel>(feeds.Items, 
          new Func<SyndicationItem, NewsItemDataModel>(this.CreateItemDataModel)));
    }

    private NewsItemDataModel CreateItemDataModel(SyndicationItem feed)
    {
      return new NewsItemDataModel()
        {
          Title = feed.Title.Text,
          Body = ((string)((string)feed.Summary.Text).Replace("\r", "")).Replace("\n", ""),
          Timestamp = default,//feed.PublishDate.DateTime.ToString("dd MMMM hh:mm"),
          Uri = feed.Id
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
