using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin.Matlab.Parameters
{
    /// <summary>
    /// Represents a generic-value parameter.
    /// </summary>
    public class GenericParameter : MatlabParameter
    {
        /// <summary>
        /// Specifies the underlying type of this <see cref="MatlabParameter."/>
        /// </summary>
        [Browsable( false )]
        public override ParameterType Type
        {
            get
            {
                return ParameterType.Object;
            }
        }


        /// <summary>
        /// Initializes and returns the value implementation for this
        /// particular type of <see cref="MatlabParameter"/>. The subclass
        /// should retain a reference to this value.
        /// </summary>
        /// <returns>An <see cref="IParametrValue"/> appropriate for the
        /// <see cref="MatlabParameter"/> subclass.</returns>
        protected override IParameterValue CreateValue()
        {
            _value = new ObjectValue();
            return _value;
        }

        /// <summary>
        /// Creates an <see cref="XElement"/> describing the contents of
        /// </summary>
        /// <returns>An <see cref="XElement"/> detailing the value of this
        /// <see cref="MatlabParameter"/>.</returns>
        protected override XElement CreateValueXml()
        {
            byte[] objBytes = new byte[0];
            if( _value.Value != null )
            {
                objBytes = _trySerializeValue( _value.Value );
            }

            return new XElement( "value",
                new XCData( System.Text.Encoding.Default.GetString( objBytes ) ) );
        }

        /// <summary>
        /// Restores this <see cref="MatlabParameter"/>'s value from the
        /// previously generated Xml.
        /// </summary>
        /// <param name="xml">The <see cref="XElement"/> containing the
        /// previously persisted information.</param>
        protected override void RestoreValue( XElement xml )
        {
            if( xml.FirstNode != null && xml.FirstNode.NodeType == System.Xml.XmlNodeType.CDATA )
            {
                XCData node = (XCData)xml.FirstNode;
                BinaryFormatter f = new BinaryFormatter();
                byte[] cdata = System.Text.Encoding.Default.GetBytes( node.Value );
                using( MemoryStream s = new MemoryStream( cdata ) )
                {
                    _value.Value = f.Deserialize( s );
                }
            }
        }


        /// <summary>
        /// Attempts to serialize the incoming object
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <returns>The serialized form of the value</returns>
        private byte[] _trySerializeValue( object value )
        {
            try
            {
                BinaryFormatter f = new BinaryFormatter();
                using( MemoryStream s = new MemoryStream() )
                {
                    f.Serialize( s, value );
                    return s.ToArray();
                }
            }
            catch( SerializationException e )
            {
                // The object we were provided with cannot be serialized
                throw new PluginException( "Cannot serialize GenericParameter value", e );
            }
        }


        /// <summary>
        /// Contains the inner object value.
        /// </summary>
        private ObjectValue _value;
    }
}
