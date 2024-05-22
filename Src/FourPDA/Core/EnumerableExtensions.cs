// Decompiled with JetBrains decompiler
// Type: ForPDA.Core.EnumerableExtensions
// Assembly: ForPDA.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F063FDB-43B6-4F39-AEFE-6C4BC0FF6CDF
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.Core.dll

using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace ForPDA.Core
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<T> Even<T>(this IEnumerable<T> enumerable)
    {
      bool returnNext = true;
      foreach (T value in enumerable)
      {
        if (returnNext)
          yield return value;
        returnNext = !returnNext;
      }
    }

    public static IEnumerable<T> Odd<T>(this IEnumerable<T> enumerable)
    {
      bool returnNext = false;
      foreach (T value in enumerable)
      {
        if (returnNext)
          yield return value;
        returnNext = !returnNext;
      }
    }

    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
    {
      return new ObservableCollection<T>(enumerable);
    }

    public static BindableCollection<T> ToBindableCollection<T>(this IEnumerable<T> enumerable)
    {
      return new BindableCollection<T>(enumerable);
    }
  }
}
