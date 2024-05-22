// Decompiled with JetBrains decompiler
// Type: FourPDA.Interaction.ILinqTree`1
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

using System.Collections.Generic;

#nullable disable
namespace FourPDA.Interaction
{
  public interface ILinqTree<T>
  {
    IEnumerable<T> Children();

    T Parent { get; }
  }
}
