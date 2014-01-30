using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.XML
{
    /// <summary>
    /// Provides building functionality of DIPS Xml documents.
    /// </summary>
    public class XmlBuilder
    {
        public void Append( AlgorithmPlugin process )
        {
            try
            {
                AlgorithmDefinition definition = PluginReflector.CreateDefinition( process );

            }
            catch( Exception e )
            {
                throw new XmlBuilderException( "Error appending process. See inner exception.", e );
            }
        }


        private void _appendDefinition( AlgorithmDefinition definition )
        {

        }
    }
}
