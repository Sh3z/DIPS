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
using Femore.UI.Dialogs.SaveNewDs;

namespace Femore.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for SaveNewDsStep1.xaml
    /// </summary>
    public partial class SaveNewDsStep1 : Window
    {
        public SaveNewDsStep1()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, RoutedEventArgs e)
        {
            SaveNewDsStep2 saveStep2 = new SaveNewDsStep2();
            saveStep2.Show();
        }
    }
}
