// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.VisualTreeAdapter
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

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
