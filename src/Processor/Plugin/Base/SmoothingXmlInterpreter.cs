using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Represents the object used to convert the properties of the
    /// <see cref="Smooth"/> plugin to and from Xml
    /// </summary>
    [PipelineXmlOriginator( typeof( Smooth ) )]
    public class SmoothingXmlInterpreter : IPipelineXmlInterpreter
    {
        /// <summary>
        /// Converts the parameter object provided by the
        /// <see cref="AlgorithmDefinition"/> containing the properties
        /// to persist.
        /// </summary>
        /// <param name="parameterObject">The value of the algorithms
        /// parameter object</param>
        /// <returns>The <see cref="XElement"/> describing the properties
        /// within the object.</returns>
        public XElement CreateXml( ICloneable parameterObject )
        {
            if( parameterObject is SmoothProperties == false )
            {
                return new XElement( "properties" );
            }

            SmoothProperties s = parameterObject as SmoothProperties;

            // Save the state of the public properties in the smoother
            string smootherID = s.SmoothMode;
            XAttribute smootherAttr = new XAttribute( "smoother", smootherID );

            Type smootherType = s.Smoother.GetType();
            var propertyInfos = smootherType.GetProperties();
            var properties =    from property in propertyInfos
                                select new XElement( "property",
                                    new XAttribute( "name", property.Name ),
                                    _objtoBinString( property.GetValue( s.Smoother ) ) );

            return new XElement( "properties", smootherAttr, properties ); ;
        }

        /// <summary>
        /// Converts the provided Xml back into the appropriate parameter
        /// object for the algorithm
        /// </summary>
        /// <param name="parameterXml">The <see cref="XElement"/> describing the properties
        /// within the object.</param>
        /// <returns>The appropriate object used to describe the parameters
        /// for the process.</returns>
        public ICloneable CreateObject( XElement parameterXml )
        {
            if( parameterXml == null )
            {
                return new SmoothProperties();
            }

            SmoothProperties p = new SmoothProperties();
            XAttribute smootherAttr = parameterXml.Attribute( "smoother" );
            if( smootherAttr != null )
            {
                p.SmoothMode = smootherAttr.Value;
            }

            _populateSmoother( p.Smoother, parameterXml.Descendants( "property" ) );
            return p;
        }


        /// <summary>
        /// Converts an object into a binary-formatted string
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns>The binary-formatted form of the object</returns>
        private string _objtoBinString( object obj )
        {
            byte[] asBytes = _objToBytes( obj );
            return System.Convert.ToBase64String( asBytes );
        }

        /// <summary>
        /// Converts a previously binary-formatted string back into
        /// an object
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <returns>The original object form</returns>
        private object _stringToObj( string s )
        {
            byte[] bytes = System.Convert.FromBase64String( s );
            return _bytesToObj( bytes );
        }

        /// <summary>
        /// Converts an object into bytes
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns>The byte array form of the object</returns>
        private byte[] _objToBytes( object obj )
        {
            BinaryFormatter f = new BinaryFormatter();
            using( MemoryStream ms = new MemoryStream() )
            {
                f.Serialize( ms, obj );
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts bytes back into an object
        /// </summary>
        /// <param name="bytes">The bytes to convert</param>
        /// <returns>The original object</returns>
        private object _bytesToObj( byte[] bytes )
        {
            using( MemoryStream ms = new MemoryStream( bytes ) )
            {
                BinaryFormatter f = new BinaryFormatter();
                return f.Deserialize( ms );
            }
        }

        /// <summary>
        /// Repopulates the properties of a smoother from Xml
        /// </summary>
        /// <param name="smoother">The smoother to repopulate</param>
        /// <param name="properties">The values of the properties within
        /// the Xml</param>
        private void _populateSmoother( ISmoother smoother, IEnumerable<XElement> properties )
        {
            Type smootherType = smoother.GetType();
            var propertyInfos = smootherType.GetProperties();
            foreach( XElement element in properties )
            {
                XAttribute nameAttr = element.Attribute( "name" );
                if( nameAttr == null )
                {
                    continue;
                }

                PropertyInfo prop = propertyInfos.FirstOrDefault( x => x.Name == nameAttr.Value );
                if( prop == null )
                {
                    continue;
                }

                prop.SetValue( smoother, _stringToObj( element.Value ) );
            }
        }
    }
}
