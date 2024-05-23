using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FourPDA.Views.MainPivot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPivotView : Page
    {
        public MainPivotView()
        {
            this.InitializeComponent();
        }
    }
}

/*
 // FourPDA.Views.MainPivot.MainPivotView

//using FluentAppBar;
using ForPDA.AppServices.ViewModels.MainPivot;
using FourPDA.Controls;
//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Views.MainPivot
{
  public class MainPivotView : Page
  {
    //internal Grid LayoutRoot;
    //private bool _contentLoaded;

    public MainPivotView()
    {
      this.InitializeComponent();
      ((FrameworkElement) this).Loaded += new EventHandler(this.PageLoaded);
    }

    private void PageLoaded(object sender, EventArgs e)
    {
      AppBar.Setup<MainPivotViewModel>((Page) this).Third((Action<IApplicationBarIconButton, MainPivotViewModel>)
          ((btn, vm) => vm.RefreshData()));
    }

    //[DebuggerNonUserCode]
    //public void InitializeComponent()
    //{
    //  if (this._contentLoaded)
    //    return;
    //  this._contentLoaded = true;
    //  Application.LoadComponent((object) this, new Uri("/FourPDA;component/Views/MainPivot/MainPivotView.xaml", UriKind.Relative));
    //  this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
    }
  }
}
 
 */
