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
using DIPS.UI.Dialogs;
using DIPS.ViewModel;
using DIPS.UI.Pages;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnViewExistingDS_Click(object sender, RoutedEventArgs e)
        {
            // Create the window and provide it with the presentation layer.
            PrototypeViewModel vm = new PrototypeViewModel();
            ViewExistingDataSet viewWindow = new ViewExistingDataSet(vm);
            viewWindow.WindowNav = this;

            this.NavigationService.Navigate(viewWindow);
        }

        private void btnLoadDataset_Click(object sender, RoutedEventArgs e)
        {
            SaveNewDsStep1 saveDS = new SaveNewDsStep1();
            saveDS.Show();
        }
    }
}
