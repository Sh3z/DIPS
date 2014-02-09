using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    [Algorithm( "gamma" )]
    public class GammaCorrection : AlgorithmPlugin
    {
        [AlgorithmProperty( "gamma", 3d )]
        public double Gamma
        {
            get;
            set;
        }

        public override void Run()
        {
            
        }
    }
}
