using Femore.UI;
using Femore.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Femore
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
            PrototypeViewModel vm = new PrototypeViewModel();
            MainWindow window = new MainWindow( vm );
            window.ShowDialog();
        }
    }
}
