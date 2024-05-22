// Decompiled with JetBrains decompiler
// Type: ForPDA.AppServices.ViewModels.Forum.ForumPageViewModel
// Assembly: ForPDA.AppServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D325BF8E-CDA8-4E31-B95E-BD3BD3D2F348
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\ForPDA.AppServices.dll

using Caliburn.Micro;
using ForPDA.AppServices.Controllers;
using ForPDA.AppServices.DataModels;
using ForPDA.Communication;
using ForPDA.Communication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace ForPDA.AppServices.ViewModels.Forum
{
  public class ForumPageViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly ForumController _forumController;
    private readonly ForumDataService _forumDataService;
    private readonly INavigationService _navigationService;
    private ForumModel _currentForum;

    public ForumPageViewModel()
    {
      if (!DesignerProperties.IsInDesignTool)
        return;
      this.ForumName = "Windows Phone";
      BindableCollection<object> bindableCollection = new BindableCollection<object>();
      bindableCollection.Add((object) new ForumDataModel()
      {
        Title = "SubForum"
      });
      bindableCollection.Add((object) new ForumDataModel()
      {
        Title = "SubForum2"
      });
      bindableCollection.Add((object) new TopicDataModel()
      {
        Name = "How to buy windowsphone"
      });
      bindableCollection.Add((object) new TopicDataModel()
      {
        Name = "How to buy windowsphone2"
      });
      bindableCollection.Add((object) new TopicDataModel()
      {
        Name = "How to buy windowsphone3"
      });
      this.AllItems = bindableCollection;
    }

    public ForumPageViewModel(
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

    public string ForumId
    {
      get => this.\u003CForumId\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CForumId\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CForumId\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (ForumId));
      }
    }

    public string ForumName
    {
      get => this.\u003CForumName\u003Ek__BackingField;
      set
      {
        if (string.Equals(this.\u003CForumName\u003Ek__BackingField, value, StringComparison.Ordinal))
          return;
        this.\u003CForumName\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (ForumName));
      }
    }

    public BindableCollection<object> AllItems
    {
      get => this.\u003CAllItems\u003Ek__BackingField;
      set
      {
        if (this.\u003CAllItems\u003Ek__BackingField == value)
          return;
        this.\u003CAllItems\u003Ek__BackingField = value;
        this.NotifyOfPropertyChange(nameof (AllItems));
      }
    }

    public bool CanReturnBack => !this._currentForum.HasRootParent;

    public void SelectForum(ForumDataModel forum) => this.LoadDataAsync(forum.Id);

    public void GoBack() => this.LoadDataAsync(this._currentForum.ParentId);

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.LoadDataAsync(this.ForumId);
    }

    private async void LoadDataAsync(string forumId)
    {
      using (this._busyIndicator.StartJob())
      {
        if (this.AllItems != null)
          this.AllItems.Clear();
        ForumModel root = await this._forumDataService.LoadForumHierarchyAsync();
        this._currentForum = root.GetChild(forumId);
        this.ForumName = this._currentForum.Name;
        List<ForumTopicModel> topics = await this._forumDataService.LoadTopicsAsync(forumId);
        this.AllItems = new BindableCollection<object>(((IEnumerable<ForumModel>) this._currentForum.Children).Select<ForumModel, ForumDataModel>((Func<ForumModel, ForumDataModel>) (x => this._forumController.CreateDataModel(x, this.ForumName))).Cast<object>().Concat<object>(((IEnumerable<ForumTopicModel>) topics).Select<ForumTopicModel, TopicDataModel>((Func<ForumTopicModel, TopicDataModel>) (t => this._forumController.CreateDataModel(t))).Cast<object>()));
      }
    }
  }
}
