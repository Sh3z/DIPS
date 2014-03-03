using DIPS.Processor.Plugin.Base.Smoothing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Represents the information required to process an image with the
    /// <see cref="Smooth"/> plugin.
    /// </summary>
    [DisplayName( "Smoothing Properties" )]
    public class SmoothProperties : ICloneable, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothProperties"/>
        /// class.
        /// </summary>
        public SmoothProperties()
        {
            SmoothMode = SmoothModeItemsSource.Gaussian;
        }


        /// <summary>
        /// Occurs when the value of a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Gets or sets the identifier of the smoothing mode.
        /// </summary>
        [Description( "Specifies the smoothing mode to use" )]
        [ItemsSource( typeof( SmoothModeItemsSource ) )]
        public string SmoothMode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                Smoother = SmootherFactory.Manufacture( _mode );
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _mode;

        /// <summary>
        /// Gets or sets the <see cref="ISmoother"/> to use when smoothing.
        /// </summary>
        [Description( "Encapsulates the unique properties of the selected smoother" )]
        [ExpandableObject]
        public ISmoother Smoother
        {
            get
            {
                return _smoother;
            }
            set
            {
                _smoother = value;
                if( PropertyChanged != null )
                {
                    PropertyChanged( this, new PropertyChangedEventArgs( "Smoother" ) );
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private ISmoother _smoother;


        /// <summary>
        /// Creates a copy of this <see cref="SmoothProperties"/> object.
        /// </summary>
        /// <returns>A new instance of the <see cref="SmoothProperties"/>
        /// class with the same values as the current instance.</returns>
        public object Clone()
        {
            SmoothProperties s = new SmoothProperties();
            s.SmoothMode = SmoothMode;
            s.Smoother = (ISmoother)Smoother.Clone();
            return s;
        }
    }
}
