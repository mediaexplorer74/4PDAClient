using HtmlAgilityPack;
using System;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NewsParserApp
{
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.Parameter is NewsItem newsItem)
            {
                TitleTextBlock.Text = newsItem.Title;
                LoadNewsContent(newsItem.Link);
            }
        }

        private async void LoadNewsContent(string link)
        {
            var httpClient = new System.Net.Http.HttpClient();
            try
            {
                // зарегистрирует кодовые страницы Windows. Тогда windows-1251 будет распознана HTTP клиентом автоматически.
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string responseString = await httpClient.GetStringAsync(link);

                HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(responseString);

                // Здесь вы можете настроить XPath для получения содержимого новости

                HtmlNode contentNode =
                    //htmlDocument.DocumentNode.SelectSingleNode("//div[@class='entry-content']");
                    htmlDocument.DocumentNode.SelectSingleNode("//div[@class='title']");


                if (contentNode != null)
                {
                    ContentTextBlock.Text = contentNode.InnerHtml; // Выводим HTML-контент
                }
                else
                {
                    // План Б - показать нераспарсенные сырые данные
                    ContentTextBlock.Text = responseString;
                }
            }
            catch (Exception ex)
            {
                ContentTextBlock.Text = $"Произошла ошибка: {ex.Message}";
            }
        }
    }
}
