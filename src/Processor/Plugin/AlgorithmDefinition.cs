using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents the definition of an <see cref="AlgorithmPlugin"/> through
    /// metadata.
    /// </summary>
    public class AlgorithmDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmDefinition"/>
        /// class.
        /// </summary>
        /// <param name="algorithmName">The name of the algorithm.</param>
        /// <param name="properties">The properties associated with the
        /// algorithm.</param>
        /// <exception cref="ArgumentException">algorithmName is null or
        /// empty.</exception>
        public AlgorithmDefinition( string algorithmName, IEnumerable<Property> properties )
        {
            if( string.IsNullOrEmpty( algorithmName ) )
            {
                throw new ArgumentException( "algorithmName" );
            }

            AlgorithmName = algorithmName;
            Properties = new PropertySet();

            if( properties != null )
            {
                foreach( var property in properties )
                {
                    Properties.Add( property );
                }
            }
        }


        /// <summary>
        /// Gets the unique name given to the algorithm.
        /// </summary>
        public string AlgorithmName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the set of properties associated with the algorithm.
        /// </summary>
        public PropertySet Properties
        {
            get;
            private set;
        }
    }
}
