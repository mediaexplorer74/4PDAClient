// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.ViewModels.MainPivot.NewsViewModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Caliburn.Micro;
using ForPDA.AppServices.DataModels;
using ForPDA.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

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

    public IEnumerable<NewsItemDataModel> NewsItems
    {
      get => this.\u003CNewsItems\u003Ek__BackingField;
      set
      {
        if (this.\u003CNewsItems\u003Ek__BackingField == value)
          return;
        this.\u003CNewsItems\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (NewsItems));
      }
    }

    public async Task LoadDataAsync()
    {
      SyndicationFeed feeds = await this._newsDataService.LoadFeedsAsync();
      this.NewsItems = (IEnumerable<NewsItemDataModel>) Enumerable.ToList<NewsItemDataModel>(Enumerable.Select<SyndicationItem, NewsItemDataModel>(feeds.Items, new Func<SyndicationItem, NewsItemDataModel>(this.CreateItemDataModel)));
    }

    private NewsItemDataModel CreateItemDataModel(SyndicationItem feed)
    {
      return new NewsItemDataModel()
      {
        Title = feed.Title.Text,
        Body = ((string) ((string) feed.Summary.Text).Replace("\r", "")).Replace("\n", ""),
        Timestamp = feed.PublishDate.DateTime.ToString("dd MMMM hh:mm"),
        Uri = feed.Id
      };
    }
  }
}
