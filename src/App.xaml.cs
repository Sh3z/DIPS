using DIPS.Processor;
using DIPS.UI;
using DIPS.ViewModel;
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
            MainNavi navWindow = new MainNavi();
            navWindow.Service = s;
            navWindow.ShowDialog();
        }
    }
}
