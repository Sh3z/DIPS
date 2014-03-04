using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Debug
{
    /// <summary>
    /// Provides information for the <see cref="Idle"/> plugin.
    /// </summary>
    public class IdleProperties : ICloneable
    {
        /// <summary>
        /// Gets or sets the number of seconds to sit idly.
        /// </summary>
        [Description( "The number of seconds to sit idly" )]
        public int Seconds
        {
            get;
            set;
        }


        /// <summary>
        /// Creates a copy of this <see cref="IdleProperties"/>.
        /// </summary>
        /// <returns>A copy of this <see cref="IdleProperties"/>.</returns>
        public object Clone()
        {
            return new IdleProperties()
            {
                Seconds = this.Seconds
            };
        }
    }
}
