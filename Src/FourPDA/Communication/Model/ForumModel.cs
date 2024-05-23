// ForPDA.Communication.Model.ForumModel

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace ForPDA.Communication.Model
{
  public class ForumModel
  {
    public ForumModel(string id, string name, string parentId)
    {
      this.Id = id;
      this.Name = name;
      this.ParentId = parentId;
      this.Children = new List<ForumModel>();
    }

    public ForumModel GetChild(string forumId)
    {
      return this.Id == forumId ? this : Enumerable.FirstOrDefault<ForumModel>(Enumerable.Select<ForumModel, ForumModel>((IEnumerable<ForumModel>) this.Children, (Func<ForumModel, ForumModel>) (c => c.GetChild(forumId))), (Func<ForumModel, bool>) (f => f != null));
    }

    public IEnumerable<ForumModel> AllChildren
    {
      get
      {
        yield return this;
        foreach (ForumModel child in this.Children)
        {
          foreach (ForumModel iteratedChild in child.AllChildren)
            yield return iteratedChild;
        }
      }
    }

    public List<ForumModel> Children { get; private set; }

    public string Name { get; private set; }

    public string Id { get; private set; }

    public string ParentId { get; private set; }

    public bool HasRootParent => this.ParentId == "-1";

    public override string ToString()
    {
      CultureInfo invariantCulture = CultureInfo.InvariantCulture;
      object[] objArray1 = new object[6];
      object[] objArray2 = objArray1;
      IEnumerable<ForumModel> allChildren = this.AllChildren;
      bool flag1 = false;
      string str1;
      if (allChildren != null)
      {
        IEnumerator enumerator = allChildren.GetEnumerator();
        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder1.Append((object) "[");
        while (enumerator.MoveNext())
        {
          if (flag1)
            stringBuilder1.Append((object) ", ");
          else
            flag1 = true;
          StringBuilder stringBuilder2 = stringBuilder1;
          string str2;
          if (enumerator.Current != null)
            str2 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}", new object[1]
            {
              enumerator.Current
            });
          else
            str2 = "null";
          stringBuilder2.Append((object) str2);
        }
        stringBuilder1.Append((object) "]");
        str1 = ((StringBuilder) stringBuilder1).ToString();
      }
      else
        str1 = "null";
      objArray2[0] = (object) str1;
      object[] objArray3 = objArray1;
      List<ForumModel> children = this.Children;
      bool flag2 = false;
      string str3;
      if (children != null)
      {
        IEnumerator enumerator = ((IEnumerable) children).GetEnumerator();
        StringBuilder stringBuilder3 = new StringBuilder();
        stringBuilder3.Append((object) "[");
        while (enumerator.MoveNext())
        {
          if (flag2)
            stringBuilder3.Append((object) ", ");
          else
            flag2 = true;
          StringBuilder stringBuilder4 = stringBuilder3;
          string str4;
          if (enumerator.Current != null)
            str4 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}", new object[1]
            {
              enumerator.Current
            });
          else
            str4 = "null";
          stringBuilder4.Append((object) str4);
        }
        stringBuilder3.Append((object) "]");
        str3 = ((StringBuilder) stringBuilder3).ToString();
      }
      else
        str3 = "null";
      objArray3[1] = (object) str3;
      object[] objArray4 = objArray1;
      string name = this.Name;
      string str5 = name == null ? "null" : name;
      objArray4[2] = (object) str5;
      object[] objArray5 = objArray1;
      string id = this.Id;
      string str6 = id == null ? "null" : id;
      objArray5[3] = (object) str6;
      object[] objArray6 = objArray1;
      string parentId = this.ParentId;
      string str7 = parentId == null ? "null" : parentId;
      objArray6[4] = (object) str7;
      objArray1[5] = (object) (bool) this.HasRootParent;
      object[] objArray7 = objArray1;
      return string.Format((IFormatProvider) invariantCulture, "{{T: \"ForumModel\", AllChildren: {0}, Children: {1}, Name: \"{2}\", Id: \"{3}\", ParentId: \"{4}\", HasRootParent: {5}}}", objArray7);
    }
  }
}
