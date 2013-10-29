using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace FemoreGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initialize the view-model and require the user to pick an image.
            PrototypeViewModel vm = new PrototypeViewModel();
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
    }
}
