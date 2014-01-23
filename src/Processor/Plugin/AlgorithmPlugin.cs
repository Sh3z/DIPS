using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    public abstract class AlgorithmPlugin
    {
        public Bitmap Input
        {
            get;
            set;
        }

        public Bitmap Output
        {
            get;
            protected set;
        }

        public abstract void Run( RunArgs args );
    }
}
