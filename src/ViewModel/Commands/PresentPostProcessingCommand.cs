using DIPS.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DIPS.ViewModel.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the command used to specify post-processing settings.
    /// </summary>
    public class PresentPostProcessingCommand : UnityCommand
    {
        /// <summary>
        /// Occurs when the ChosenHandler property has changed.
        /// </summary>
        public event EventHandler ChosenHandlerModified;

        /// <summary>
        /// Gets the chosen <see cref="IJobResultsHandler"/> to use as
        /// the post-processing action.
        /// </summary>
        public IJobResultsHandler ChosenHandler
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
                    Container.Contains<IHandlerFactory>() &&
                    Container.Contains<IPostProcessingResolver>();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            IPostProcessingResolver resolver = Container.Resolve<IPostProcessingResolver>();
            resolver.Handlers = Container.Resolve<IHandlerFactory>();
            if( resolver.Resolve() )
            {
                ChosenHandler = resolver.ChosenHandler;
                if( ChosenHandlerModified != null )
                {
                    ChosenHandlerModified( this, EventArgs.Empty );
                }
            }
        }
    }
}
