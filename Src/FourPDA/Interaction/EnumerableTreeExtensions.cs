// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.EnumerableTreeExtensions
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System;
using System.Collections.Generic;
using System.Windows;

#nullable disable
namespace FourPDA.Interaction
{
  public static class EnumerableTreeExtensions
  {
    private static IEnumerable<DependencyObject> DrillDown(
      this IEnumerable<DependencyObject> items,
      Func<DependencyObject, IEnumerable<DependencyObject>> function)
    {
      foreach (DependencyObject item in items)
      {
        foreach (DependencyObject itemChild in function(item))
          yield return itemChild;
      }
    }

    public static IEnumerable<DependencyObject> DrillDown<T>(
      this IEnumerable<DependencyObject> items,
      Func<DependencyObject, IEnumerable<DependencyObject>> function)
      where T : DependencyObject
    {
      foreach (DependencyObject item in items)
      {
        foreach (DependencyObject itemChild in function(item))
        {
          if (itemChild is T obj)
            yield return (DependencyObject) obj;
        }
      }
    }

    public static IEnumerable<DependencyObject> Descendants(this IEnumerable<DependencyObject> items)
    {
      return items.DrillDown((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.Descendants()));
    }

    public static IEnumerable<DependencyObject> DescendantsAndSelf(
      this IEnumerable<DependencyObject> items)
    {
      return items.DrillDown((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.DescendantsAndSelf()));
    }

    public static IEnumerable<DependencyObject> Ancestors(this IEnumerable<DependencyObject> items)
    {
      return items.DrillDown((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.Ancestors()));
    }

    public static IEnumerable<DependencyObject> AncestorsAndSelf(
      this IEnumerable<DependencyObject> items)
    {
      return items.DrillDown((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.AncestorsAndSelf()));
    }

    public static IEnumerable<DependencyObject> Elements(this IEnumerable<DependencyObject> items)
    {
      return items.DrillDown((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.Elements()));
    }

    public static IEnumerable<DependencyObject> ElementsAndSelf(
      this IEnumerable<DependencyObject> items)
    {
      return items.DrillDown((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.ElementsAndSelf()));
    }

    public static IEnumerable<DependencyObject> Descendants<T>(
      this IEnumerable<DependencyObject> items)
      where T : DependencyObject
    {
      return items.DrillDown<T>((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.Descendants()));
    }

    public static IEnumerable<DependencyObject> DescendantsAndSelf<T>(
      this IEnumerable<DependencyObject> items)
      where T : DependencyObject
    {
      return items.DrillDown<T>((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.DescendantsAndSelf()));
    }

    public static IEnumerable<DependencyObject> Ancestors<T>(
      this IEnumerable<DependencyObject> items)
      where T : DependencyObject
    {
      return items.DrillDown<T>((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.Ancestors()));
    }

    public static IEnumerable<DependencyObject> AncestorsAndSelf<T>(
      this IEnumerable<DependencyObject> items)
      where T : DependencyObject
    {
      return items.DrillDown<T>((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.AncestorsAndSelf()));
    }

    public static IEnumerable<DependencyObject> Elements<T>(this IEnumerable<DependencyObject> items) where T : DependencyObject
    {
      return items.DrillDown<T>((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.Elements()));
    }

    public static IEnumerable<DependencyObject> ElementsAndSelf<T>(
      this IEnumerable<DependencyObject> items)
      where T : DependencyObject
    {
      return items.DrillDown<T>((Func<DependencyObject, IEnumerable<DependencyObject>>) (i => i.ElementsAndSelf()));
    }
  }
}
