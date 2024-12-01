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


                HtmlNodeCollection rootNodes = htmlDocument.DocumentNode.SelectNodes(
                    "//article/div[@class='description']");

                //**********************
                if (rootNodes != null)
                    foreach (HtmlNode r_node in rootNodes)
                    {
                        bool flag = false;

                        HtmlNodeCollection childNodes = r_node.ChildNodes;


                        if (childNodes != null)
                        {
                            HtmlNode[] oneNode = new HtmlNode[5];
                            int position = 0;
                            foreach (HtmlNode c_node in childNodes)
                            {
                                Debug.WriteLine(c_node);
                                oneNode[position] = c_node;
                                ++position;
                            }

                            if ((oneNode[0].Name=="h2") && (oneNode[1].Name=="div"))
                            {
                                Debug.WriteLine(oneNode[0].InnerText + " :: " + oneNode[1].InnerText);


                                HtmlNodeCollection subNodes = oneNode[0].ChildNodes;

                                string uri = "";
                                foreach (HtmlNode item in subNodes)
                                {
                                    uri = item.GetAttributeValue("href", string.Empty);
                                }

                                string title = oneNode[0].InnerText;//GetAttributeValue("title", string.Empty);

                                string body = oneNode[1].InnerText;

                                //string uri = "777";// oneNode[0].OuterHtml.GetAttributeValue("href", string.Empty);


                                newsItems.Add
                                (
                                    new NewsItemDataModel
                                    {
                                        Title = title,
                                        Body = body,
                                        Uri = uri,
                                        IsNotifying = true,
                                        Timestamp = "12:00" // reserved for sorting, etc.
                                    }
                                );

                            }


                        }
                        else
                        {
                            Debug.WriteLine("rootNodes empty!");
                        }
                    }
                        //**********************
/*

                        // парсим ноды первого вида 
                        HtmlNodeCollection newsNodes1 =
                //htmlDocument.DocumentNode.SelectNodes("//article/ * /div[@class='v-panel']/a"); //("//div[@class='v-panel']/a");
                htmlDocument.DocumentNode.SelectNodes("//article/div[@class='description']/h2[@class='list-post-title']/a");
                

                

                

                // парсим ноды второго вида
                HtmlNodeCollection newsNodes2 =
                //htmlDocument.DocumentNode.SelectNodes("//article/ * /div/p[@style='text-align: justify;']");
                htmlDocument.DocumentNode.SelectNodes("//article/div[@class='description']/div[@itemprop='description']/p[@style='text-align: justify;']");

                if (newsNodes2 != null)
                {
                    
                    string[] body = new string[1000];

                    int counter2 = 0;
                    if (newsNodes1 != null)
                        foreach (var n_node in newsNodes2)
                        {
                            //string body_text = n_node.GetAttributeValue("title", string.Empty);
                            body[counter2] = n_node.InnerText;
                            ++counter2; 
                        }

                    int counter1 = 0;
                    foreach (var node in newsNodes1)
                    {

                        string title = node.GetAttributeValue("title", string.Empty);
                        //node.InnerText + ": " +  node.OuterHtml.ToString();
                                                
                        string link = node.GetAttributeValue("href", string.Empty);


                        newsItems.Add
                        (
                            new NewsItemDataModel
                            { 
                                Title = title,
                                Body = body[counter1], 
                                Uri = link,
                                IsNotifying = true,
                                Timestamp = "12:00" // reserved for sorting, etc.
                            }
                        );
                        ++counter1;
                    }//foreach
                    
                }
                else
                {
           
                    Debug.WriteLine("[ex] News not found.");
                }*/
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
