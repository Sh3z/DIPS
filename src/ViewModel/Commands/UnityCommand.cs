using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents an <see cref="ICommand"/> which utilizes an
    /// <see cref="IUnityContainer"/>. This class is abstract.
    /// </summary>
    public abstract class UnityCommand : Command
    {
        /// <summary>
        /// Gets or sets the <see cref="IUnityContainer"/> used in the current
        /// application.
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                IUnityContainer old = _container;
                _container = value;
                OnContainerChanged( old, _container );
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IUnityContainer _container;


        /// <summary>
        /// Invokes whenever the value of the Container property has been
        /// changed.
        /// </summary>
        /// <param name="previous">The previous value of the property.</param>
        /// <param name="current">The new value of the property.</param>
        protected virtual void OnContainerChanged( IUnityContainer previous, IUnityContainer current )
        {
            OnCanExecuteChanged();
        }
    }
}
