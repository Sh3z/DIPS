using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    [Algorithm( "gamma", ParameterObjectType = typeof( GammaProperties ) )]
    [AlgorithmMetadata(
        "Gamma Correction",
        Description = "Corrects Gamma level of input using provided target Gamma." )]
    public class GammaCorrection : AlgorithmPlugin
    {
        [AlgorithmProperty( "gamma", 3d )]
        public double Gamma
        {
            get;
            set;
        }

        public override void Run( object parameterObject )
        {

        }
    }
}
