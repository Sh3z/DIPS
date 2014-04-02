using Database.Connection;
using Database.Unity;
using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.UI;
using DIPS.UI.Unity.Implementations;
using DIPS.Unity;
using DIPS.Unity.Implementations;
using DIPS.ViewModel;
using DIPS.ViewModel.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace DIPS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Occurs when the application has started.
        /// </summary>
        /// <param name="e">Event information.</param>
        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            ValidateConnection.validateConnection();

            // Create the window and provide it with the presentation layer.
            IDIPS service = ServiceHelper.CreateLocalService();
            IProcessingService s = service.Processor;

            IUnityContainer c = GlobalContainer.Instance.Container;
            c.RegisterInstance<IDIPS>( service );
            c.RegisterInstance<IProcessingService>( s );
            c.RegisterInstance<IPipelineManager>( s.PipelineManager );

            FilterTreeView ftv = new FilterTreeView();
            QueueDialog qd = new QueueDialog();
            UIContext context = new UIContext();
            HandlerFactory f = new HandlerFactory();
            ImageViewer iv = new ImageViewer();
            f.Load( Assembly.GetAssembly( typeof( HandlerFactory ) ) );

            c.RegisterInstance<IHandlerFactory>( f );
            c.RegisterInstance<IUIContext>( context );
            c.RegisterInstance<IFilterTreeView>(ftv);
            c.RegisterInstance<IQueueDialog>(qd);
            c.RegisterInstance<IImageView>(iv);

            MainNavi navWindow = new MainNavi();
            try
            {
                navWindow.ShowDialog();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
