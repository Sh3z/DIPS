using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    /// <summary>
    /// Represents the singleton providing the application-wide unity container.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class GlobalContainer : IDisposable
    {
        /// <summary>
        /// Static singleton initializer.
        /// </summary>
        static GlobalContainer()
        {
            _container = new GlobalContainer();
        }

        /// <summary>
        /// Private constructor (enforce singleton usage).
        /// </summary>
        private GlobalContainer()
        {
            Container = new UnityContainer();
            Assembly[] assemblies = new[] { Assembly.GetAssembly( typeof( GlobalContainer ) ) };
            Container.RegisterTypes(
                AllClasses.FromAssemblies( assemblies ),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled );
        }


        /// <summary>
        /// Gets the shared <see cref="GlobalContainer"/> singleton.
        /// </summary>
        public static GlobalContainer Instance
        {
            get
            {
                return _container;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private static GlobalContainer _container;

        /// <summary>
        /// Gets the global <see cref="IUnityContainer"/> shared across the
        /// application.
        /// </summary>
        public IUnityContainer Container
        {
            get;
            private set;
        }


        void IDisposable.Dispose()
        {
            Container.Dispose();
        }
    }
}
