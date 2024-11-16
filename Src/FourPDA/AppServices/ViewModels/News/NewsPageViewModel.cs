// FourPDA.AppServices.ViewModels.MainPivot.NewsViewModel

using Caliburn.Micro;
using FourPDA.AppServices.DataModels;
using FourPDA.Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Syndication; //using System.ServiceModel.Syndication;

#nullable disable
namespace FourPDA.ViewModels
{
    public class NewsPageViewModel : Screen //Conductor<Screen>.Collection.OneActive // Screen ?
    {
               
        private readonly INavigationService _navigationService;


        public NewsPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        private ObservableCollection<NewsItemDataModel> newsitems;

        public ObservableCollection<NewsItemDataModel> NewsItems
        {
            get => this.newsitems;
            set
            {
                if (this.newsitems == value)
                    return;
                this.newsitems = value;
                this.NotifyOfPropertyChange(nameof(NewsItems));
            }
        }

        public void OpenNewsDetails(NewsDataModel newsItem)
        {
            (Caliburn.Micro.NavigationExtensions.UriFor<NewsDetailPageViewModel>
            (this._navigationService)).Navigate();
            //_navigationService.NavigateToViewModel<NewsDetailPageViewModel>(newsItem);
        }

        public async Task LoadDataAsync()
        {
            SyndicationFeed feeds = default;//await this._newsDataService.LoadFeedsAsync();

            this.NewsItems = (ObservableCollection<NewsItemDataModel>)feeds.Items;/*(IEnumerable<NewsItemDataModel>)Enumerable.ToList<NewsItemDataModel>(
                Enumerable.Select<SyndicationItem, NewsItemDataModel>(feeds.Items,
                new Func<SyndicationItem, NewsItemDataModel>(this.CreateItemDataModel)));*/
        }

        private NewsItemDataModel CreateItemDataModel(SyndicationItem feed)
        {
            return new NewsItemDataModel()
            {
                Title = "TEST",//feed.Title.Text,
                Body = ((string)((string)feed.Summary.Text).Replace("\r", "")).Replace("\n", ""),
                Timestamp = default,//feed.PublishDate.DateTime.ToString("dd MMMM hh:mm"), //TODO
                Uri = feed.Id
            };
        }

        //RnD
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            //this.LoadDataAsync(); // ?
        }

        public void GoBack() 
        {
            //
        }

        //RnD
        public bool CanReturnBack
        {
            get
            {
                return false;//!this._currentForum.HasRootParent;
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnActivate()
        {
            //this.LoadDataAsync(); // ?
            NewsItems = new ObservableCollection<NewsItemDataModel>
            {
                new NewsItemDataModel {Title = "The Avengers", Body = "Joss Whedon"},
                new NewsItemDataModel {Title = "Transformers", Body = "Michael Bay"},
                new NewsItemDataModel {Title = "X-Men Days of the future past", Body = "Bryan Synger"}
            };
        }
    }
}
