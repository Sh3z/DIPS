using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace FemoreGUI
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        /// <summary>
        /// Dependency Property constructor.
        /// </summary>
        static ImageWindow()
        {
            ImageProperty = DependencyProperty.Register(
                "Image", typeof( ImageSource ), typeof( ImageWindow ) );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageWindow"/> class.
        /// </summary>
        public ImageWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        /// <summary>
        /// Identifies the Image Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ImageProperty;


        /// <summary>
        /// Gets or sets the <see cref="ImageSource"/> to be displayed.
        /// </summary>
        [Bindable( true )]
        public ImageSource Image
        {
            get
            {
                return GetValue( ImageProperty ) as ImageSource;
            }
            set
            {
                SetValue( ImageProperty, value );
            }
        }
    }
}
