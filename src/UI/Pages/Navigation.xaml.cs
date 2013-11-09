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
using Femore.UI;
using Femore.ViewModel;

namespace Femore.UI.Pages
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Window
    {
        public Navigation()
        {
            InitializeComponent();
        }

        private void btnViewExistingDS_Click(object sender, RoutedEventArgs e)
        {
            // Create the window and provide it with the presentation layer.
            PrototypeViewModel vm = new PrototypeViewModel();
            ViewExistingDataset viewWindow = new ViewExistingDataset(vm);
            viewWindow.WindowNav = this;
            viewWindow.Show();
        }
    }
}
