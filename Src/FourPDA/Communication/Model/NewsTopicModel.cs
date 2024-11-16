// FourPDA.Communication.Model.NewsTopicModel

#nullable disable
namespace FourPDA.Communication.Model
{
    public class NewsTopicModel
    {
        public NewsTopicModel(string name)
        { 
            this.Name = name; 
        }

        public string Name { get; set; }

        public string LastMessageTime { get; set; }

        public string AuthorName { get; set; }
    }
}