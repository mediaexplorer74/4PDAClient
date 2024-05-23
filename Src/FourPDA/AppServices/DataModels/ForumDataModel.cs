// ForPDA.AppServices.DataModels.ForumDataModel

#nullable disable
namespace ForPDA.AppServices.DataModels
{
  public class ForumDataModel
  {
    public string Title { get; set; }

    public string Id { get; set; }

    public bool HasChildren { get; set; }

    public string ParentId { get; set; }
  }
}
