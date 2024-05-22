// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.DataModels.ForumDataModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

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
