using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        
        private void windowFilter_Closed(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
