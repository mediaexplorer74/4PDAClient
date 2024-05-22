// Decompiled with JetBrains decompiler
// Type: FourPDA.Bootstrapper
// Assembly: FourPDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\ForPDA\FourPDA.dll

//using Caliburn.Micro;
using ForPDA.AppServices;
using ForPDA.AppServices.Controllers;
using ForPDA.AppServices.ViewModels;
using ForPDA.AppServices.ViewModels.Forum;
using ForPDA.AppServices.ViewModels.MainPivot;
using ForPDA.Communication;
using FourPDA.Controls;
//using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
//using Windows.UI.Xaml.Controls;
//using Telerik.Windows.Controls;

#nullable disable
namespace FourPDA
{
  public class Bootstrapper //: PhoneBootstrapper
  {
    private PhoneContainer _container;

       /*
        protected override void Configure()
        {
          this._container = new PhoneContainer();
          this.RegisterViewModels();
          this.RegisterServices();
          this.RegisterConventions();
          this._container.RegisterPhoneServices((Frame) this.RootFrame);
        }

        private void RegisterConventions()
        {
          AssemblySource.Instance.Add(typeof (MainPivotViewModel).Assembly);
          ViewModelLocator.AddNamespaceMapping("FourPDA.Views.MainPivot", "ForPDA.AppServices.ViewModels.MainPivot");
          ViewLocator.AddNamespaceMapping("ForPDA.AppServices.ViewModels.MainPivot", "FourPDA.Views.MainPivot");
          ViewModelLocator.AddNamespaceMapping("FourPDA.Views.Forum", "ForPDA.AppServices.ViewModels.Forum", "Page");
          ViewLocator.AddNamespaceMapping("ForPDA.AppServices.ViewModels.Forum", "FourPDA.Views.Forum", "Page");
          ViewModelLocator.AddNamespaceMapping("FourPDA.Views", "ForPDA.AppServices.ViewModels", "Page");
          ViewLocator.AddNamespaceMapping("ForPDA.AppServices.ViewModels", "FourPDA.Views", "Page");
        }

        private void RegisterServices()
        {
          this._container.PerRequest<NewsDataService>();
          this._container.PerRequest<ForumDataService>();
          this._container.PerRequest<ForumController>();
          this._container.PerRequest<HttpCommunicator>();
          this._container.Handler<IBusyIndicator>((Func<SimpleContainer, object>) 
              (c => (object) GlobalBusyIndicator.Create()));
        }

        private void RegisterViewModels()
        {
          this._container.PerRequest<MainPivotViewModel>();
          this._container.PerRequest<NewsViewModel>();
          this._container.PerRequest<ForumsViewModel>();
          this._container.PerRequest<ForumPageViewModel>();
          this._container.PerRequest<NewsDetailsPageViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
          return this._container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
          return this._container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) => this._container.BuildUp(instance);

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
          RadPhoneApplicationFrame frame = new RadPhoneApplicationFrame();
          ScreenHelper.Init((PhoneApplicationFrame) frame);
          return (PhoneApplicationFrame) frame;
        }
       */
  }
}
