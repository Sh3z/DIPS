using DIPS.Processor.Plugin.Matlab.Parameters;
using DIPS.UI.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    public class MatlabParametersCollectionEditor : CollectionEditor
    {
        static MatlabParametersCollectionEditor()
        {
            _types = new List<Type>()
            {
                typeof( MatlabParameter ),
                typeof( FileParameter )
            };
        }

        private static IList<Type> _types;

        public override IList<Type> NewItemTypes
        {
            get
            {
                return _types;
            }
        }
    }
}
