// ForPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel

using Caliburn.Micro;
using ForPDA.AppServices.DataModels;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace ForPDA.AppServices.ViewModels.MainPivot
{
  public class MainPivotViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly INavigationService _navigationService;

    private NewsViewModel NewsViewModel_BackingField;
    public NewsViewModel NewsViewModel
    {
      get => this.NewsViewModel_BackingField;
      set
      {
        if (this.NewsViewModel_BackingField == value)
          return;
        this.NewsViewModel_BackingField = value;
        this.NotifyOfPropertyChange(nameof (NewsViewModel));
      }
    }

    private  ForumsViewModel ForumsViewModel_BackingField;        

    public ForumsViewModel ForumsViewModel
    {
      get => this.ForumsViewModel_BackingField;
      set
      {
        if (this.ForumsViewModel_BackingField == value)
          return;
        this.ForumsViewModel_BackingField = value;
        this.NotifyOfPropertyChange(nameof (ForumsViewModel));
      }
    }

    public MainPivotViewModel(
      IBusyIndicator busyIndicator,
      INavigationService navigationService,
      NewsViewModel newsViewModel,
      ForumsViewModel forumsViewModel)
    {
    //RnD
    //this.LoadDataAsync();

    this._busyIndicator = busyIndicator;
      this._navigationService = navigationService;
      this.NewsViewModel = newsViewModel;
      this.ForumsViewModel = forumsViewModel;
    }

    public void OpenNewsDetails(NewsItemDataModel newsItem)
    {
      ParameterExpression parameterExpression = default;

            //RnD
      // ISSUE: method reference
      //this._navigationService.UriFor<NewsDetailsPageViewModel>().WithParam<string>(
      //    Expression.Lambda<Func<NewsDetailsPageViewModel, string>>(
      //        (Expression) Expression.Property((Expression) parameterExpression, 
      //        (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle)
      //        __methodref (NewsDetailsPageViewModel.get_NewsUri))), parameterExpression), newsItem.Uri).Navigate();
    }

    protected override void OnInitialize()
    {
        this.LoadDataAsync();
    }

    private async void LoadDataAsync()
    {
      using (this._busyIndicator.StartJob())
      {
        await this.NewsViewModel.LoadDataAsync();
        await this.ForumsViewModel.LoadDataAsync();
      }
    }

    public void RefreshData()
    {
        this.LoadDataAsync();
    }

  }//class end
}
