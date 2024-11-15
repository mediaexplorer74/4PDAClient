// FourPDA.AppServices.ViewModels.Forum.MainPageViewModel

using Caliburn.Micro;
using FourPDA.AppServices;
using FourPDA.AppServices.Controllers;
using FourPDA.AppServices.DataModels;
using FourPDA.AppServices.ViewModels;
using FourPDA.AppServices.ViewModels.MainPivot;
using FourPDA.Communication;
using FourPDA.Communication.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace FourPDA.ViewModels
{
  // A base implementation of Caliburn.Micro.IScreen (Conductor, etc.)
  public class MainPageViewModel : Screen // PropertyChangedBase ?
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly ForumController _forumController;
    private readonly ForumDataService _forumDataService;
    
    private readonly Caliburn.Micro.INavigationService _navigationService;

    private ForumModel _currentForum;

        private string ForumId_BackingField;
        public string ForumId
        {
            get => this.ForumId_BackingField;
            set
            {
                if (string.Equals(this.ForumId_BackingField, value, StringComparison.Ordinal))
                    return;
                this.ForumId_BackingField = value;
                this.NotifyOfPropertyChange(nameof(ForumId));
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
                this.NotifyOfPropertyChange(nameof(ForumName));
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
                this.NotifyOfPropertyChange(nameof(AllItems));
            }
        }

        private NewsViewModel NewsViewModel_BackingField = new NewsViewModel(new NewsDataService());
        public NewsViewModel NewsViewModel
    {
        get => this.NewsViewModel_BackingField;
        set
        {
            if (this.NewsViewModel_BackingField == value)
                return;
            this.NewsViewModel_BackingField = value;
            this.NotifyOfPropertyChange(nameof(NewsViewModel));
        }
    }

    private ForumsViewModel ForumsViewModel_BackingField = new ForumsViewModel(new ForumController(), 
        new ForumDataService(default), default, default);
    public ForumsViewModel ForumsViewModel
    {
        get => this.ForumsViewModel_BackingField;
        set
        {
            if (this.ForumsViewModel_BackingField == value)
                return;
            this.ForumsViewModel_BackingField = value;
            this.NotifyOfPropertyChange(nameof(ForumsViewModel));
        }
    }

     

     //---------------------------
        public MainPageViewModel
        (
             ForumController forumController,
             ForumDataService forumDataService,
             IBusyIndicator busyIndicator,
             Caliburn.Micro.INavigationService navigationService
        )
        {
            this._forumController = forumController;
            this._forumDataService = forumDataService;
            this._busyIndicator = busyIndicator;
            this._navigationService = navigationService;
        }

        // --------------------------

   

    public bool CanReturnBack => !this._currentForum.HasRootParent;

    public void SelectForum(ForumDataModel forum)
    {
       this.LoadDataAsync();//(forum.Id); 
    }

    public void GoBack()
    {
        this.LoadDataAsync();//(this._currentForum.ParentId); 
    }



    public void OpenNewsDetails(NewsItemDataModel newsItem)
    {
            //ParameterExpression parameterExpression;

            /*
             This code creates a new instance of NewsDetailsPageViewModel, 
            sets the NewsUri property to newsItem.Uri, 
            and then navigates to the NewsDetailsPageViewModel. 
            Note that this assumes that NewsDetailsPageViewModel has a NewsUri property. 
            If it doesn't, you'll need to modify the code accordingly.             
             */
         
            //not needed ?
            //NewsDetailsPageViewModel newsDetailsPageViewModel = new NewsDetailsPageViewModel(
            //    (NewsDataService)this._navigationService, this._busyIndicator);

            (Caliburn.Micro.NavigationExtensions.UriFor<NewsDetailsPageViewModel>
            (this._navigationService)).Navigate();
        }


    protected override void OnInitialize()
    {
      //base.OnInitialize(); //?
                           
      this.LoadDataAsync(); //this.LoadDataAsync(this.ForumId);
        }

        /*
    private async void LoadDataAsync(string forumId)
    {
        //using (this._busyIndicator.StartJob())
        try
        {
            if (this.AllItems != null)
                this.AllItems.Clear();
            ForumModel root = await this._forumDataService.LoadForumHierarchyAsync();
            this._currentForum = root.GetChild(forumId);
            this.ForumName = this._currentForum.Name;
            List<ForumTopicModel> topics = await this._forumDataService.LoadTopicsAsync(forumId);
            this.AllItems = new BindableCollection<object>(((IEnumerable<ForumModel>)
                this._currentForum.Children).Select<ForumModel, ForumDataModel>(
                (Func<ForumModel, ForumDataModel>)(
                x => this._forumController.CreateDataModel(x, this.ForumName)))
                .Cast<object>().Concat<object>(((IEnumerable<ForumTopicModel>)topics)
                .Select<ForumTopicModel, TopicDataModel>((Func<ForumTopicModel, TopicDataModel>)
                (t => this._forumController.CreateDataModel(t))).Cast<object>()));
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] MainPageViewModel - LoadDataAsync ex.: " + ex.Message);
        }
       
    }//LoadDataAsync
        */

              

        private async void LoadDataAsync()
        {
            //TODO: busyIndicator
            //using (this._busyIndicator.StartJob())
            try
            {
                await this.NewsViewModel.LoadDataAsync();
                await this.ForumsViewModel.LoadDataAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] MainPageViewModel - LoadDataAsync ex: " + ex.Message);
            }
        }

        public void RefreshData() => this.LoadDataAsync();



        //RnD
        //MainPageViewModel
        public MainPageViewModel()
        {
            //TEST
            /*
            this.LoadDataAsync();//(this.ForumId);

            this.ForumName = "Windows Phone";

            BindableCollection<object> bindableCollection = new BindableCollection<object>();
            bindableCollection.Add((object)new ForumDataModel()
            {
                Title = "SubForum"
            });
            bindableCollection.Add((object)new ForumDataModel()
            {
                Title = "SubForum2"
            });
            bindableCollection.Add((object)new TopicDataModel()
            {
                Name = "How to buy windowsphone"
            });
            bindableCollection.Add((object)new TopicDataModel()
            {
                Name = "How to buy windowsphone2"
            });
            bindableCollection.Add((object)new TopicDataModel()
            {
                Name = "How to buy windowsphone3"
            });
            this.AllItems = bindableCollection;
            */
        }//MainPageViewModel
    }
}
