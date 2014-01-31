using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Compression
{
    /// <summary>
    /// Provides manufacturing of <see cref="ICompressor"/>s given by their
    /// designation within Xml.
    /// </summary>
    public static class CompressorFactory
    {
        /// <summary>
        /// Static initializer.
        /// </summary>
        static CompressorFactory()
        {
            _stringComparer = new CaseInsensitiveComparator();
            _compressors = new Dictionary<string, Type>( _stringComparer );
            foreach( Type type in Assembly.GetExecutingAssembly().GetTypes() )
            {
                _analyzeType( type );
            }
        }


        /// <summary>
        /// Manufactures an <see cref="ICompressor"/> instance that is identifier
        /// by the provided string.
        /// </summary>
        /// <param name="identifier">The case-insensitive string representing
        /// the identifier of the compressor.</param>
        /// <returns>An <see cref="ICompressor"/> represented by the identifying
        /// string, or null if no compressor has been registered with the
        /// provided identifier.</returns>
        public static ICompressor ManufactureCompressor( string identifier )
        {
            if( string.IsNullOrEmpty( identifier ) )
            {
                throw new ArgumentException( "identifier" );
            }

            string matchingKey = _compressors.Keys.FirstOrDefault( x => _stringComparer.Equals( x, identifier ) );
            if( matchingKey == null )
            {
                return null;
            }
            else
            {
                Type type = _compressors[matchingKey];
                return Activator.CreateInstance( type ) as ICompressor;
            }
        }


        /// <summary>
        /// Determines whether the inbound type is a compatible compressor.
        /// </summary>
        /// <param name="type">The Type to analyze.</param>
        private static void _analyzeType( Type type )
        {
            if( type.GetInterfaces().Contains( typeof( ICompressor ) ) == false )
            {
                return;
            }

            CompressorAttribute attr = type.GetCustomAttributes().OfType<CompressorAttribute>().FirstOrDefault();
            if( attr == null )
            {
                return;
            }

            if( _compressors.ContainsKey( attr.Identifier ) )
            {
                Debug.WriteLine( "Duplicate key registration for compressor: " + attr.Identifier );
            }
            else
            {
                _compressors.Add( attr.Identifier, type );
            }
        }


        /// <summary>
        /// Retains the name -> type pairings of compressors.
        /// </summary>
        private static IDictionary<string, Type> _compressors;

        /// <summary>
        /// Contains a string comparer object for the factory to use.
        /// </summary>
        private static IEqualityComparer<string> _stringComparer;
    }
}
