// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.DataModels.SubForumDataModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using System.Windows.Media;

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
