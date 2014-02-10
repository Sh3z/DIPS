using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the view-model of an <see cref="AlgorithmDefinition"/>
    /// object.
    /// </summary>
    public class AlgorithmViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmViewModel"/>
        /// class.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/> this
        /// view-model provides presentation logic for.</param>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        public AlgorithmViewModel( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                throw new ArgumentNullException( "definition" );
            }

            Definition = definition;
            ParameterObject = definition.ParameterObject;
        }


        /// <summary>
        /// Gets the <see cref="AlgorithmDefinition"/> this
        /// <see cref="AlgorithmViewModel"/> is presenting.
        /// </summary>
        public AlgorithmDefinition Definition
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets a value indicating whether this <see cref="AlgorithmViewModel"/>
        /// has a parameter object instance for the provided <see cref="AlgorithmDefinition"/>.
        /// </summary>
        public bool HasParameterObject
        {
            get
            {
                return ParameterObject != null;
            }
        }

        /// <summary>
        /// Gets the <see cref="object"/> describing the parameters the underlying
        /// algorithm will use when executing.
        /// </summary>
        public object ParameterObject
        {
            get;
            private set;
        }
    }
}
