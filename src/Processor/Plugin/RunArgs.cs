using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Provides information to the plugin for execution. This class cannot be
    /// inherited.
    /// </summary>
    public sealed class RunArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunArgs"/> class.
        /// </summary>
        /// <param name="properties">The <see cref="IPropertySet"/> applicable to
        /// the plugin.</param>
        public RunArgs( IPropertySet properties )
        {
            if( properties == null )
            {
                throw new ArgumentNullException( "properties" );
            }

            Properties = properties;
        }


        /// <summary>
        /// Gets the <see cref="IPropertySet"/> detailing any parameters for use by
        /// the plugin.
        /// </summary>
        public IPropertySet Properties
        {
            get;
            private set;
        }
    }
}
