using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Provides the ability to execute Matlab code as a processing step.
    /// </summary>
    public class MatlabProcess : AlgorithmPlugin
    {
        /// <summary>
        /// Contains the serialized Mathlab file, as bytes.
        /// </summary>
        [AlgorithmProperty( "matlab-file", "", PublicType = typeof( string ), PublicTypeConverter = typeof( FileToBytesConverter ) )]
        public byte[] SerializedMatlabFile
        {
            get;
            set;
        }

        /// <summary>
        /// Executes the algorithm represented by this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        /// <exception cref="AlgorithmException">an internal exception has occured. This
        /// is accessed through the inner exception property.</exception>
        public override void Run()
        {

        }
    }
}
