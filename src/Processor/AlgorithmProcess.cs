using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    public class AlgorithmProcess : IEnumerable<AlgorithmPlugin>
    {
        public AlgorithmProcess( IOrderedEnumerable<AlgorithmPlugin> plugins )
        {
            if( plugins == null )
            {

            }
        }


        public IEnumerator<AlgorithmPlugin> GetEnumerator()
        {
            return _algorithms.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private IOrderedEnumerable<AlgorithmPlugin> _algorithms;
    }
}
