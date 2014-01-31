using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents the declaration of a single property used by a plugin component.
    /// This class cannot be inherited.
    /// </summary>
    [DebuggerDisplay("Name = {Name} | Type = {Type}")]
    public sealed class Property : ICloneable, IEquatable<Property>
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
            internal set;
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
                if( _isValidType( value ) == false )
                {
                    throw new ArgumentException( "Invalid Value type." );
                }

                _value = value;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object _value;

        /// <summary>
        /// Gets the <see cref="ICompressor"/> used to compress the value
        /// of this <see cref="Property"/>.
        /// </summary>
        public ICompressor Compressor
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="IValueConverter"/> to use to format the value
        /// appropriatley.
        /// </summary>
        public IValueConverter Converter
        {
            get;
            internal set;
        }

        /// <summary>
        /// Determines whether this <see cref="Property"/> is represented by the
        /// provided <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to determine is contained by
        /// this <see cref="Property"/>.</param>
        /// <returns><c>true</c> if this <see cref="Property"/> is represented by the
        /// provided <see cref="Type"/>; <c>false</c> otherwise.</returns>
        public bool IsOfType( Type type )
        {
            if( type == null )
            {
                return false;
            }
            else
            {
                return _isTypeMatch( type );
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            Property clone = new Property( Name, Type );
            clone.Value = Value;
            return clone;
        }

        /// <summary>
        /// Determines whether this <see cref="Property"/> is identical to the
        /// <see cref="Property"/> instance provided.
        /// </summary>
        /// <param name="other">The <see cref="Property"/> to compare against.</param>
        /// <returns><c>true</c> if this <see cref="Property"/> is identical to that
        /// of the parameter; <c>false</c> otherwise.</returns>
        public bool Equals( Property other )
        {
            if( other == null )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            if( Name != other.Name )
            {
                return false;
            }

            if( Type != other.Type )
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Checks if the incoming type is compatible with that of this property's.
        /// </summary>
        /// <param name="t">The incoming object.</param>
        /// <returns>true if the incoming type is acceptable.</returns>
        private bool _isValidType( object t )
        {
            if( t == null && Type.IsValueType )
            {
                return false;
            }

            if( t == null )
            {
                // Can set null for reference types - continue otherwise.
                return true;
            }

            Type theType = t.GetType();
            return _isTypeMatch( theType );
        }

        private bool _isTypeMatch( Type theType )
        {
            if( theType == Type )
            {
                return true;
            }

            if( theType.IsSubclassOf( Type ) )
            {
                return true;
            }

            if( Type.IsInterface && theType.GetInterfaces().Contains( Type ) )
            {
                return true;
            }

            return false;
        }
    }
}
