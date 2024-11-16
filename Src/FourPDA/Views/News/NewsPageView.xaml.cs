using FourPDA.AppServices;
using FourPDA.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FourPDA.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsPageView : Page
    {
        public NewsPageView()
        {
            this.InitializeComponent();
        }

        public NewsPageViewModel NewsPageViewModel
        {
            get
            {
                NewsPageViewModel dataContext = (NewsPageViewModel)this.DataContext;

                Debug.WriteLine("[i] NewsPageView - dataContext=" + dataContext.ToString());
                return dataContext;  
            }
        }

        protected /*override*/ void OnBackKeyPress(EventArgs e)
        {
            //base.OnBackKeyPress(e);
            //if (!this.ViewModel.CanReturnBack)
            //    return;
            //this.Title.IsBackTransition = true;

            //this.Title.NewContentTransitionEnded
            //  += new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);

            //this.ViewModel.GoBack();
            //e.Cancel = true;
        }

        private void Title_NewContentTransitionEnded(object sender, EventArgs e)
        {
            //this.Title.NewContentTransitionEnded
            //  -= new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
            
            //this.Title.IsBackTransition = false;
        }

    }
}

/*
 using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace FourPDA.WP7.Views.MainPivot
{
  public class NewsView : UserControl
  {
   
    public NewsView() => this.InitializeComponent();
   
  }
}
 */
