using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Identifies <see cref="IJobResultsHandler"/> implementations to
    /// the factory. This class cannot be inherited.
    /// </summary>
    public sealed class HandlerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerAttribute"/>
        /// class.
        /// </summary>
        /// <param name="identifier">The identifier to give to the
        /// handler.</param>
        public HandlerAttribute( string identifier )
        {
            if( string.IsNullOrEmpty( identifier ) )
            {
                throw new ArgumentException( "identifier" );
            }

            Identifier = identifier;
        }


        /// <summary>
        /// Gets the identifie associated with the handler.
        /// </summary>
        public string Identifier
        {
            get;
            private set;
        }
    }
}
