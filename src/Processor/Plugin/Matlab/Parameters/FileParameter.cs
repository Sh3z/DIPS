using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab.Parameters
{
    /// <summary>
    /// Represents a <see cref="MatlabParameter"/> that provides a script
    /// with a file.
    /// </summary>
    public class FileParameter : MatlabParameter
    {
        /// <summary>
        /// Initializes and returns the value implementation for this
        /// particular type of <see cref="MatlabParameter"/>.
        /// </summary>
        /// <returns>An <see cref="IParametrValue"/> appropriate for the
        /// <see cref="MatlabParameter"/> subclass.</returns>
        protected override IParameterValue CreateValue()
        {
            return new FileValue();
        }
    }
}
