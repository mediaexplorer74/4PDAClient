// FourPDA.AppServices.ViewModels.MainPivot.MainPivotViewModel

using Caliburn.Micro;
using FourPDA.AppServices.DataModels;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace FourPDA.AppServices.ViewModels.MainPivot
{
  public class MainPivotViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly Caliburn.Micro.INavigationService _navigationService;


    public MainPivotViewModel
    (
          IBusyIndicator busyIndicator,
          Caliburn.Micro.INavigationService navigationService,
          NewsViewModel newsViewModel,
          ForumsViewModel forumsViewModel
    )
    {

        //RnD
        this.LoadDataAsync();

        this._busyIndicator = busyIndicator;
        this._navigationService = navigationService;
        this.NewsViewModel = newsViewModel;
        this.ForumsViewModel = forumsViewModel;
    }


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


    public void GoBack() => this.LoadDataAsync();

    protected override void OnInitialize()
    {
        this.LoadDataAsync();
    }

    

    public void OpenNewsDetails(NewsItemDataModel newsItem)
    {
      ParameterExpression parameterExpression = default;

      /*this._navigationService.UriFor<NewsDetailsPageViewModel>().WithParam<string>(
          Expression.Lambda<Func<NewsDetailsPageViewModel, string>>(
              (Expression) Expression.Property((Expression) parameterExpression, 
              (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle)
              __methodref (NewsDetailsPageViewModel.get_NewsUri))), parameterExpression), 
          newsItem.Uri).Navigate();*/
      (Caliburn.Micro.NavigationExtensions.UriFor<NewsDetailsPageViewModel>
         ( this._navigationService )).Navigate();
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
