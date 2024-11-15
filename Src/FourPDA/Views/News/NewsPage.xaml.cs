using FourPDA.AppServices.ViewModels.MainPivot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


namespace FourPDA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
        }

        public NewsViewModel ViewModel
        {
            get => (NewsViewModel)((FrameworkElement)this).DataContext;
        }

        protected /*override*/ void OnBackKeyPress(EventArgs e)
        {
            //base.OnBackKeyPress(e);
            if (!this.ViewModel.CanReturnBack)
                return;
            //this.Title.IsBackTransition = true;
            //this.Title.NewContentTransitionEnded += new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
            this.ViewModel.GoBack();
            //e.Cancel = true;
        }

        private void Title_NewContentTransitionEnded(object sender, EventArgs e)
        {
            //this.Title.NewContentTransitionEnded -= new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
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
