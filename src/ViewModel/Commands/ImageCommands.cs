using System.Windows.Input;

namespace Femore.ViewModel.Commands
{
    /// <summary>
    /// Contains a set of <see cref="RoutedUICommand"/>s for use within the UI.
    /// </summary>
    public static class ImageCommands
    {
        /// <summary>
        /// Static property constructor.
        /// </summary>
        static ImageCommands()
        {
            OpenInWindow = new RoutedUICommand( "Open in Window", "OpenInWindow", typeof( ImageCommands ) );
        }


        /// <summary>
        /// Gets the command used to open images in new windows for detailed display.
        /// </summary>
        public static RoutedUICommand OpenInWindow
        {
            get;
            private set;
        }
    }
}
