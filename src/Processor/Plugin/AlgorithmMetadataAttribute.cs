using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents an optional attribute plugins can annotate themselves with
    /// the provide further information about themselves. This class cannot be
    /// inherited.
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false )]
    public sealed class AlgorithmMetadataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmMetadataAttribute"/>
        /// class.
        /// </summary>
        /// <param name="displayName">The friendlier display form of the
        /// algorithm.</param>
        public AlgorithmMetadataAttribute( string displayName )
        {
            DisplayName = displayName;
        }


        /// <summary>
        /// Gets the friendlier display name of the algorithm.
        /// </summary>
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a description of what function the algorithm will
        /// perform.
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
