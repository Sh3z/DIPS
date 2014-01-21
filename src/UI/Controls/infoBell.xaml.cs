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

namespace DIPS.UI.Controls
{
    /// <summary>
    /// Interaction logic for infoBell.xaml
    /// </summary>
    public partial class infoBell : UserControl
    {
        public infoBell()
        {
            InitializeComponent();
        }

        private String _setText;

        public String setText
        {
            get { return _setText; }
            set { _setText = value; }
        }           

        private void imgBell_MouseLeave(object sender, MouseEventArgs e)
        {
            txtInfoText.Visibility = Visibility.Hidden;
        }

        private void imgBell1_MouseEnter(object sender, MouseEventArgs e)
        {
            this.txtInfoText.Visibility = Visibility.Visible;
        }

        private void txtInfoText_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtInfoText.Text = this.setText;
        }
      }
        
   }

