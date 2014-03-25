using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Registry
{
    /// <summary>
    /// Represents an entity that maintains a set of plugins using the
    /// assemblies within the DIPS Registry.
    /// </summary>
    public interface IPluginRegistry
    {
        /// <summary>
        /// Initializes this <see cref="IPluginRegistry"/> from the loaded
        /// <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> specified within
        /// the Windows registry containing plugins.</param>
        void Initialize( Assembly assembly );
    }
}
