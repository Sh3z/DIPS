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

namespace Femore.UI.Dialogs.SaveNewDs
{
    /// <summary>
    /// Interaction logic for SaveNewDsStep2.xaml
    /// </summary>
    public partial class SaveNewDsStep2 : Window
    {
        public SaveNewDsStep2()
        {
            InitializeComponent();
        }

        private void btnSelection_Click(object sender, RoutedEventArgs e)
        {
            SaveNewDsStep3 saveStep3 = new SaveNewDsStep3();
            saveStep3.Show();
        }
    }
}
