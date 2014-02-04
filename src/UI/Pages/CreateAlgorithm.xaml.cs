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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for CreateAlgorithm.xaml
    /// </summary>
    public partial class CreateAlgorithm : Page
    {
        public CreateAlgorithm()
        {
            InitializeComponent();
        }

        private void rect_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            DataObject dataObj = new DataObject(r.Fill);
            DragDrop.DoDragDrop(r, dataObj, DragDropEffects.Move);
        }

        private void rectTarget_Drop(object sender, DragEventArgs e)
        {
            SolidColorBrush scb = (SolidColorBrush)e.Data.GetData(typeof(SolidColorBrush));
            rectTarget.Fill = scb;
        }
    }

   

}
