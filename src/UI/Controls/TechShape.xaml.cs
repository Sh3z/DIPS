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

        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
            rect.ReleaseMouseCapture();
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);

            // center the rect on the mouse
            double left = mousePos.X - (rect.ActualWidth / 2);
            double top = mousePos.Y - (rect.ActualHeight / 2);
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
        }
    }
}
