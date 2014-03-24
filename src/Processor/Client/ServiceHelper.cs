using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the assistant object used to locate DIPS service
    /// implementations.
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Creates and returns the <see cref="IDIPS"/> implementation
        /// used as default local services.
        /// </summary>
        /// <returns>The <see cref="IDIPS"/> service object used to
        /// run locally on the client computer.</returns>
        public static IDIPS CreateLocalService()
        {
            // Use the information in the registry to locate the default type.
            try
            {
                RegistryKey service = Registry.LocalMachine.OpenSubKey( @"SOFTWARE\Wow6432Node\DIPS\Service" );
                string typeAssembly = (string)service.GetValue( "DefaultTypeAssembly" );
                string typeName = (string)service.GetValue( "DefaultType" );
                Assembly assembly = Assembly.LoadFrom( typeAssembly );
                Type type = assembly.GetType( typeName );
                return (IDIPS)Activator.CreateInstance( type );
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves the <see cref="IDIPS"/> implementation on a remote
        /// computer.
        /// </summary>
        /// <param name="computerName">The name of the computer to locate
        /// the service.</param>
        /// <returns>he <see cref="IDIPS"/> service object running locally
        /// on the server, or null if no service is found.</returns>
        public static IDIPS LocateRemoteService( string computerName )
        {
            throw new NotImplementedException();
        }
    }
}
