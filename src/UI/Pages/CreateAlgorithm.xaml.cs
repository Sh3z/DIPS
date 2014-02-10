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
using DIPS.Database.Objects;
using DIPS.UI.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;
using DIPS.Processor.Client;
using System.Diagnostics;
using System.Xml.Linq;
using DIPS.Processor.XML.Decompilation;
using DIPS.UI.CustomControls;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for CreateAlgorithm.xaml
    /// </summary>
    public partial class CreateAlgorithm : Page
    {
        public CreateAlgorithm()
        {
            InitializeComponent();
        }


        private Boolean validateFields()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter a name for the algorithm.","Name missing",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                MessageBox.Show("Algorithm saved", "Save successful", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.GoBack();
            }
           
        }
    }

   

}
