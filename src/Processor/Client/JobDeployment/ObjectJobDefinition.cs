using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.JobDeployment
{
    public class ObjectJobDefinition : IJobDefinition
    {
        public ObjectJobDefinition( IEnumerable<AlgorithmDefinition> algorithms, IEnumerable<JobInput> inputs )
        {
            if( algorithms == null )
            {
                throw new ArgumentNullException( "algorithms" );
            }

            if( inputs == null )
            {
                throw new ArgumentNullException( "inputs" );
            }

            _algorithms = algorithms;
            _inputs = inputs;
        }

        public IEnumerable<AlgorithmDefinition> GetAlgorithms()
        {
            return _algorithms;
        }

        public IEnumerable<JobInput> GetInputs()
        {
            return _inputs;
        }


        private IEnumerable<AlgorithmDefinition> _algorithms;

        private IEnumerable<JobInput> _inputs;
    }
}
