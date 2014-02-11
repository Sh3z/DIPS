using MLApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents an ongoing engine session.
    /// </summary>
    public class MatlabSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabSession"/>
        /// class.
        /// </summary>
        /// <param name="matlab">The <see cref="IMLApp"/> instance
        /// used in the current session.</param>
        /// <exception cref="ArgumentNullException">matlab is null.</exception>
        public MatlabSession( MLAppClass matlab )
        {
            if( matlab == null )
            {
                throw new ArgumentNullException( "matlab" );
            }

            Matlab = matlab;
            Valid = true;
        }


        /// <summary>
        /// Occurs when the value of the Valid property has changed.
        /// </summary>
        public event EventHandler SessionValidityChanged;


        /// <summary>
        /// Gets the <see cref="IMLApp"/> used in the current session.
        /// </summary>
        public MLAppClass Matlab
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current session is valid.
        /// </summary>
        public bool Valid
        {
            get
            {
                return _valid;
            }
            set
            {
                if( _valid != value )
                {
                    _valid = value;
                    _notifyValidityChanged();
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _valid;


        /// <summary>
        /// Provides guarding of session misuse by throwing an Exception if the current
        /// session is invalid.
        /// </summary>
        internal void ThrowIfInvalid()
        {
            if( Valid == false )
            {
                throw new MatlabException( "The current session is invalid." );
            }
        }


        /// <summary>
        /// Notifies any listeners of the change of the value of the Valid property.
        /// </summary>
        private void _notifyValidityChanged()
        {
            if( SessionValidityChanged != null )
            {
                SessionValidityChanged( this, EventArgs.Empty );
            }
        }
    }
}
