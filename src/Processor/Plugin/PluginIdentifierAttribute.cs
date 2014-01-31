﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents a plugin against the DIPS processing system. This class cannot
    /// be inherited.
    /// </summary>
    /// <remarks>
    /// Objects annotated with this <see cref="Attribute"/> must subclass the <see cref="Plugin"/>
    /// class.
    /// </remarks>
    [AttributeUsage( AttributeTargets.Class )]
    public sealed class PluginIdentifierAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginIdentifierAttribute"/>
        /// class with the name of the plugin.
        /// </summary>
        /// <param name="pluginName">The unique name of this plugin.</param>
        public PluginIdentifierAttribute( string pluginName )
        {
            if( string.IsNullOrEmpty( pluginName ) )
            {
                throw new ArgumentException( "pluginName" );
            }

            PluginName = pluginName;
        }


        /// <summary>
        /// Gets the name of the plugin represented by the class annotated with
        /// this <see cref="PluginIdentifierAttribute"/>.
        /// </summary>
        public string PluginName
        {
            get;
            private set;
        }
    }
}