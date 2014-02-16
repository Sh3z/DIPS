using System;
using System.Collections.Generic;
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
using System.Windows.Resources;
using DIPS.ViewModel;
using DIPS.UI.Pages;
using DIPS.UI.Pages.LoadNewDataset;
using DIPS.Processor.Client;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets or sets the <see cref="IProcessingService"/> connected to
        /// the active DIPS processor.
        /// </summary>
        public IProcessingService Service
        {
            get;
            set;
        }


        private void btnViewExistingDS_Click(object sender, RoutedEventArgs e)
        {
            // Create the window and provide it with the presentation layer.
            //PrototypeViewModel vm = new PrototypeViewModel();
            ViewExistingDataSet viewWindow = new ViewExistingDataSet();
            viewWindow.WindowNav = this;

            //this.NavigationService.Navigate(viewWindow);
        }

        private void btnLoadDataset_Click(object sender, RoutedEventArgs e)
        {
            LoadNewDSStep1 loadDS1 = new LoadNewDSStep1();
            //this.NavigationService.Navigate(loadDS1);
        }

        private void clearField()
        {
            txtExplanation.Text = string.Empty;
        }

        private void btnAbout_MouseEnter(object sender, MouseEventArgs e)
        {
            txtExplanation.Text = "This screen will explain who has made this application and what it does.";
        }

        private void btnAbout_MouseLeave(object sender, MouseEventArgs e)
        {
            clearField();
        }

        private void btnLoadDataset_MouseEnter(object sender, MouseEventArgs e)
        {
           txtExplanation.Text = "This screen processes selected images using techniques created in the algorithm builder.";
        }

        private void btnCreateAlgorithm_MouseEnter(object sender, MouseEventArgs e)
        {
            txtExplanation.Text = "Ths screen allows you to create your own algorithm for processing images.";
        }

        private void btnCreateAlgorithm_MouseLeave(object sender, MouseEventArgs e)
        {
            clearField();
        }

        private void btnLoadDataset_MouseLeave(object sender, MouseEventArgs e)
        {
            clearField();
        }

        private void btnViewExistingDS_MouseEnter(object sender, MouseEventArgs e)
        {
           txtExplanation.Text = "This screen allows you to view datasets that have already been processed";
        }

        private void btnViewExistingDS_MouseLeave(object sender, MouseEventArgs e)
        {
            clearField();
        }

        private void btnCreateAlgorithm_Click(object sender, RoutedEventArgs e)
        {
            ViewAlgorithms viewAlgorithmScreen = new ViewAlgorithms();
            viewAlgorithmScreen.Service = Service;
            //this.NavigationService.Navigate(viewAlgorithmScreen);
        }
    }
}
