using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Provides the two-way process of converting Matlab Properties into Xml.
    /// </summary>
    [PipelineXmlOriginator( typeof( MatlabProcess ) )]
    public class MatlabXmlInterpreter : IPipelineXmlInterpreter
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
        public XElement CreateXml( object parameterObject )
        {
            if( parameterObject == null )
            {
                return new XElement( "properties" );
            }

            MatlabProperties p = parameterObject as MatlabProperties;
            string path = p.ScriptFile;
            string contentsAsStr = System.Convert.ToBase64String( p.SerializedFile );
            ICollection<XElement> paramElements = new List<XElement>();
            foreach( MatlabParameter parameter in p.Parameters )
            {
                XElement paramXml = parameter.CreateXml();
                paramElements.Add( paramXml );
            }

            return new XElement( "properties",
                new XAttribute( "script-path", path ),
                new XElement("script-copy",
                    new XCData( contentsAsStr ) ),
                new XElement( "parameters", paramElements ) );
        }

        /// <summary>
        /// Converts the provided Xml back into the appropriate parameter
        /// object for the algorithm
        /// </summary>
        /// <param name="parameterXml">The <see cref="XElement"/> describing the properties
        /// within the object.</param>
        /// <returns>The appropriate object used to describe the parameters
        /// for the process.</returns>
        public object CreateObject( XElement parameterXml )
        {
            MatlabProperties p = new MatlabProperties();
            if( parameterXml == null )
            {
                return p;
            }

            XAttribute pathAttr = parameterXml.Attribute( "script-path" );
            if( pathAttr != null )
            {
                p.ScriptFile = pathAttr.Value;
            }

            XElement scriptCopyElement = parameterXml.Descendants( "script-copy" ).FirstOrDefault();
            if( scriptCopyElement != null
                && scriptCopyElement.FirstNode != null
                && scriptCopyElement.FirstNode.NodeType == System.Xml.XmlNodeType.CDATA )
            {
                string cdataStr = ( (XCData)scriptCopyElement.FirstNode ).Value;
                byte[] cdata = System.Convert.FromBase64String( cdataStr );
                p.SerializedFile = cdata;
            }

            foreach( var paramElement in parameterXml.Descendants( "parameter" ) )
            {
                XAttribute typeAttr = paramElement.Attribute( "type" );
                if( typeAttr == null )
                {
                    continue;
                }

                MatlabParameter param = MatlabParameterFactory.Manufacture( typeAttr.Value );
                param.Restore( paramElement );
                p.Parameters.Add( param );
            }

            return p;
        }
    }
}
