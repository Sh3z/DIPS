using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    public class GammaProperties
    {
        [Category( "Properties" )]
        [Description( "Represents the target Gamma of the image" )]
        public double Gamma
        {
            get;
            set;
        }
    }
}
