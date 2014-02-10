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
        /// Gets or sets the input <see cref="Image"/> to this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        public Image Input
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the output of this <see cref="AlgorithmPlugin"/> generated after executing
        /// the Run method on this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        public Image Output
        {
            get;
            protected set;
        }

        /// <summary>
        /// Executes the algorithm represented by this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        /// <exception cref="AlgorithmException">an internal exception has occured. This
        /// is accessed through the inner exception property.</exception>
        public virtual void Run()
        {
            Run( null );
        }

        /// <summary>
        /// Executes the algorithm represented by this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        /// <param name="parameterObject">An object of the type provided by the
        /// <see cref="AlgorithmAttribute"/> describing the properties used by this
        /// <see cref="AlgorithmPlugin"/>.</param>
        /// <exception cref="AlgorithmException">an internal exception has occured. This
        /// is accessed through the inner exception property.</exception>
        public abstract void Run( object parameterObject );
    }
}
