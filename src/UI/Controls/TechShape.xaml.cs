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

namespace DIPS.UI.Controls
{
    /// <summary>
    /// Interaction logic for TechShape.xaml
    /// </summary>
    public partial class TechShape : UserControl
    {
        private Technique _tech;

        public Technique Tech
        {
            get { return _tech; }
            set { _tech = value; }
        }

        private bool _isRectDragInProg;

        public TechShape()
        {
            InitializeComponent();
            
            if (this.Tech == null)
            {
                Tech = new Technique();
                this.Tech.Name = lblTechName.Content.ToString();
            }
        }

        private void rect_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            TechShape r = (TechShape)sender;
            DataObject data = new DataObject();
            data.SetData("Object", this);
            DragDrop.DoDragDrop(r, data, DragDropEffects.Move);
        }
    }
}
