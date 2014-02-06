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

        private List<ImageDataset> _allDatasets;

        public List<ImageDataset> allDatasets
        {
            get { return _allDatasets; }
            set { _allDatasets = value; }
        }

        private CheckBox _chkFilterActive;

        public CheckBox chkFilterActive
        {
            get { return _chkFilterActive; }
            set { _chkFilterActive = value; }
        }
        

        private void PrepareParameters()
        {
          Filter = new Filter();

            Filter.PatientID = txtPatientID.Text;
            
            if (dtpDateTo.SelectedDate != null)
            {
                Filter.AcquisitionDateFrom = dtpPickFrom.SelectedDate.Value;
            }

            if (dtpDateTo.SelectedDate != null)
            {
                Filter.AcquisitionDateTo = dtpDateTo.SelectedDate.Value;
            }

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
            allDatasets = ImageRepository.generateCustomTreeView(Filter);
            setupTreeview();

            chkFilterActive.IsChecked = true;

            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void setupTreeview()
        {
            if (allDatasets != null)
            {
                TreeView.Items.Clear();

                foreach (ImageDataset ds in allDatasets)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = ds.name;
                    item.ItemsSource = ds.relatedImages;

                    TreeView.Items.Add(item);
                }
            }

        }

        private void windowFilter_Closed(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
