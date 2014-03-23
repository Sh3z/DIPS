using DIPS.UI.Service;
using DIPS.UI.Service.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIPS.Processor.Service
{
    /// <summary>
    /// Entry point to the DIPS processor.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            TCPAssistant.Register( InternalService.Service );
            _launch();
        }


        /// <summary>
        /// Launches the DIPS processor using the provided command-line arguments
        /// </summary>
        private static void _launch()
        {
            string[] args = Environment.GetCommandLineArgs();
            if( args.Contains( "/interactive" ) )
            {
                if( Environment.UserInteractive )
                {
                    // Launch the processor with the GUI
                    ServiceDialog dialog = new ServiceDialog();
                    ServiceViewModel vm = new ServiceViewModel( InternalService.Service );
                    dialog.DataContext = vm;
                    dialog.ShowDialog();
                }
            }
            else
            {
                _launchWindowsService();
            }
        }

        /// <summary>
        /// Launches the DIPS processor as a Windows service.
        /// </summary>
        /// <returns>true if the service is deployed</returns>
        private static void _launchWindowsService()
        {
            try
            {
                if( Environment.UserInteractive == false )
                {
                    _runWindowsService();
                }
            }
            catch
            {
                // Todo logging
            }
        }

        /// <summary>
        /// Launches the service through ServiceBase
        /// </summary>
        private static void _runWindowsService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ProcessingWindowsService() 
            };

            ServiceBase.Run( ServicesToRun );
        }
    }
}
