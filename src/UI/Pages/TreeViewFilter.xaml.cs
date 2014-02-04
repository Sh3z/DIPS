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

        private DateTime dateFrom;
        private DateTime dateTo;
        private String patientID;
        private Char sex;
        

        private void PrepareParameters()
        {
            dateFrom = new DateTime();
            dateTo = new DateTime();

            String patientID = txtPatientID.Text;
            dateFrom = dtpPickFrom.SelectedDate.Value;
            dateTo = dtpDateTo.SelectedDate.Value;

            if (radFemale.IsChecked == true)
            {
                sex = 'F';
            } else if (radMale.IsChecked == true)
            {
                sex = 'M';
            }
        }

        private void ValidateFields()
        {

        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            PrepareParameters();
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
