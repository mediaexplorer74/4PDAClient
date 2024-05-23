// ForPDA.AppServices.DataModels.SubForumDataModel

//using System.Windows.Media;
using Windows.UI;

#nullable disable
namespace ForPDA.AppServices.DataModels
{
  public class SubForumDataModel : ForumDataModel
  {
    public int SquareWidth { get; set; }

    public int SquareHeight { get; set; }

    public Color Color { get; set; }

    public bool IsRoot => this.SquareHeight == 0;

    public int FontSize { get; set; }
  }
}
