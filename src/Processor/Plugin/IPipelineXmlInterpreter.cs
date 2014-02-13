using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents the object used to translate parameter objects of
    /// algorithms to and from Xml.
    /// </summary>
    public interface IPipelineXmlInterpreter
    {
        /// <summary>
        /// Converts the parameter object provided by the
        /// <see cref="AlgorithmDefinition"/> containing the properties
        /// to persist.
        /// </summary>
        /// <param name="parameterObject">The value of the algorithms
        /// parameter object</param>
        /// <returns>The <see cref="XElement"/> describing the properties
        /// within the object.</returns>
        XElement CreateXml( object parameterObject );

        /// <summary>
        /// Converts the provided Xml back into the appropriate parameter
        /// object for the algorithm
        /// </summary>
        /// <param name="parameterXml">The <see cref="XElement"/> describing the properties
        /// within the object.</param>
        /// <returns>The appropriate object used to describe the parameters
        /// for the process.</returns>
        object CreateObject( XElement parameterXml );
    }
}
