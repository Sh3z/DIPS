using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents the base class of all plugins within the algorithm system. This class
    /// is abstract.
    /// </summary>
    public abstract class AlgorithmPlugin
    {
        /// <summary>
        /// Gets or sets the input <see cref="Bitmap"/> to this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        public Bitmap Input
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the output of this <see cref="AlgorithmPlugin"/> generated after executing
        /// the Run method on this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        public Bitmap Output
        {
            get;
            protected set;
        }

        /// <summary>
        /// Executes the algorithm using the provided input.
        /// </summary>
        /// <param name="args">The set of input arguments when executing this plugin.</param>
        /// <exception cref="AlgorithmException">an internal exception has occured. This
        /// is accessed through the inner exception property.</exception>
        public abstract void Run( RunArgs args );

        /// <summary>
        /// Manufactures a collection of <see cref="Property"/> objects representing the
        /// parameters that can be modified within this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Property"/> objects representing the parameters
        /// that can be modified.</returns>
        public abstract ICollection<Property> CreatePropertyDefinitions();
    }
}
