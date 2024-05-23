// ForPDA.Communication.Model.ForumTopicModel

#nullable disable
namespace ForPDA.Communication.Model
{
  public class ForumTopicModel
  {
    public ForumTopicModel(string name) => this.Name = name;

    public string Name { get; set; }

    public string LastMessageTime { get; set; }

    public string AuthorName { get; set; }
  }
}
