using DIPS.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Reflection;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the command used to load an assembly containing
    /// custom-defined results handlers.
    /// </summary>
    public class LoadHandlersAssemblyCommand : UnityCommand
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="LoadHandlersAssemblyCommand"/> class.
        /// </summary>
        /// <param name="factory">The <see cref="IHandlerFactory"/> this
        /// command will load assemblies into.</param>
        /// <exception cref="ArgumentNullException">factory is null</exception>
        public LoadHandlersAssemblyCommand( IHandlerFactory factory )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            Factory = factory;
        }


        /// <summary>
        /// Gets the <see cref="IHandlerFactory"/> this command is
        /// applicable to.
        /// </summary>
        public IHandlerFactory Factory
        {
            get;
            private set;
        }


        /// <summary>
        /// Defines the method that determines whether the command can
        /// execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the
        /// command does not require data to be passed, this object can be
        /// set to null.</param>
        /// <returns>true if this command can be executed; otherwise,
        /// false.</returns>
        public override bool CanExecute( object parameter )
        {
            return  Container != null &&
                    Container.Contains<IFilePickerService>();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            IFilePickerService service = Container.Resolve<IFilePickerService>();
            service.Mode = FilePickerMode.Open;
            service.Filter = "Assemblies (*.dll)|*.dll";
            if( service.SelectPath() )
            {
                _tryLoadHandlersInAssembly( service.Path );
            }
        }


        /// <summary>
        /// Attempts to load the assembly given by the path into the
        /// handlers factory
        /// </summary>
        /// <param name="pathToAssembly">The path to the assembly</param>
        private void _tryLoadHandlersInAssembly( string pathToAssembly )
        {
            try
            {
                Assembly a = Assembly.LoadFile( pathToAssembly );
                Factory.Load( a );
            }
            catch { }
        }
    }
}
