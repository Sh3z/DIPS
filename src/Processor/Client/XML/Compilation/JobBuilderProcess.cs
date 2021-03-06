﻿using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Compilation
{
    /// <summary>
    /// Represents the builder process used to construct job definition
    /// documents.
    /// </summary>
    public class JobBuilderProcess : IBuilderProcess
    {
        /// <summary>
        /// Gets the name to give to the root node of the final Xml document.
        /// </summary>
        public string RootNodeName
        {
            get
            {
                return "job";
            }
        }

        /// <summary>
        /// Constructs Xml using the <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// detailing the process and its properties.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</returns>
        public XElement Build( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                return new XElement( "algorithm" );
            }

            const string algElementName = "algorithm";
            XAttribute nameAttr = new XAttribute( "name", definition.AlgorithmName );
            if( definition.Properties.Any() == false )
            {
                return new XElement( algElementName, nameAttr );
            }

            ICollection<XElement> properties = new List<XElement>();
            foreach( Property property in definition.Properties )
            {
                XAttribute name = new XAttribute( "name", property.Name );
                XAttribute type = new XAttribute( "type", property.Type );
                XElement value = new XElement( "value", property.Value );
                XElement propertyXml = new XElement( "property", name, type, value );
                properties.Add( propertyXml );
            }

            XElement propertiesXml = new XElement( "properties", properties );
            return new XElement( algElementName, nameAttr, propertiesXml );
        }

        /// <summary>
        /// Constructs Xml using the <see cref="JobInput"/>.
        /// </summary>
        /// <param name="input">The <see cref="JobInput"/> to be used as input
        /// in a job.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="JobInput"/>.</returns>
        /// <exception cref="InvalidOperationException">this
        /// <see cref="IBuilderProcess"/> does not support building
        /// inputs.</exception>
        public XElement BuildInput( JobInput input )
        {
            ICollection<object> content = new List<object>();
            if( input.Identifier != null )
            {
                XAttribute id = new XAttribute( "identifier", input.Identifier );
                content.Add( id );
            }

            byte[] imgBytes = null;
            if( input.Compressor != null )
            {
                string compressorId = CompressorFactory.ResolveIdentifier( input.Compressor );
                imgBytes = CompressionAssistant.Compress( input.Input, input.Compressor );
                XAttribute compressor = new XAttribute( "compressor", compressorId );
                content.Add( compressor );
            }
            else
            {
                imgBytes = CompressionAssistant.ImageToBytes( input.Input );
            }

            string bytesAsString = System.Text.Encoding.Default.GetString( imgBytes );
            XCData imgNode = new XCData( bytesAsString );
            content.Add( imgNode );

            return new XElement( "input", content );
        }
    }
}
