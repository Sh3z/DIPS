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
using DIPS.Database.Objects;
using System.IO;


namespace DIPS.UI.Pages.LoadNewDataset
{
    /// <summary>
    /// Interaction logic for LoadNewDSStep3.xaml
    /// </summary>
    public partial class LoadNewDSStep3 : UserControl
    {
        private List<FileInfo> _listOfFiles;
        public List<FileInfo> ListofFiles
        {
            get { return _listOfFiles; }
            set { _listOfFiles = value; }
        }

        public LoadNewDSStep3()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
           
            if (cboPostProcess.Text == "Shut down")
            {
                MessageBoxResult result = MessageBox.Show("You have chosen to turn off the computer after processing - are you sure?", "Shut down computer after processing?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Images processed.","Processing Complete",MessageBoxButton.OK,MessageBoxImage.Information);
                    Process.Start("shutdown", "/s /t 0");
                }
            } else if (cboPostProcess.Text == "Sleep") {
                 MessageBoxResult resultSleep = MessageBox.Show("You have chosen to put the computer into hibernate mode after processing - are you sure?", "Hibernate computer after processing?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultSleep == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Images processed.","Processing Complete",MessageBoxButton.OK,MessageBoxImage.Information);
                   // Hibernate
                    Process.Start("shutdown", "/h /f");
                }
            
            } else
            {
                MessageBox.Show("Images processed","Processing complete.",MessageBoxButton.OK,MessageBoxImage.Information);
            }

            MainPage main = new MainPage();
            //this.NavigationService.Navigate(main);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstLoadedFiles.ItemsSource = ListofFiles;
        }
    }
}
