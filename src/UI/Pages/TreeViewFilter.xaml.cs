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
using Database;
using Database.Objects;
using DIPS.Database.Objects;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for TreeViewFilter.xaml
    /// </summary>
    public partial class TreeViewFilter : Window
    {
        public TreeViewFilter()
        {
            InitializeComponent();
        }

        private Filter _filter;

        public Filter Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        private TreeView _treeview;

        public TreeView TreeView
        {
            get { return _treeview; }
            set { _treeview = value; }
        }
        

        private void PrepareParameters()
        {
          Filter = new Filter();

            String patientID = txtPatientID.Text;
            Filter.AcquisitionDateFrom = dtpPickFrom.SelectedDate.Value;
            Filter.AcquisitionDateTo = dtpDateTo.SelectedDate.Value;

            if (radFemale.IsChecked == true)
            {
                Filter.Gender = 'F';
            } else if (radMale.IsChecked == true)
            {
                Filter.Gender = 'M';
            }
        }

        private void ValidateFields()
        {
            
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            PrepareParameters();
            List<ImageDataset> dataset = new List<ImageDataset>();
            setupTreeview(ImageRepository.generateCustomTreeView(Filter));

            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void setupTreeview(List<ImageDataset> allDatasets)
        {
            if (allDatasets != null)
            {
                foreach (ImageDataset ds in allDatasets)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = ds.name;
                    item.ItemsSource = ds.relatedImages;

                    TreeView.Items.Add(item);
                }
            }

        }
    }
}
