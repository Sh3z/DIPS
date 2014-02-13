using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents an error in trying to use a <see cref="MatlabEngine"/>
    /// or any of its components after the session has been terminated.
    /// </summary>
    public class InvalidSessionException : MatlabException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSessionException"/>
        /// class.
        /// </summary>
        public InvalidSessionException()
            : base( "The Matlab session has expired." )
        {
        }
    }
}
