using DIPS.Processor.Plugin.Matlab.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Provides simple creation of particular <see cref="MatlabParameter"/>
    /// types.
    /// </summary>
    public static class MatlabParameterFactory
    {
        /// <summary>
        /// Static initializer.
        /// </summary>
        static MatlabParameterFactory()
        {
            _paramTypes = new Dictionary<string, Type>();
            _paramTypes.Add( "object", typeof( GenericParameter ) );
            _paramTypes.Add( "file", typeof( FileParameter ) );
        }


        /// <summary>
        /// Initializes and returns a <see cref="MatlabParameter"/> corresponding
        /// to the provided Xml identifier.
        /// </summary>
        /// <param name="type">The value of the "type" tag in the Xml.
        /// <returns>A <see cref="MatlabParameter"/> corresponding to the
        /// xmlName value.</returns>
        public static MatlabParameter Manufacture( string xmlName )
        {
            if( xmlName == null )
            {
                return null;
            }

            Type t = null;
            _paramTypes.TryGetValue( xmlName.ToLower(), out t );
            if( t == null )
            {
                return new GenericParameter();
            }
            else
            {
                return Activator.CreateInstance( t ) as MatlabParameter;
            }
        }


        /// <summary>
        /// Contains the xml -> type pairings
        /// </summary>
        private static IDictionary<string, Type> _paramTypes;
    }
}
