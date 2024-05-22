// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.TreeExtensions
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

#nullable disable
namespace FourPDA.Interaction
{
  public static class TreeExtensions
  {
    public static IEnumerable<DependencyObject> Descendants(this DependencyObject item)
    {
      ILinqTree<DependencyObject> adapter = (ILinqTree<DependencyObject>) new VisualTreeAdapter(item);
      foreach (DependencyObject child in adapter.Children())
      {
        yield return child;
        foreach (DependencyObject grandChild in child.Descendants())
          yield return grandChild;
      }
    }

    public static IEnumerable<DependencyObject> DescendantsAndSelf(this DependencyObject item)
    {
      yield return item;
      foreach (DependencyObject child in item.Descendants())
        yield return child;
    }

    public static IEnumerable<DependencyObject> Ancestors(this DependencyObject item)
    {
      ILinqTree<DependencyObject> adapter = (ILinqTree<DependencyObject>) new VisualTreeAdapter(item);
      for (DependencyObject parent = adapter.Parent; parent != null; parent = adapter.Parent)
      {
        yield return parent;
        adapter = (ILinqTree<DependencyObject>) new VisualTreeAdapter(parent);
      }
    }

    public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject item)
    {
      yield return item;
      foreach (DependencyObject ancestor in item.Ancestors())
        yield return ancestor;
    }

    public static IEnumerable<DependencyObject> Elements(this DependencyObject item)
    {
      ILinqTree<DependencyObject> adapter = (ILinqTree<DependencyObject>) new VisualTreeAdapter(item);
      foreach (DependencyObject child in adapter.Children())
        yield return child;
    }

    public static IEnumerable<DependencyObject> ElementsBeforeSelf(this DependencyObject item)
    {
      if (item.Ancestors().FirstOrDefault<DependencyObject>() != null)
      {
        foreach (DependencyObject child in item.Ancestors().First<DependencyObject>().Elements())
        {
          if (!child.Equals((object) item))
            yield return child;
          else
            break;
        }
      }
    }

    public static IEnumerable<DependencyObject> ElementsAfterSelf(this DependencyObject item)
    {
      if (item.Ancestors().FirstOrDefault<DependencyObject>() != null)
      {
        bool afterSelf = false;
        foreach (DependencyObject child in item.Ancestors().First<DependencyObject>().Elements())
        {
          if (afterSelf)
            yield return child;
          if (child.Equals((object) item))
            afterSelf = true;
        }
      }
    }

    public static IEnumerable<DependencyObject> ElementsAndSelf(this DependencyObject item)
    {
      yield return item;
      foreach (DependencyObject child in item.Elements())
        yield return child;
    }

    public static IEnumerable<T> Descendants<T>(this DependencyObject item)
    {
      return item.Descendants().OfType<T>();
    }

    public static IEnumerable<T> ElementsBeforeSelf<T>(this DependencyObject item)
    {
      return item.ElementsBeforeSelf().Where<DependencyObject>((Func<DependencyObject, bool>) (i => i is T)).Cast<T>();
    }

    public static IEnumerable<DependencyObject> ElementsAfterSelf<T>(this DependencyObject item)
    {
      return item.ElementsAfterSelf().Where<DependencyObject>((Func<DependencyObject, bool>) (i => i is T)).Cast<DependencyObject>();
    }

    public static IEnumerable<DependencyObject> DescendantsAndSelf<T>(this DependencyObject item)
    {
      return item.DescendantsAndSelf().Where<DependencyObject>((Func<DependencyObject, bool>) (i => i is T)).Cast<DependencyObject>();
    }

    public static IEnumerable<T> Ancestors<T>(this DependencyObject item)
    {
      return item.Ancestors().OfType<T>();
    }

    public static IEnumerable<T> AncestorsAndSelf<T>(this DependencyObject item)
    {
      return item.AncestorsAndSelf().OfType<T>();
    }

    public static IEnumerable<DependencyObject> Elements<T>(this DependencyObject item)
    {
      return item.Elements().Where<DependencyObject>((Func<DependencyObject, bool>) (i => i is T)).Cast<DependencyObject>();
    }

    public static IEnumerable<DependencyObject> ElementsAndSelf<T>(this DependencyObject item)
    {
      return item.ElementsAndSelf().Where<DependencyObject>((Func<DependencyObject, bool>) (i => i is T)).Cast<DependencyObject>();
    }
  }
}
