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

            List<TechShape> tech1 = new List<TechShape>();
            tech1 = setupParameters();

            
        }

        private List<TechShape> setupParameters()
        {
            List<TechShape> techList1 = new List<TechShape>();
            
            Technique tech1 = new Technique();
            tech1.Name = "Fuzzy1";
            tech1.ID = 1;

            Technique tech2 = new Technique();
            tech2.Name = "Fuzzy2";
            tech2.ID = 2;

            TechShape techShape1 = new TechShape();
            techShape1.Tech = tech1;
            TechShape techShape2 = new TechShape();
            techShape2.Tech = tech2;
            TechShape techShape3 = new TechShape();
            techShape3.Tech = tech2;
            TechShape techShape4 = new TechShape();
            techShape4.Tech = tech2;
            TechShape techShape5 = new TechShape();
            techShape5.Tech = tech2;

            techList1.Add(techShape1);
            techList1.Add(techShape2);

            stackListOfAvailableTech.Children.Add(techShape1);
            stackListOfAvailableTech.Children.Add(techShape2);
            stackListOfAvailableTech.Children.Add(techShape3);
            stackListOfAvailableTech.Children.Add(techShape4);
            stackListOfAvailableTech.Children.Add(techShape5);

            return techList1;
        }



        private void panel_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    // Get the panel that the element currently belongs to, 
                    // then remove it from that panel and add it the Children of 
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.KeyStates == DragDropKeyStates.ControlKey &&
                            e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            TechShape _circle = new TechShape();
                            _panel.Children.Add(_circle);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Copy;
                        }
                        else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's 
                // GiveFeedback event handler to determine which cursor to display. 
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }
    }

   

}
