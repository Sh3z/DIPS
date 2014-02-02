using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Client.JobDeployment
{
    public class XmlJobDefinition : IJobDefinition
    {
        public XmlJobDefinition( XDocument xml )
        {
            if( xml == null )
            {
                throw new ArgumentNullException( "xml" );
            }
        }

        public IEnumerable<AlgorithmDefinition> GetAlgorithms()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobInput> GetInputs()
        {
            throw new NotImplementedException();
        }
    }
}
