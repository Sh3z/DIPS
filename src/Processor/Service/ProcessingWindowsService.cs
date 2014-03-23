using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Service
{
    /// <summary>
    /// Represents the Windows service version of the DIPS processor.
    /// </summary>
    public partial class ProcessingWindowsService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingWindowsService"/> class.
        /// </summary>
        public ProcessingWindowsService()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Occurs when the service has started.
        /// </summary>
        /// <param name="args">N/A</param>
        protected override void OnStart( string[] args )
        {
            InternalService.Service.Start();
        }

        /// <summary>
        /// Occurs when the service has been stopped.
        /// </summary>
        protected override void OnStop()
        {
            InternalService.Service.Stop();
        }
    }
}
