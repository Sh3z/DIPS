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
    public partial class LoadNewDSStep1 : UserControl
    {
        private List<FileInfo> _listOfFiles;

        public List<FileInfo> ListofFiles
        {
            get { return _listOfFiles; }
            set { _listOfFiles = value; }
        }
        

        public LoadNewDSStep1()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, RoutedEventArgs e)
        {
            //open dialog for user to select files
                selectFilesForDataset();
        }

        private Boolean validateFields()
        {
            //if ()
            //{
            //    MessageBox.Show("No files have been selected for processing.", "No files selected.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    return false;
            //}

            return true;
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
                ListofFiles = new List<FileInfo>();

                foreach (string file in dialogOpen.FileNames)
                {
                    //lstFiles.Items.Add(file);

                    FileInfo uploadFile = new FileInfo(file);
                    ListofFiles.Add(uploadFile);
                }
            }

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                LoadNewDSStep2 loadDS2 = new LoadNewDSStep2();
                loadDS2.ListofFiles = ListofFiles;

                //this.NavigationService.Navigate(loadDS2);
            }
        }
    }
}
