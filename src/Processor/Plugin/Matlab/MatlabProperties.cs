﻿using DIPS.UI.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents the object providing the property information for the
    /// <see cref="MatlabProcess"/> plugin.
    /// </summary>
    [Serializable]
    [DisplayName( "Matlab Properties" )]
    public class MatlabProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabProperties"/>
        /// class.
        /// </summary>
        public MatlabProperties()
        {
            Parameters = new MatlabParametersCollection();
            _scriptFile = new MemoryFile();
        }


        /// <summary>
        /// Contains the path to the Matlab file to run.
        /// </summary>
        [Editor( typeof( FileEditor ), typeof( UITypeEditor ) )]
        public string ScriptFile
        {
            get
            {
                return _scriptFile.Path;
            }
            set
            {
                _scriptFile.Path = value;
                _scriptFile.Refresh();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private MemoryFile _scriptFile;

        /// <summary>
        /// Contains the serialized form of the file. This is non-browsable
        /// and is updated when the file is set.
        /// </summary>
        [Browsable( false )]
        public byte[] SerializedFile
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MatlabProperties"/>
        /// has a valid script to run.
        /// </summary>
        [Browsable( false )]
        public bool HasScript
        {
            get;
            private set;
        }

        /// <summary>
        /// Contains the collection of individual parameters for use by the
        /// script.
        /// </summary>
        [Description( "The individual parameters required by the script. " +
                        "If you forget to add a parameter, the algorithm will crash. " )]
        [Editor( typeof( MatlabParametersCollectionEditor ), typeof( UITypeEditor ) )]
        public MatlabParametersCollection Parameters
        {
            get;
            private set;
        }
    }
}
