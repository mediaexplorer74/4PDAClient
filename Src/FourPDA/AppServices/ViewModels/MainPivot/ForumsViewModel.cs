﻿// ForPDA.AppServices.ViewModels.MainPivot.ForumsViewModel

using Caliburn.Micro;
using ForPDA.AppServices.Controllers;
using ForPDA.AppServices.DataModels;
using ForPDA.AppServices.ViewModels.Forum;
using ForPDA.Communication;
using ForPDA.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

#nullable disable
namespace ForPDA.AppServices.ViewModels.MainPivot
{
  public class ForumsViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly INavigationService _navigationService;
    private readonly ForumController _forumController;
    private readonly ForumDataService _forumDataService;

    public ForumsViewModel(
      ForumController forumController,
      ForumDataService forumDataService,
      IBusyIndicator busyIndicator,
      INavigationService navigationService)
    {
      this._forumController = forumController;
      this._forumDataService = forumDataService;
      this._busyIndicator = busyIndicator;
      this._navigationService = navigationService;
    }

    private List<ForumDataModel> Forums_BackingField;
    public List<ForumDataModel> Forums
    {
      get => this.Forums_BackingField;
      set
      {
        if (this.Forums_BackingField == value)
          return;
        this.Forums_BackingField = value;
        this.NotifyOfPropertyChange(nameof (Forums));
      }
    }

    public void OpenForum(ForumDataModel forum)
    {
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
     
      // ISSUE: method reference
      // ISSUE: method reference
      //this._navigationService.UriFor<ForumPageViewModel>().WithParam<string>(
      //    Expression.Lambda<Func<ForumPageViewModel, string>>(
      //        (Expression) Expression.Property((Expression) parameterExpression1,
      //        (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle) 
      //        __methodref (ForumPageViewModel.get_ForumName))), parameterExpression1),
      //    forum.Title).WithParam<string>(Expression.Lambda<Func<ForumPageViewModel, string>>(
      //        (Expression) Expression.Property((Expression) parameterExpression2, 
      //        (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle) __methodref
      //        (ForumPageViewModel.get_ForumId))), parameterExpression2), forum.Id).Navigate();
    }

    public async Task LoadDataAsync()
    {
      using (this._busyIndicator.StartJob())
      {
        ForumModel rootForum = await this._forumDataService.LoadForumHierarchyAsync();
        this.Forums = Enumerable.ToList<ForumDataModel>(((IEnumerable<ForumModel>) rootForum.Children)
            .Select<ForumModel, ForumDataModel>(new Func<ForumModel, ForumDataModel>(
                this._forumController.CreateDataModel)));
      }
    }
  }

    public interface INavigationService
    {
    }
}
