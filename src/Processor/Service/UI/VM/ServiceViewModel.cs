using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.UI.Service.VM
{
    /// <summary>
    /// Represents the <see cref="ViewModel"/> providing presentation
    /// logic for the DIPS UI window.
    /// </summary>
    public class ServiceViewModel : ViewModel.ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceViewModel"/>
        /// class.
        /// </summary>
        /// <param name="service">The <see cref="IDIPS"/> service
        /// instance to provide presentation logic for.</param>
        /// <exception cref="ArgumentNullException">service is
        /// null.</exception>
        public ServiceViewModel( IDIPS service )
        {
            if( service == null )
            {
                throw new ArgumentNullException( "service" );
            }

            _service = service;
        }


        /// <summary>
        /// Gets or sets a value indicating whether the service is in
        /// interactive mode.
        /// </summary>
        public bool IsInInteractiveMode
        {
            get
            {
                return _isInInteractiveMode;
            }
            set
            {
                _isInInteractiveMode = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isInInteractiveMode;


        /// <summary>
        /// Contains the service instance this class provides presentation
        /// logic for.
        /// </summary>
        private IDIPS _service;
    }
}
