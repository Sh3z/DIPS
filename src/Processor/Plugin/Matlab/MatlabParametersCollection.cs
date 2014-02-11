using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents an aggregation of <see cref="MatlabParameter"/>s used by the
    /// <see cref="MatlabProcess"/>.
    /// </summary>
    [Serializable]
    [DebuggerDisplay( "Count = {Count}" )]
    public class MatlabParametersCollection : CollectionBase, IList<MatlabParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabParametersCollection"/>
        /// class.
        /// </summary>
        public MatlabParametersCollection()
        {
        }


        /// <summary>
        /// Gets the number of <see cref="MatlabParameter"/>s present within this
        /// <see cref="MatlabParametersCollection"/>.
        /// </summary>
        public int Count
        {
            get
            {
                return List.Count;
            }
        }

        /// <summary>
        /// Adds a new parameter to this <see cref="MatlabParametersCollection"/>.
        /// </summary>
        /// <param name="item">The <see cref="MatlabParameter"/> to add to this
        /// <see cref="MatlabParametersCollection"/>.</param>
        public void Add( MatlabParameter item )
        {
            List.Add( item );
        }

        /// <summary>
        /// Removes all parameters from this <see cref="MatlabParametersCollection"/>.
        /// </summary>
        public void Clear()
        {
            List.Clear();
        }

        /// <summary>
        /// Determines whether this <see cref="MatlabParametersCollection"/>
        /// contains a specific parameter.
        /// </summary>
        /// <param name="item">The <see cref="MatlabParameter"/> to determine is contained
        /// within this <see cref="MatlabParametersCollection"/>.</param>
        /// <returns><c>true</c> if the <see cref="MatlabParameter"/> is present
        /// within this <see cref="MatlabParametersCollection"/>; <c>false</c>
        /// otherwise.</returns>
        public bool Contains( MatlabParameter item )
        {
            return List.Contains( item );
        }

        /// <summary>
        /// Copies the elements of this <see cref="MatlabParametersCollection"/>
        /// to the one-dimensional <see cref="System.Array"/>.
        /// </summary>
        /// <param name="array">The destination <see cref="System.Array"/> to copy
        /// the contents of this <see cref="MatlabParametersCollection"/> to.</param>
        /// <param name="arrayIndex">The index in which to begin copying elements from
        /// this <see cref="MatlabParametersCollection"/>.</param>
        public void CopyTo( MatlabParameter[] array, int arrayIndex )
        {
            List.CopyTo( array, arrayIndex );
        }

        /// <summary>
        /// Removes an item from this <see cref="MatlabParametersCollection"/>.
        /// </summary>
        /// <param name="item">The <see cref="MatlabParameter"/> to remove
        /// from this <see cref="MatlabParametersCollection"/>.</param>
        /// <returns><c>true</c> if the item was successfully removed from this
        /// <see cref="MatlabParametersCollection"/>; <c>false</c>
        /// otherwise.</returns>
        public bool Remove( MatlabParameter item )
        {
            if( List.Contains( item ) )
            {
                List.Remove( item );
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines the index of a specific item within this
        /// <see cref="MatlabParametersCollection"/>.
        /// </summary>
        /// <param name="item">The <see cref="MatlabParameter"/> in which
        /// to resolve the index of.</param>
        /// <returns>The zero-based index of the <see cref="MatlabParameter"/>
        /// within this <see cref="MatlabParametersCollection"/>, or
        /// -1 if the item is not present.</returns>
        public int IndexOf( MatlabParameter item )
        {
            return List.IndexOf( item );
        }

        /// <summary>
        /// Inserts a <see cref="MatlabParameter"/> into this
        /// <see cref="MatlabParametersCollection"/> at a specific index.
        /// </summary>
        /// <param name="index">The zero-based index in which to insert
        /// the item.</param>
        /// <param name="item">The <see cref="MatlabParameter"/> to insert into
        /// this <see cref="MatlabParametersCollection"/>.</param>
        public void Insert( int index, MatlabParameter item )
        {
            List.Insert( index, item );
        }

        /// <summary>
        /// Removes the item at the specific index within this
        /// <see cref="MatlabParametersCollection"/>.
        /// </summary>
        /// <param name="index">The zero-based index in which to remove
        /// the item.</param>
        public void RemoveAt( int index )
        {
            List.RemoveAt( index );
        }

        /// <summary>
        /// Gets or sets the <see cref="MatlabParameter"/> at the specified index
        /// within this <see cref="MatlabParametersCollection"/>.
        /// </summary>
        /// <param name="index">The zero-based index in which to access
        /// this <see cref="MatlabParametersCollection"/>.</param>
        /// <returns>The <see cref="MatlabParameter"/> at the specified
        /// index within this <see cref="MatlabParametersCollection"/>.</returns>
        public MatlabParameter this[int index]
        {
            get
            {
                return List[index] as MatlabParameter;
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Gets an enumerator used to enumerate through the collection.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerator"/>
        /// used to step through the contents of this
        /// <see cref="MatlabParametersCollection"/>.</returns>
        public IEnumerator<MatlabParameter> GetEnumerator()
        {
            return List.OfType<MatlabParameter>().GetEnumerator();
        }

        bool ICollection<MatlabParameter>.IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}
