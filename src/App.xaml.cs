using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.UI;
using DIPS.Unity;
using DIPS.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

            // Create the window and provide it with the presentation layer.
            ProcessingService s = new ProcessingService();

            IUnityContainer c = GlobalContainer.Instance.Container;
            c.RegisterInstance<IProcessingService>( s );
            c.RegisterInstance<IPipelineManager>( s.PipelineManager );

            MainNavi navWindow = new MainNavi();
            navWindow.Service = s;
            navWindow.ShowDialog();
        }
    }
}
