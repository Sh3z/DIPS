using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents a set of <see cref="Property"/> objects parsed from XML.
    /// </summary>
    public class PropertySet : ISet<Property>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySet"/> class.
        /// </summary>
        public PropertySet()
        {
            _set = new HashSet<Property>();
        }


        /// <summary>
        /// Gets or sets the value of the <see cref="Property"/> within this
        /// <see cref="PropertySet"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Property"/> to retrieve
        /// or set the value of. Case sensitive.</param>
        /// <returns>The value of the <see cref="Property"/> within this
        /// <see cref="PropertySet"/> with the given name.</returns>
        public object this[string name]
        {
            get
            {
                var property = PropertyForName( name );
                if( property != null )
                {
                    return property.Value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                var property = PropertyForName( name );
                if( property != null )
                {
                    property.Value = value;
                }
                else
                {
                    Type valueType = value == null ? typeof( object ) : value.GetType();
                    Property p = new Property( name, valueType );
                    p.Value = value;
                    Add( p );
                }
            }
        }

        /// <summary>
        /// Determines whether this <see cref="IPropertySet"/> contains a
        /// <see cref="Property"/> with the provided name.
        /// </summary>
        /// <param name="propertyName">The case-sentive name of the property
        /// to check exists within this <see cref="IPropertySet"/></param>
        /// <returns><c>true</c> if this <see cref="IPropertySet"/> contains a
        /// <see cref="Property"/> with the name provided; <c>false</c>
        /// otherwise.</returns>
        public bool Contains( string propertyName )
        {
            return _set.Any( x => x.Name == propertyName );
        }

        /// <summary>
        /// Resolves the <see cref="Property"/> contained within this <see cref="IPropertySet"/>
        /// for the given name.
        /// </summary>
        /// <param name="name">The name of the property to locate.</param>
        /// <returns>An instance of <see cref="Property"/> which shares the name given by the
        /// input. If no properties consist of this name, this returns null.</returns>
        public Property PropertyForName( string name )
        {
            return _set.Where( x => x.Name == name ).FirstOrDefault();
        }


        #region ISet Members

        public bool Add( Property item )
        {
            return _set.Add( item );
        }

        public void ExceptWith( IEnumerable<Property> other )
        {
            _set.ExceptWith( other );
        }

        public void IntersectWith( IEnumerable<Property> other )
        {
            _set.IntersectWith( other );
        }

        public bool IsProperSubsetOf( IEnumerable<Property> other )
        {
            return _set.IsProperSubsetOf( other );
        }

        public bool IsProperSupersetOf( IEnumerable<Property> other )
        {
            return _set.IsProperSupersetOf( other );
        }

        public bool IsSubsetOf( IEnumerable<Property> other )
        {
            return _set.IsSubsetOf( other );
        }

        public bool IsSupersetOf( IEnumerable<Property> other )
        {
            return _set.IsSupersetOf( other );
        }

        public bool Overlaps( IEnumerable<Property> other )
        {
            return _set.Overlaps( other );
        }

        public bool SetEquals( IEnumerable<Property> other )
        {
            return _set.SetEquals( other );
        }

        public void SymmetricExceptWith( IEnumerable<Property> other )
        {
            _set.SymmetricExceptWith( other );
        }

        public void UnionWith( IEnumerable<Property> other )
        {
            _set.UnionWith( other );
        }

        void ICollection<Property>.Add( Property item )
        {
            Add( item );
        }

        public void Clear()
        {
            _set.Clear();
        }

        public bool Contains( Property item )
        {
            return _set.Contains( item );
        }

        public void CopyTo( Property[] array, int arrayIndex )
        {
            _set.CopyTo( array, arrayIndex );
        }

        public int Count
        {
            get
            {
                return _set.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove( Property item )
        {
            return _set.Remove( item );
        }

        public IEnumerator<Property> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        /// <summary>
        /// Contains the actual ISet implementation we are composed of.
        /// </summary>
        private HashSet<Property> _set;
    }
}
