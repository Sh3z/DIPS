using DIPS.UI.Pages;
using DIPS.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using DIPS.UI.Objects;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid;

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

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"Bitmaps|*.bmp|Jpgs|*.jpg";
            bool? result = dialog.ShowDialog();
            if( result.HasValue && result.Value )
            {
                Bitmap theBmp = new Bitmap( dialog.FileName );
                vm.ImageToProcess = theBmp;
            }
            else
            {
                System.Windows.MessageBox.Show("File not present");
            }

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
            foreach (ImageDataset ds in allDatasets)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = ds.name;
                item.ItemsSource = ds.relatedImages;

                treeDatasets.Items.Add(item);
            }
        }

        private void setupTreeviewObjects()
        {
            PatientImage img1 = new PatientImage();
            PatientImage img2 = new PatientImage();
            PatientImage img3 = new PatientImage();
            PatientImage img4 = new PatientImage();
            PatientImage img5 = new PatientImage();
            PatientImage img6 = new PatientImage();
            PatientImage img7 = new PatientImage();
            PatientImage img8 = new PatientImage();
            PatientImage img9 = new PatientImage();
            PatientImage img10 = new PatientImage();

            Patient patient1 = new Patient();
            Patient patient2 = new Patient();

            img1.imgID = 1;
            img2.imgID = 2;
            img3.imgID = 3;
            img4.imgID = 4;
            img5.imgID = 5;
            img6.imgID = 6;
            img7.imgID = 7;
            img8.imgID = 8;
            img9.imgID = 9;
            img10.imgID = 10;

            ObservableCollection<PatientImage> imageCollectionDS1 = new ObservableCollection<PatientImage>();
            ObservableCollection<PatientImage> imageCollectionDS2 = new ObservableCollection<PatientImage>();
            ObservableCollection<PatientImage> imageCollectionDS3 = new ObservableCollection<PatientImage>();
            ObservableCollection<PatientImage> imageCollectionDS4 = new ObservableCollection<PatientImage>();

            imageCollectionDS1.Add(img1);
            imageCollectionDS2.Add(img2);
            imageCollectionDS3.Add(img3);
            imageCollectionDS3.Add(img4);

            ImageDataset imgDS1 = new ImageDataset("August", imageCollectionDS1);
            ImageDataset imgDS2 = new ImageDataset("September", imageCollectionDS2);
            ImageDataset imgDS3 = new ImageDataset("October", imageCollectionDS3);
            ImageDataset imgDS4 = new ImageDataset("November", imageCollectionDS4);

            List<ImageDataset> allDatasetsActive = new List<ImageDataset>();

            allDatasetsActive.Add(imgDS1);
            allDatasetsActive.Add(imgDS2);
            allDatasetsActive.Add(imgDS3);
            allDatasetsActive.Add(imgDS4);

            //initialise all datasets var
            allDatasets = new List<ImageDataset>();
            allDatasets = allDatasetsActive;
        }
    }

      

}
