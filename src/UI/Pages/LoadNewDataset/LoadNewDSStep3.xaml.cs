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
using DIPS.UI.Pages;
using System.Diagnostics;
using System.Runtime.InteropServices; 

namespace DIPS.UI.Pages.LoadNewDataset
{
    /// <summary>
    /// Interaction logic for LoadNewDSStep3.xaml
    /// </summary>
    public partial class LoadNewDSStep3 : Page
    {
        public LoadNewDSStep3()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Images now being processed");

            if (chkTurnOffComputer.IsChecked == true)
            {
                Process.Start("shutdown", "/s /t 0");
            }
            else
            {
                MainPage main = new MainPage();
                this.NavigationService.Navigate(main);
            }
        }
    }
}
