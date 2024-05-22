// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.ViewModels.NewsDetailsPageViewModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Caliburn.Micro;
using ForPDA.Communication;
using System;

#nullable disable
namespace ForPDA.AppServices.ViewModels
{
  public class NewsDetailsPageViewModel : Screen
  {
    private readonly NewsDataService _newsDataService;
    private readonly IBusyIndicator _busyIndicator;
    private IBrowserView _view;

    public NewsDetailsPageViewModel(NewsDataService newsDataService, IBusyIndicator busyIndicator)
    {
      this._newsDataService = newsDataService;
      this._busyIndicator = busyIndicator;
    }

    public string NewsUri
    {
      get => this.\u003CNewsUri\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CNewsUri\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CNewsUri\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (NewsUri));
      }
    }

    public bool IsLoaded
    {
      get => this.\u003CIsLoaded\u003Ek__BackingField;
      set
      {
        if (this.\u003CIsLoaded\u003Ek__BackingField == value)
          return;
        this.\u003CIsLoaded\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (IsLoaded));
      }
    }

    protected override void OnViewLoaded(object view)
    {
      base.OnViewLoaded(view);
      this._view = (IBrowserView) view;
      this.LoadPageAsync();
    }

    private async void LoadPageAsync()
    {
      using (this._busyIndicator.StartJob())
      {
        string html = await this._newsDataService.LoadNewsHtmlPage(this.NewsUri, ScreenHelper.IsDarkTheme);
        this._view.LoadContent(html);
        this.IsLoaded = true;
      }
    }
  }
}
