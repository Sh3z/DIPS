using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents an output from the processing system.
    /// </summary>
    public interface IProcessedImage
    {
        /// <summary>
        /// Gets the <see cref="Image"/> generated from running a process
        /// against the input.
        /// </summary>
        Image Output
        {
            get;
        }

        /// <summary>
        /// Gets the identifier of the input provided to the processor to generate
        /// the associated output.
        /// </summary>
        string Identifier
        {
            get;
        }
    }
}
