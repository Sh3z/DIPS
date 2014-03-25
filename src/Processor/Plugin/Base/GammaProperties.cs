using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Encapsulates the information required to process an image with the
    /// <see cref="GammaCorrection"/> plugin.
    /// </summary>
    [DisplayName( "Gamma Correction" )]
    public class GammaProperties : ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GammaProperties"/>
        /// class.
        /// </summary>
        public GammaProperties()
        {
            Gamma = 1;
        }


        /// <summary>
        /// Gets the default <see cref="GammaProperties"/> instance.
        /// </summary>
        [Browsable( false )]
        public static GammaProperties Default
        {
            get
            {
                return _default;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private static GammaProperties _default = new GammaProperties();

        /// <summary>
        /// Gets or sets the target gamma.
        /// </summary>
        [Category( "Properties" )]
        [Description( "Represents the target Gamma of the image" )]
        public double Gamma
        {
            get;
            set;
        }


        /// <summary>
        /// Creates a copy of this <see cref="GammaProperties"/> object.
        /// </summary>
        /// <returns>A new instance of the <see cref="GammaProperties"/>
        /// class with the same values as the current instance.</returns>
        public object Clone()
        {
            return new GammaProperties() { Gamma = this.Gamma };
        }
    }
}
