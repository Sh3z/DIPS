using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DIPS.UI.Pages;
using DIPS.Processor.Client;
using DIPS.ViewModel.UserInterfaceVM;
using DIPS.ViewModel;

namespace DIPS.UI
{
    /// <summary>
    /// Interaction logic for MainNavi.xaml
    /// </summary>
    public partial class MainNavi : Window
    {
        public MainNavi()
        {
            InitializeComponent();
            BaseViewModel.OverallFrame = this.MainNaviControl;

            this.MainNaviControl.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            //this.MainNaviControl.Content = _main;
        }



        /// <summary>
        /// Contains a reference to the root page view.
        /// </summary>
        
        private static ViewExistingDatasetViewModel _main;
        public ViewExistingDatasetViewModel Main
        {
            get
            {
                return _main;
            }
        }
    }
}
