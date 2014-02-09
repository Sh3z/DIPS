using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the definition of an <see cref="AlgorithmPlugin"/> through
    /// metadata.
    /// </summary>
    public class AlgorithmDefinition : IEquatable<AlgorithmDefinition>
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
        /// Gets or sets the displayable form of this
        /// <see cref="AlgorithmDefinition"/>s name, provided by the plugin.
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the description of this
        /// <see cref="AlgorithmDefinition"/>, provided by the plugin.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the set of properties associated with the algorithm.
        /// </summary>
        public PropertySet Properties
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether this <see cref="AlgorithmDefinition"/> is equivilant to
        /// the provided <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="other">The <see cref="AlgorithmDefinition"/> to compare against.</param>
        /// <returns><c>true</c> if this <see cref="AlgorithmDefinition"/> is
        /// the same as the provided <see cref="AlgorithmDefinition"/>.</returns>
        public bool Equals( AlgorithmDefinition other )
        {
            if( other == null )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            if( other.AlgorithmName == AlgorithmName )
            {
                return true;
            }

            return other.Properties.Equals( Properties );
        }
    }
}
