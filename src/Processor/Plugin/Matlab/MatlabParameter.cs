using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents a single parameter used for executing a Matlab script.
    /// </summary>
    [Serializable]
    [DisplayName( "Matlab Parameter" )]
    public class MatlabParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabParameter"/> class.
        /// </summary>
        public MatlabParameter()
        {
            Name = string.Empty;
            Workspace = "Base";
        }


        /// <summary>
        /// Gets or sets the identifier of the property for use by the Matlab
        /// script.
        /// </summary>
        [Description( "The identifier of this parameter used by the script" )]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the workspace to set the property.
        /// </summary>
        [Description( "The assigned workspace of this parameter" )]
        public string Workspace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the actual data.
        /// </summary>
        [Description( "The value given to this parameter" )]
        public object Data
        {
            get;
            set;
        }
    }
}
