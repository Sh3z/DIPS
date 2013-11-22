using DIPS.UI.Pages;
using DIPS.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

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
                MessageBox.Show("File not present");
            }
        }


        /// <summary>
        /// Contains the reference to the view-model.
        /// </summary>
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IFemoreViewModel _viewModel;
    }
}
