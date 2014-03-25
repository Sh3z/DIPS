using DIPS.Processor.Plugin.Matlab.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents the possible <see cref="MatlabParameter"/> types.
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// A generic value container parameter
        /// </summary>
        Object,

        /// <summary>
        /// A file parameter container
        /// </summary>
        File
    }

    /// <summary>
    /// Represents a single parameter used for executing a Matlab script. This
    /// class is abstract.
    /// </summary>
    [Serializable]
    [DisplayName( "Matlab Parameter" )]
    public abstract class MatlabParameter
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
        /// Specifies the underlying type of this <see cref="MatlabParameter."/>
        /// </summary>
        [Browsable( false )]
        public abstract ParameterType Type
        {
            get;
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
        /// Creates the Xml form of this <see cref="MatlabParameter"/> for
        /// persistence.
        /// </summary>
        /// <returns>An <see cref="XElement"/> describing this parameter.</returns>
        public XElement CreateXml()
        {
            return new XElement( "parameter",
                new XAttribute( "name", Name ),
                new XAttribute( "workspace", Workspace ),
                new XAttribute( "type", Type ),
                CreateValueXml() );
        }

        /// <summary>
        /// Restores this <see cref="MatlabParameter"/> from the provided
        /// <see cref="XElement"/>.
        /// </summary>
        /// <param name="xml">The <see cref="XElement"/> detailing the values
        /// of the <see cref="MatlabParameter"/>.</param>
        public void Restore( XElement xml )
        {
            if( xml == null )
            {
                return;
            }

            XAttribute name = xml.Attribute( "name" );
            if( name != null )
            {
                Name = name.Value;
            }

            XAttribute workspace = xml.Attribute( "workspace" );
            if( workspace != null )
            {
                Workspace = workspace.Value;
            }

            XElement valElement = xml.Descendants( "value" ).FirstOrDefault();
            if( valElement != null )
            {
                RestoreValue( valElement );
            }
        }


        /// <summary>
        /// Initializes and returns the value implementation for this
        /// particular type of <see cref="MatlabParameter"/>. The subclass
        /// should retain a reference to this value.
        /// </summary>
        /// <returns>An <see cref="IParametrValue"/> appropriate for the
        /// <see cref="MatlabParameter"/> subclass.</returns>
        protected abstract IParameterValue CreateValue();

        /// <summary>
        /// Creates an <see cref="XElement"/> describing the contents of
        /// </summary>
        /// <returns>An <see cref="XElement"/> detailing the value of this
        /// <see cref="MatlabParameter"/>.</returns>
        protected abstract XElement CreateValueXml();

        /// <summary>
        /// Restores this <see cref="MatlabParameter"/>'s value from the
        /// previously generated Xml.
        /// </summary>
        /// <param name="xml">The <see cref="XElement"/> containing the
        /// previously persisted information.</param>
        protected abstract void RestoreValue( XElement xml );
    }
}
