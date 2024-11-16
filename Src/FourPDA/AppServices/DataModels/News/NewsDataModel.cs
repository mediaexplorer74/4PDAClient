// FourPDA.AppServices.DataModels.NewsDataModel

#nullable disable
namespace FourPDA.AppServices.DataModels
{
    public class NewsDataModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public string Timestamp { get; set; }

        public bool HasChildren { get; set; }

        public string ParentId { get; set; }
    }
}
