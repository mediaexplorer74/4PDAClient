// FourPDA.Interaction.ILinqTree`1

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
