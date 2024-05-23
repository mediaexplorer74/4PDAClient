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


namespace FourPDA.Views.MainPivot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForumsView : Page
    {
        public ForumsView()
        {
            this.InitializeComponent();
        }
    }
}

/*
 // FourPDA.Views.MainPivot.ForumsView


using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace FourPDA.Views.MainPivot
{
  public class ForumsView : UserControl
  {
    internal Grid LayoutRoot;
    private bool _contentLoaded;

    public ForumsView() => this.InitializeComponent();

    //[DebuggerNonUserCode]
    //public void InitializeComponent()
    //{
    //  if (this._contentLoaded)
    //    return;
    //  this._contentLoaded = true;
    //  Application.LoadComponent((object) this, new Uri("/FourPDA;component/Views/MainPivot/ForumsView.xaml", UriKind.Relative));
    //  this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
    }
  }
}
 
 */
