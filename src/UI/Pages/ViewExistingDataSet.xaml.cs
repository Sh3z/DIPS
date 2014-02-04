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
using System.Windows.Forms;
using DIPS.UI.Controls;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for ViewExistingDataSet.xaml
    /// </summary>
    public partial class ViewExistingDataSet : Page
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

        private List<ImageDataset> _allDatasets;

        public List<ImageDataset> allDatasets
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
        /// <param name="vm">The presentation layer-level view-model.</param>
        public ViewExistingDataSet(IFemoreViewModel vm)
        {
            InitializeComponent();

            // Set the Data Context as the view-model.
            DataContext = vm;
            addObjectsTotreeView();
            setupProperties();
        }

        /// <summary>
        /// Contains the reference to the view-model.
        /// </summary>
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IFemoreViewModel _viewModel;

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
            if (allDatasets != null)
            {
                foreach (ImageDataset ds in allDatasets)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = ds.name;
                    item.ItemsSource = ds.relatedImages;

                    treeDatasets.Items.Add(item);
                }
            }
            
        }

        private void setupTreeviewObjects()
        {
            allDatasets = ImageRepository.generateTreeView();
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
            SqlConnection con = new SqlConnection(staticVariables.sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("spr_RetrieveImage_v001", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = fileID;
            byte[] image = (byte[])cmd.ExecuteScalar();

            BitmapImage theBmp = ToImage(image);
                        
            unProcessedImg.Source = theBmp;
            con.Close();
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

                    populateImageDescription(ImageRepository.retrieveImageProperties(imgName));
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
            if (FilterSettings == null)
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
