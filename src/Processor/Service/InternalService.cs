using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Service
{
    /// <summary>
    /// Represents the real DIPS processing service. This can be used by various
    /// implementations of the running service.
    /// </summary>
    [Serializable]
    internal class InternalService : MarshalByRefObject, IDIPS
    {
        /// <summary>
        /// Singleton constructor.
        /// </summary>
        static InternalService()
        {
            _service = new InternalService();
        }

        /// <summary>
        /// Singleton instance accessor.
        /// </summary>
        public static InternalService Service
        {
            get
            {
                return _service;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private static InternalService _service;

        /// <summary>
        /// Gets a value indicating whether the service has been initialized.
        /// </summary>
        public bool Initialized
        {
            get
            {
                lock( _service )
                {
                    return _initialized;
                }
            }
            private set
            {
                lock( _service )
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
            lock( _service )
            {
                if( _service.Initialized )
                {
                    return;
                }

                _service.Processor = new ProcessingService();
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
