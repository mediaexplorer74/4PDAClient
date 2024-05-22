// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.DataModels.NewsItemDataModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Caliburn.Micro;
using System;

#nullable disable
namespace ForPDA.AppServices.DataModels
{
  public class NewsItemDataModel : PropertyChangedBase
  {
    public string Title
    {
      get => this.\u003CTitle\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CTitle\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CTitle\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (Title));
      }
    }

    public string Body
    {
      get => this.\u003CBody\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CBody\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CBody\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (Body));
      }
    }

    public string Timestamp
    {
      get => this.\u003CTimestamp\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CTimestamp\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CTimestamp\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (Timestamp));
      }
    }

    public string Uri
    {
      get => this.\u003CUri\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CUri\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CUri\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (Uri));
      }
    }
  }
}
