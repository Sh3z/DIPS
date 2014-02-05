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

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for ViewAlgorithms.xaml
    /// </summary>
    public partial class ViewAlgorithms : Page
    {
        public ViewAlgorithms()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CreateAlgorithm createAlgorithmScreen = new CreateAlgorithm();
            this.NavigationService.Navigate(createAlgorithmScreen);
        }
    }
}
