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
using DIPS.Database.Objects;
using DIPS.UI.Controls;

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

            TechShape tech1 = new TechShape();
            tech1 = setupParameters();

            stackListOfAvailableTech.Children.Add(tech1);
        }

        private TechShape setupParameters()
        {
            Technique tech1 = new Technique();
            tech1.Name = "Fuzzy1";
            tech1.ID = 1;

            TechShape techShape1 = new TechShape();
            techShape1.Tech = tech1;

            return techShape1;
        }

       

        private void stackTarget_Drop(object sender, DragEventArgs e)
        {
            TechShape tech = (TechShape)e.Data.GetData(typeof(TechShape));
            stackListOfSelectedTech.Children.Add(tech);
        }
    }

   

}
