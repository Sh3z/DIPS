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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DIPS.UI.Pages.LoadNewDataset;

namespace DIPS.UI.Pages.LoadNewDataset
{
    /// <summary>
    /// Interaction logic for LoadNewDSStep2.xaml
    /// </summary>
    public partial class LoadNewDSStep2 : Page
    {
        public LoadNewDSStep2()
        {
            InitializeComponent();
        }

        private void btnSelection_Click(object sender, RoutedEventArgs e)
        {
            LoadNewDSStep3 loadDS3 = new LoadNewDSStep3();
            this.NavigationService.Navigate(loadDS3);
        }
    }
}
