// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Caliburn.Micro;
using ForPDA.AppServices.DataModels;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace ForPDA.AppServices.ViewModels.MainPivot
{
  public class MainPivotViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly INavigationService _navigationService;

    public NewsViewModel NewsViewModel
    {
      get => this.\u003CNewsViewModel\u003Ek__BackingField;
      set
      {
        if (this.\u003CNewsViewModel\u003Ek__BackingField == value)
          return;
        this.\u003CNewsViewModel\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (NewsViewModel));
      }
    }

    public ForumsViewModel ForumsViewModel
    {
      get => this.\u003CForumsViewModel\u003Ek__BackingField;
      set
      {
        if (this.\u003CForumsViewModel\u003Ek__BackingField == value)
          return;
        this.\u003CForumsViewModel\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (ForumsViewModel));
      }
    }

    public MainPivotViewModel(
      IBusyIndicator busyIndicator,
      INavigationService navigationService,
      NewsViewModel newsViewModel,
      ForumsViewModel forumsViewModel)
    {
      this._busyIndicator = busyIndicator;
      this._navigationService = navigationService;
      this.NewsViewModel = newsViewModel;
      this.ForumsViewModel = forumsViewModel;
    }

    public void OpenNewsDetails(NewsItemDataModel newsItem)
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      this._navigationService.UriFor<NewsDetailsPageViewModel>().WithParam<string>(Expression.Lambda<Func<NewsDetailsPageViewModel, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle) __methodref (NewsDetailsPageViewModel.get_NewsUri))), parameterExpression), newsItem.Uri).Navigate();
    }

    protected override void OnInitialize() => this.LoadDataAsync();

    private async void LoadDataAsync()
    {
      using (this._busyIndicator.StartJob())
      {
        await this.NewsViewModel.LoadDataAsync();
        await this.ForumsViewModel.LoadDataAsync();
      }
    }

    public void RefreshData() => this.LoadDataAsync();
  }
}
