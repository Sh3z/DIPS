using Femore.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Femore.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new <see cref="MainWindow"/> using the provided <see cref="IFemoreViewModel"/>
        /// as its presentation logic.
        /// </summary>
        /// <param name="vm">The presentation layer-level view-model.</param>
        public MainWindow( IFemoreViewModel vm )
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
                Close();
            }
        }


        /// <summary>
        /// Contains the reference to the view-model.
        /// </summary>
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IFemoreViewModel _viewModel;
    }
}
