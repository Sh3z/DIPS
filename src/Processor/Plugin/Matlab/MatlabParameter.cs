using DIPS.Processor.Plugin.Matlab.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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
        [ItemsSource( typeof( WorkspaceItemsSource ) )]
        public string Workspace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IParameterValue"/> containing the details
        /// of this <see cref="MatlabParameter."/>
        /// </summary>
        [ExpandableObject]
        public IParameterValue Value
        {
            get
            {
                if( _value == null )
                {
                    _value = CreateValue();
                }

                return _value;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IParameterValue _value;


        /// <summary>
        /// Initializes and returns the value implementation for this
        /// particular type of <see cref="MatlabParameter"/>.
        /// </summary>
        /// <returns>An <see cref="IParametrValue"/> appropriate for the
        /// <see cref="MatlabParameter"/> subclass.</returns>
        protected virtual IParameterValue CreateValue()
        {
            return new ObjectValue();
        }
    }
}
