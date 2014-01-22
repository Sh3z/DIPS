using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    public interface IRequestHandler
    {
        void Handle( IJobTicket job );
    }
}
