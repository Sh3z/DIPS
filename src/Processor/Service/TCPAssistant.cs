using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Service
{
    /// <summary>
    /// Aids in establishing the DIPS service to a TCP port.
    /// </summary>
    internal static class TCPAssistant
    {
        /// <summary>
        /// Gets the port the DIPS service will be registered within.
        /// </summary>
        public static int Port
        {
            get
            {
                return 4982;
            }
        }

        /// <summary>
        /// Registers the provided service to the DIPS port.
        /// </summary>
        /// <param name="service">The <see cref="InternalService"/> to register
        /// to a particular port.</param>
        public static void Register( InternalService service )
        {
            TcpChannel channel = new TcpChannel( Port );
            channel.StartListening( null );
            ChannelServices.RegisterChannel( channel, true );
            WellKnownServiceTypeEntry obj = new WellKnownServiceTypeEntry(
                typeof( InternalService ), "DIPS/Service", WellKnownObjectMode.Singleton );
        }
    }
}
