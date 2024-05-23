// FourPDA.Interaction.VisualTreeAdapter

using System.Collections.Generic;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


#nullable disable
namespace FourPDA.Interaction
{
  public class VisualTreeAdapter : ILinqTree<DependencyObject>
  {
    private DependencyObject _item;

    public VisualTreeAdapter(DependencyObject item) => this._item = item;

    public IEnumerable<DependencyObject> Children()
    {
      int childrenCount = VisualTreeHelper.GetChildrenCount(this._item);
      for (int i = 0; i < childrenCount; ++i)
        yield return VisualTreeHelper.GetChild(this._item, i);
    }

    public DependencyObject Parent => VisualTreeHelper.GetParent(this._item);
  }
}
