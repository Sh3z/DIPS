using DIPS.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DIPS.UI.Controls
{
    /// <summary>
    /// Interaction logic for TechShape.xaml
    /// </summary>
    public class TechShape : Control
    {
        public Technique Tech
        {
            get;
            set;
        }


        static TechShape()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TechShape), new FrameworkPropertyMetadata(typeof(TechShape)));

            TechniqueNameProperty = DependencyProperty.Register("TechniqueName", typeof(string), typeof(TechShape), new PropertyMetadata(string.Empty));
        }


        public TechShape()
        {
            if (this.Tech == null)
            {
                Tech = new Technique();
            }
        }


        public static readonly DependencyProperty TechniqueNameProperty;

        public string TechniqueName
        {
            get
            {
                return GetValue(TechniqueNameProperty) as string;
            }
            set
            {
                SetValue(TechniqueNameProperty, value);
            }
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DataObject data = new DataObject();
            data.SetData("Object", this);
            DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
        }
    }
}
