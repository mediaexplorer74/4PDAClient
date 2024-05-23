// ForPDA.AppServices.ViewModels.Forum.MainPageViewModel

using Caliburn.Micro;
using ForPDA.AppServices;
using ForPDA.AppServices.Controllers;
using ForPDA.AppServices.DataModels;
using ForPDA.Communication;
using ForPDA.Communication.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace ForPDA.ViewModels
{
  public class MainPageViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly ForumController _forumController;
    private readonly ForumDataService _forumDataService;
    private readonly INavigationService _navigationService;
    private ForumModel _currentForum;

    public MainPageViewModel()
    {           
      //RnD
      this.LoadDataAsync(this.ForumId);

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

    public MainPageViewModel(
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

    private string ForumId_BackingField;
    public string ForumId
    {
      get => this.ForumId_BackingField;
      set
      {
        if (string.Equals(this.ForumId_BackingField, value, StringComparison.Ordinal))
          return;
        this.ForumId_BackingField = value;
        this.NotifyOfPropertyChange(nameof (ForumId));
      }
    }

    private string ForumName_BackingField;
    public string ForumName
    {
      get => this.ForumName_BackingField;
      set
      {
        if (string.Equals(this.ForumName_BackingField, value, StringComparison.Ordinal))
          return;
        this.ForumName_BackingField = value;
        this.NotifyOfPropertyChange(nameof (ForumName));
      }
    }

    private BindableCollection<object> AllItems_BackingField; 
    public BindableCollection<object> AllItems
    {
      get => this.AllItems_BackingField;
      set
      {
        if (this.AllItems_BackingField == value)
          return;
        this.AllItems_BackingField = value;
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
        this.AllItems = new BindableCollection<object>(((IEnumerable<ForumModel>) 
            this._currentForum.Children).Select<ForumModel, ForumDataModel>(
            (Func<ForumModel, ForumDataModel>) (
            x => this._forumController.CreateDataModel(x, this.ForumName)))
            .Cast<object>().Concat<object>(((IEnumerable<ForumTopicModel>) topics)
            .Select<ForumTopicModel, TopicDataModel>((Func<ForumTopicModel, TopicDataModel>)
            (t => this._forumController.CreateDataModel(t))).Cast<object>()));
      }
       
    }
  }
}
