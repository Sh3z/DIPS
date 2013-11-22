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
    /// Interaction logic for LoadNewDSStep1.xaml
    /// </summary>
    public partial class LoadNewDSStep1 : Page
    {
        public LoadNewDSStep1()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, RoutedEventArgs e)
        {
            LoadNewDSStep2 loadDS2 = new LoadNewDSStep2();
            this.NavigationService.Navigate(loadDS2);
        }
    }
}
