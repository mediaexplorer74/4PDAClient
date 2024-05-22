// Decompiled with JetBrains decompiler
// Type: ForPDA.Communication.Model.ForumTopicModel
// Assembly: ForPDA.Communication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7621B702-77AB-4A6D-A66D-A001F234DCA5
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.Communication.dll

#nullable disable
namespace ForPDA.Communication.Model
{
  public class ForumTopicModel
  {
    public ForumTopicModel(string name) => this.Name = name;

    public string Name { get; set; }

    public string LastMessageTime { get; set; }

    public string AuthorName { get; set; }
  }
}
