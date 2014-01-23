using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents the declaration of a single property used by a plugin component.
    /// This class cannot be inherited.
    /// </summary>
    [DebuggerDisplay("Name = {Name} | Type = {Type}")]
    public sealed class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="type">The type of the property.</param>
        public Property( string name, Type type )
        {
            if( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( "name" );
            }

            if( type == null )
            {
                throw new ArgumentNullException( "type" );
            }

            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets the name of the property used by the plugin component.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the property used by the plugin component.
        /// </summary>
        public Type Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value of this property.
        /// </summary>
        /// <exception cref="ArgumentException">the value is not of the same type
        /// as specified in this <see cref="Property"/> instance's Type
        /// property.</exception>
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if( value == null )
                {
                    return;
                }

                if( _isValidType( value.GetType() ) == false )
                {
                    throw new ArgumentException( "Invalid Value type." );
                }

                _value = value;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object _value;


        /// <summary>
        /// Checks if the incoming type is compatible with that of this property's.
        /// </summary>
        /// <param name="t">The incoming type.</param>
        /// <returns>true if the incoming type is acceptable.</returns>
        private bool _isValidType( Type t )
        {
            if( t == Type )
            {
                return true;
            }

            if( t.IsSubclassOf( Type ) )
            {
                return true;
            }

            if( Type.IsInterface && t.GetInterfaces().Contains( Type ) )
            {
                return true;
            }

            return false;
        }
    }
}
