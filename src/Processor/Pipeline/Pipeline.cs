using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Pipeline
{
    /// <summary>
    /// Represents a processing pipeline for use against a set of inputs.
    /// </summary>
    public class Pipeline : Collection<PipelineEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pipeline"/> class.
        /// </summary>
        public Pipeline()
        {
        }


        /// <summary>
        /// Adds a new entry to this <see cref="Pipeline"/>.
        /// </summary>
        /// <param name="item">The <see cref="PipelineEntry"/> to add
        /// to the end of this <see cref="Pipeline"/>.</param>
        new public void Add( PipelineEntry item )
        {
            if( item != null )
            {
                base.Add( item );
            }
        }
    }
}
