// FourPDA.AppServices.ViewModels.MainPivot.NewsViewModel

using Caliburn.Micro;
using FourPDA.AppServices;
using FourPDA.AppServices.Controllers;
using FourPDA.AppServices.DataModels;
using FourPDA.Communication;
using FourPDA.Communication.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Syndication;
using HtmlAgilityPack; //!
using Windows.Security.Cryptography.Core;
using System.Xml.Linq;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using Telerik.Core;

#nullable disable
namespace FourPDA.ViewModels
{
    public class NewsPageViewModel : Conductor<Screen>.Collection.OneActive  // Screen?
    {
               
        private readonly Caliburn.Micro.INavigationService _navigationService;

        private readonly IBusyIndicator _busyIndicator;

        private readonly NewsController _newsController;

        private readonly NewsDataService _newsDataService;// = NewsPageViewModel.NewsDataService();

        

        public readonly string RSS_Url = "http://4pda.to";//"https://4pda.to/feed";

        private NewsModel _currentNew;

        private ObservableCollection<NewsItemDataModel> newsItems;
        public ObservableCollection<NewsItemDataModel> NewsItems
        {
            get => this.newsItems;
            set
            {
                if (this.newsItems == value)
                    return;
                this.newsItems = value;
                this.NotifyOfPropertyChange(nameof(NewsItems));
            }
        }


        //---------------------------
        // construct controllers & data services
        public NewsPageViewModel
        (
                NewsController newsController,
                NewsDataService newsDataService,
                IBusyIndicator busyIndicator,
                Caliburn.Micro.INavigationService navigationService
        )
        {
            this._newsController = newsController;
            
            this._newsDataService = newsDataService;

            this._busyIndicator = busyIndicator;

            this._navigationService = navigationService;
        }
        //---------------------------

        public NewsPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        public void OpenNewsDetails(NewsItemDataModel newsItem)
        {
            //(Caliburn.Micro.NavigationExtensions.UriFor<NewsDetailPageViewModel>
            //(this._navigationService)).Navigate();
            _navigationService.NavigateToViewModel<NewsDetailPageViewModel>(newsItem);
        }

        private static NewsDataService NewsDataService()
        {
            NewsDataService nds = default;

            return nds;
        }

        public async Task LoadDataAsync()
        {
            //DAMAGED
            //SyndicationFeed feeds = await this._newsDataService.LoadFeedsAsync();

            //this.NewsItems = (ObservableCollection<NewsItemDataModel>)feeds.Items;
            


            //(IEnumerable<NewsItemDataModel>)Enumerable.ToList<NewsItemDataModel>(
            //    Enumerable.Select<SyndicationItem, NewsItemDataModel>(feeds.Items,
            //    new Func<SyndicationItem, NewsItemDataModel>(this.CreateItemDataModel)));
        }

        private NewsItemDataModel CreateItemDataModel(SyndicationItem feed)
        {
            return new NewsItemDataModel()
            {
                Title = "TEST",//feed.Title.Text,
                Body = ((string)((string)feed.Summary.Text).Replace("\r", "")).Replace("\n", ""),
                Timestamp = default,//feed.PublishDate.DateTime.ToString("dd MMMM hh:mm"), //TODO
                Uri = feed.Id,
            };
        }

     
        protected override async void OnActivate() 
        {
            base.OnInitialize();

            //this.LoadDataAsync(); 

            // Load real web data set
            NewsItems = await RealLoadDataAsync();

            // Load (fake) test data set
            //NewsItems = await FakeLoadDataAsync();
        }

        
        private async Task<ObservableCollection<NewsItemDataModel>> RealLoadDataAsync()
        {
            var newsItems = new ObservableCollection<NewsItemDataModel>();
            
            HttpClient httpClient = new HttpClient();

            try
            {
                // зарегистрирует кодовые страницы Windows.
                // Тогда windows-1251 будет распознана HTTP клиентом автоматически.
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


                string responseString = await httpClient.GetStringAsync(RSS_Url);

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(responseString);

                // парсим ноды первого вида 
                HtmlNodeCollection newsNodes1 =
                htmlDocument.DocumentNode.SelectNodes("//article/*/div[@class='v-panel']/a"); //("//div[@class='v-panel']/a");

                // парсим ноды второго вида
                HtmlNodeCollection newsNodes2 =
                htmlDocument.DocumentNode.SelectNodes("//article/*/div/p[@style='text-align: justify;']");

                if (newsNodes2 != null)
                {
                    string body = "";

                    if (newsNodes1 != null)
                        foreach (var n_node in newsNodes2)
                    {
                        body = n_node.InnerText;
                    }


                    foreach (var node in newsNodes1)
                    {

                        string text = node.GetAttributeValue("title", string.Empty);
                        //node.InnerText + ": " +  node.OuterHtml.ToString();

                        string stringPartToCut = "Комментарии к ";
                        // индекс последнего символа
                        int startIndex = stringPartToCut.Length - 1;

                        string title = text;

                        if (startIndex > 0)
                            title = text.Substring(startIndex);

                        string link = node.GetAttributeValue("href", string.Empty);


                        newsItems.Add
                            (
                            new NewsItemDataModel
                            { 
                                Title = title,
                                Body = body, // reserved for short "content body"
                                Uri = link,
                                IsNotifying = true,
                                Timestamp = "12:00" // reserved for sorting, etc.
                            }
                        );
                    }
                    
                }
                else
                {
           
                    Debug.WriteLine("[ex] News not found.");
                }
            }
            catch (Exception ex)
            {
        
                Debug.WriteLine("[ex] httpClient.GetStringAsync error: " + ex.Message);
            }

            return newsItems;
        }

        private async Task<ObservableCollection<NewsItemDataModel>> FakeLoadDataAsync()
        {
            var newsItems = new ObservableCollection<NewsItemDataModel>
            {
                new NewsItemDataModel {Title = "The Avengers",
                    Body = "Joss Whedon",
                    Uri = "test_url1",
                    IsNotifying = true,
                    Timestamp = "12:00"
                },
                new NewsItemDataModel {Title = "Transformers",
                    Body = "Michael Bay",
                    Uri = "test_url2",
                    IsNotifying = true,
                    Timestamp = "13:00"
                },
                new NewsItemDataModel {Title = "X-Men Days of the future past",
                    Body = "Bryan Synger",
                    Uri = "test_url3",
                    IsNotifying = false,
                    Timestamp = "14:00"
                }
            };
            return newsItems;
        }

        /* public void GoBack() 
         {
             //
         }

         public bool CanReturnBack
         {
             get
             {
                 return !this._currentNew.HasRootParent;
             }
         }
        */

    }
}
