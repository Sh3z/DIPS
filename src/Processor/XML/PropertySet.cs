using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.XML
{
    /// <summary>
    /// Represents a set of <see cref="Property"/> objects parsed from XML.
    /// </summary>
    public class PropertySet : IPropertySet
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
                var property = _propertyWithName( name );
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
                var property = _propertyWithName( name );
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
        /// Gets the first property with the specified name.
        /// </summary>
        /// <param name="name">The name of the property to retrieve. Case sensitive.</param>
        /// <returns>The first property with the specified name, or null if the property
        /// cannot be found.</returns>
        private Property _propertyWithName( string name )
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
