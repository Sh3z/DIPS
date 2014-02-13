﻿using System;
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
    public class GammaProperties
    {
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
        private static GammaProperties _default = new GammaProperties() { Gamma = 1 };

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
    }
}
