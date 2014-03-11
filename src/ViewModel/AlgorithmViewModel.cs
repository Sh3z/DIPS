using DIPS.Processor.Client;
using DIPS.Util.Commanding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the view-model of an <see cref="AlgorithmDefinition"/>
    /// object.
    /// </summary>
    public class AlgorithmViewModel : ViewModel, ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmViewModel"/>
        /// class.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/> this
        /// view-model provides presentation logic for.</param>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        public AlgorithmViewModel( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                throw new ArgumentNullException( "definition" );
            }

            Definition = definition;
            ParameterObject = definition.ParameterObject;
            _removeCmd = new RelayCommand( _remove, _canRemove );
            IsRemovable = true;
        }


        /// <summary>
        /// Occurs when this <see cref="AlgorithmViewModel"/> has requested
        /// to be removed.
        /// </summary>
        public event EventHandler RemovalRequested;


        /// <summary>
        /// Gets the <see cref="AlgorithmDefinition"/> this
        /// <see cref="AlgorithmViewModel"/> is presenting.
        /// </summary>
        public AlgorithmDefinition Definition
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets a value indicating whether this <see cref="AlgorithmViewModel"/>
        /// has a parameter object instance for the provided <see cref="AlgorithmDefinition"/>.
        /// </summary>
        public bool HasParameterObject
        {
            get
            {
                return ParameterObject != null;
            }
        }

        /// <summary>
        /// Gets the <see cref="object"/> describing the parameters the underlying
        /// algorithm will use when executing.
        /// </summary>
        public ICloneable ParameterObject
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the represented
        /// algorithm is removable.
        /// </summary>
        public bool IsRemovable
        {
            get
            {
                return _isRemovable;
            }
            set
            {
                _isRemovable = value;
                OnPropertyChanged();
                _removeCmd.ExecutableStateChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isRemovable;

        /// <summary>
        /// Gets the <see cref="ICommand"/> used to remove this algorithm.
        /// </summary>
        public ICommand Remove
        {
            get
            {
                return _removeCmd;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _removeCmd;


        /// <summary>
        /// Returns a new object that is a copy of the current
        /// <see cref="AlgorithmViewModel"/>.
        /// </summary>
        /// <returns>A new <see cref="AlgorithmViewModel"/> with the
        /// same values as the current instance.</returns>
        public object Clone()
        {
            ICloneable paramsObj = null;
            if( this.ParameterObject != null )
            {
                paramsObj = (ICloneable)this.ParameterObject.Clone();
            }

            return new AlgorithmViewModel( Definition )
            {
                IsRemovable = this.IsRemovable,
                ParameterObject = paramsObj
            };
        }


        /// <summary>
        /// Performs the Remove.CanExecute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        private bool _canRemove( object parameter )
        {
            return IsRemovable;
        }

        /// <summary>
        /// Performs the Remove.Execute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        private void _remove( object parameter )
        {
            if( RemovalRequested != null )
            {
                RemovalRequested( this, EventArgs.Empty );
            }
        }
    }
}
