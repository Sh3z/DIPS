using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents the source of possible items for choosing the target
    /// workspace for a <see cref="MatlabParameter"/>.
    /// </summary>
    public class WorkspaceItemsSource : IItemsSource
    {
        /// <summary>
        /// Gets the identifier for the base workspace.
        /// </summary>
        public static string Base
        {
            get
            {
                return "Base";
            }
        }

        /// <summary>
        /// Gets the identifier for the global workspace.
        /// </summary>
        public static string Global
        {
            get
            {
                return "Global";
            }
        }


        /// <summary>
        /// Creates the possible workspaces up for selection.
        /// </summary>
        /// <returns>The possible workspaces up for selection.</returns>
        public ItemCollection GetValues()
        {
            ItemCollection c = new ItemCollection();
            c.Add( Base );
            c.Add( Global );
            return c;
        }
    }
}
