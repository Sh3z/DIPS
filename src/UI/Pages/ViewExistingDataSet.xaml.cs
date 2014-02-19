using DIPS.UI.Pages;
using DIPS.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using DIPS.Database.Objects;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Database;
using DIPS.Database;
using System.IO;
using System;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DIPS.UI.Controls;
using Database.DicomHelper;
using Database.Repository;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for ViewExistingDataSet.xaml
    /// </summary>
    public partial class ViewExistingDataSet : UserControl
    {       
        /// <summary>
        /// Initializes a new <see cref="ViewExistingDataset"/> using the provided <see cref="IFemoreViewModel"/>
        /// as its presentation logic.
        /// </summary>
        /// <param name="vm">The presentation layer-level view-model.</param>
        /// 
       //declarion of properties
        private MainPage windowNav;
        //getters and setters
        public MainPage WindowNav
        {
            get
            {
                return windowNav;
            }
            set
            {
                windowNav = value;
            }
        }

        private List<Patient> _allDatasets;

        public List<Patient> allDatasets
        {
            get { return _allDatasets; }
            set { _allDatasets = value; }
        }

        private ImageProperties _propList;

        public ImageProperties propList
        {
            get { return _propList; }
            set { _propList = value; }
        }

        private TreeViewFilter _filterSettings;

        public TreeViewFilter FilterSettings
        {
            get { return _filterSettings; }
            set { _filterSettings = value; }
        }
        
        
        
        /// <summary>
        /// Initializes a new <see cref="ViewExistingDataset"/> using the provided <see cref="IFemoreViewModel"/>
        /// as its presentation logic.
        /// </summary>
        public ViewExistingDataSet()
        {
            InitializeComponent();
            addObjectsTotreeView();
            setupProperties();
        }

        private void insertImageProperties()
        {
            
        }

        private void setupProperties()
        {
            propList = new ImageProperties();
            _propertyGrid.SelectedObject = propList;
        }

        private void addObjectsTotreeView()
        {
            setupTreeviewObjects();
            setupTreeview();   
        }

        private void setupTreeview()
        {
            treeDatasets.Items.Clear();

            if (allDatasets != null)
            {
                foreach (Patient patient in allDatasets)
                {
                    TreeViewItem mainTree = new TreeViewItem();
                    mainTree.Header = patient.patientID;

                    foreach (ImageDataset ds in patient.dataSet)
                    {
                        TreeViewItem subTree = new TreeViewItem();
                        mainTree.Items.Add(subTree);
                        subTree.Header = ds.series;
                        subTree.ItemsSource = ds.relatedImages;
                    }
                    treeDatasets.Items.Add(mainTree);
                }
            }
            
        }

        private void setupTreeviewObjects()
        {
            ImageRepository repo = new ImageRepository();
            allDatasets = repo.generateTreeView();
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var Text = e.NewValue.ToString();
                setImage(Text);
                //retrieveProperties(Text);
            }
            catch (Exception e2) { }
        }

        public void setImage(String fileID)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveImage_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = fileID;
                byte[] image = (byte[])cmd.ExecuteScalar();

                BitmapImage theBmp = ToImage(image);
                unProcessedImg.Source = theBmp;
            }
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void treeDatasets_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    System.Windows.Controls.TextBlock selectedItem = e.OriginalSource as System.Windows.Controls.TextBlock;
                    String imgName = selectedItem.Text;

                    setImage(imgName);
                    ImageRepository repo = new ImageRepository();
                    populateImageDescription(repo.retrieveImageProperties(imgName));
                }

                //retrieveProperties(Text);
            }
            catch (Exception e2) { }
        }

        private void populateImageDescription(List<String> description)
        {
            txtImageDesc.Text = String.Empty;
            foreach (string desc in description)
            {
                txtImageDesc.Text += desc;
                txtImageDesc.Text += Environment.NewLine;
            }
        }

        private void btnSelectFilter_Click(object sender, RoutedEventArgs e)
        {
            if (FilterSettings == null || FilterSettings.Activate() == false)
            {
                TreeViewFilter tvf = new TreeViewFilter();
                FilterSettings = tvf;
                tvf.TreeView = this.treeDatasets;
                tvf.chkFilterActive = this.chkActiveFilter;
                
                FilterSettings.ShowDialog();
            }
            else
            {
                FilterSettings.Visibility = Visibility.Visible;
            }
        }

        private void chkActiveFilter_Click(object sender, RoutedEventArgs e)
        {
            if (FilterSettings != null)
            {
                if (chkActiveFilter.IsChecked.Value)
                {
                    FilterSettings.setupTreeview();
                }
                else
                {
                    setupTreeview();
                }
            }
        }

    }

      

}
