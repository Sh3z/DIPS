using System;
using System.IO;
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
using Microsoft.Win32;
using System.Collections.ObjectModel;

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
            //open dialog for user to select files
            selectFilesForDataset();

            
            LoadNewDSStep2 loadDS2 = new LoadNewDSStep2();
            this.NavigationService.Navigate(loadDS2);
        }

        private void selectFilesForDataset()
        {
            Stream myStream;
            OpenFileDialog dialogOpen = new OpenFileDialog();
            ;
            //Setup properties for open file dialog
            dialogOpen.InitialDirectory = "C:\\";
            dialogOpen.Filter = @"Bitmaps|*.bmp|Jpgs|*.jpg";
            dialogOpen.FilterIndex = 1;
            dialogOpen.Multiselect = true;
            dialogOpen.Title = "Please select image files which are going to be part of this dataset";

            Nullable<bool> isOkay = dialogOpen.ShowDialog();
            String[] strFiles = dialogOpen.FileNames;
                
            if (isOkay == true)
            {
                foreach (String file in strFiles)
                {
                    //lstFiles.
                }
            }

        }
    }
}
