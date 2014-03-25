using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents a default <see cref="IDIPS"/> service ran locally.
    /// </summary>
    public class DefaultService : MarshalByRefObject, IDIPS
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultService"/>
        /// class.
        /// </summary>
        public DefaultService()
        {
            Processor = new ProcessingService();
        }


        /// <summary>
        /// Gets a value indicating whether the service has been initialized.
        /// </summary>
        public bool Initialized
        {
            get
            {
                lock( this )
                {
                    return _initialized;
                }
            }
            private set
            {
                lock( this )
                {
                    _initialized = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _initialized;

        /// <summary>
        /// Gets the <see cref="IProcessingService"/> retained by this service.
        /// </summary>
        public IProcessingService Processor
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets this <see cref="IDIPS"/> into a running state.
        /// </summary>
        public void Start()
        {
            lock( this )
            {
                if( Initialized )
                {
                    return;
                }

                Processor = new ProcessingService();
            }
        }

        /// <summary>
        /// Halts this <see cref="IDIPS"/> and shuts down the processor.
        /// </summary>
        public void Stop()
        {

        }
    }
}
