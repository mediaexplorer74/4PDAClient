using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HtmlAgilityPack;
using System.Text;
using System.Diagnostics;
using Windows.Security.Cryptography.Core;
using System.Xml.Linq;

namespace NewsParserApp
{
    public sealed partial class MainPage : Page
    {
        private List<NewsItem> newsItems = new List<NewsItem>();

        public readonly string RSS_Url = "http://4pda.to";//"https://4pda.to/feed";

        public MainPage()
        {
            this.InitializeComponent();

            GetNewsAsync();
        }

        

        private async void ParseButton_Click(object sender, RoutedEventArgs e)
        {
            OutputTextBlock.Text = "Парсинг новостей...";
            await GetNewsAsync();
        }

        private async Task GetNewsAsync()
        {
            
            HttpClient httpClient = new HttpClient();

            try
            {
                // зарегистрирует кодовые страницы Windows.
                // Тогда windows-1251 будет распознана HTTP клиентом автоматически.
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


                string responseString = await httpClient.GetStringAsync(RSS_Url);

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(responseString);

                HtmlNodeCollection newsNodes =
                htmlDocument.DocumentNode.SelectNodes("//div[@class='v-panel']/a");

                /*
               
                byte[] response = await httpClient.GetByteArrayAsync(RSS_Url);

                string responseString = 
                    Encoding.ASCII.GetString(response, 0, response.Length - 1);

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(responseString);

                HtmlNodeCollection newsNodes =
                htmlDocument.DocumentNode.SelectNodes("//div[@class='more-box']/a");
                //htmlDocument.DocumentNode.SelectNodes("//title[@class='title']/a");
                //htmlDocument.DocumentNode.SelectNodes("//h2[@class='entry-title']/a");
                */

                newsItems.Clear();

                if (newsNodes != null)
                {
                    foreach (var node in newsNodes)
                    {
                        
                        
                        string text = node.GetAttributeValue("title", string.Empty);//node.InnerText + ": " +  node.OuterHtml.ToString();

                        string stringPartToCut = "Комментарии к ";
                        // индекс последнего символа
                        int startIndex = stringPartToCut.Length - 1;

                        string title = text;
                        
                        if (startIndex > 0)
                          title = text.Substring(startIndex);

                        string link = node.GetAttributeValue("href", string.Empty);


                        newsItems.Add(new NewsItem { Title = title, Link = link });
                    }
                    NewsListView.ItemsSource = newsItems;
                }
                else
                {
                    OutputTextBlock.Text = "Новости не найдены.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] httpClient.GetStringAsync error: " + ex.Message);
                OutputTextBlock.Text = $"Произошла ошибка: {ex.Message}";
            }
        }

        private void NewsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewsListView.SelectedItem is NewsItem selectedNews)
            {
                Frame.Navigate(typeof(DetailsPage), selectedNews);
            }
        }
    }

    public class NewsItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
