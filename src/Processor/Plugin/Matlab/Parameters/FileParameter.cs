using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin.Matlab.Parameters
{
    /// <summary>
    /// Represents a <see cref="MatlabParameter"/> that provides a script
    /// with a file.
    /// </summary>
    public class FileParameter : MatlabParameter
    {
        /// <summary>
        /// Specifies the underlying type of this <see cref="MatlabParameter."/>
        /// </summary>
        [Browsable( false )]
        public override ParameterType Type
        {
            get
            {
                return ParameterType.File;
            }
        }


        /// <summary>
        /// Initializes and returns the value implementation for this
        /// particular type of <see cref="MatlabParameter"/>.
        /// </summary>
        /// <returns>An <see cref="IParametrValue"/> appropriate for the
        /// <see cref="MatlabParameter"/> subclass.</returns>
        protected override IParameterValue CreateValue()
        {
            if( _value == null )
            {
                _value = new FileValue();
            }

            return _value;
        }

        /// <summary>
        /// Creates an <see cref="XElement"/> describing the contents of
        /// </summary>
        /// <returns>An <see cref="XElement"/> detailing the value of this
        /// <see cref="MatlabParameter"/>.</returns>
        protected override XElement CreateValueXml()
        {
            // We should keep the raw bytes of the file in case it's gone later.
            XCData file = null;
            if( _value.IsValid )
            {
                file = new XCData( System.Text.Encoding.Default.GetString( _value.Bytes ) );
            }

            return new XElement( "value",
                new XAttribute( "path", _value.Path ),
                file );
        }

        /// <summary>
        /// Restores this <see cref="MatlabParameter"/>'s value from the
        /// previously generated Xml.
        /// </summary>
        /// <param name="xml">The <see cref="XElement"/> containing the
        /// previously persisted information.</param>
        protected override void RestoreValue( XElement xml )
        {
            CreateValue();
            XAttribute path = xml.Attribute( "path" );
            if( path != null )
            {
                _value.Path = path.Value;
            }

            if( xml.FirstNode != null
                && xml.FirstNode.NodeType == System.Xml.XmlNodeType.CDATA )
            {
                string data = ( (XCData)xml.FirstNode ).Value;
                byte[] cdata = System.Text.Encoding.Default.GetBytes( data );
                _value.Bytes = cdata;
            }
        }


        /// <summary>
        /// Contains the underlying file object.
        /// </summary>
        private FileValue _value;
    }
}
