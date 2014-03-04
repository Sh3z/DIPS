using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the presentation logic of a single input.
    /// </summary>
    public class InputViewModel : ViewModel
    {
        public InputViewModel( JobInput input )
        {
            if( input == null )
            {
                throw new ArgumentNullException( "input" );
            }

            Input = input.Input;
            Identifier = input.Identifier;
        }

        public Image Input
        {
            get;
            private set;
        }

        public Image Output
        {
            get
            {
                return _output;
            }
            set
            {
                if( value != null )
                {
                    _output = (Image)value.Clone();
                    OnPropertyChanged();
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private Image _output;

        public object Identifier
        {
            get;
            private set;
        }

        public bool IsProcessed
        {
            get
            {
                return _isProcessed;
            }
            set
            {
                _isProcessed = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isProcessed;
    }
}
