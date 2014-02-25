using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a set of <see cref="IProcessedImage"/>s generated as the
    /// output of a job.
    /// </summary>
    public class ProcessedImageSet : ISet<IProcessedImage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessedImageSet"/>.
        /// </summary>
        public ProcessedImageSet()
        {
            _set = new HashSet<IProcessedImage>( new ProcessedImageIdentifierComparer() );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessedImageSet"/> with
        /// the provided set of existing elements.
        /// </summary>
        /// <param name="images">The set of <see cref="IProcessedImage"/> objects
        /// to copy into the new <see cref="ProcessedImageSet"/>.</param>
        public ProcessedImageSet( IEnumerable<IProcessedImage> images )
        {
            IEqualityComparer<IProcessedImage> comparator =
                new ProcessedImageIdentifierComparer();
            if( images != null )
            {
                _set = new HashSet<IProcessedImage>( images, comparator );
            }
            else
            {
                _set = new HashSet<IProcessedImage>( comparator );
            }
        }


        /// <summary>
        /// Returns the <see cref="Image"/> of the stored
        /// <see cref="IProcessedImage"/> for the given identifier.
        /// </summary>
        /// <param name="identifier">The identifier provided to the input
        /// to identify it to the client.</param>
        /// <returns>The <see cref="Image"/> with the associated identifier
        /// within this <see cref="ProcessedImageSet"/>; or null if no
        /// image shares this identifier.</returns>
        public Image this[string identifier]
        {
            get
            {
                IProcessedImage img = this.FirstOrDefault( x => x.Identifier == identifier );
                if( img != null )
                {
                    return img.Output;
                }
                else
                {
                    return null;
                }
            }
        }


        #region ISet Members

        /// <summary>
        /// Adds an <see cref="IProcessedImage"/> to the current set and
        /// returns a value to indicate if the element was successfully added.
        /// </summary>
        /// <param name="item">The <see cref="IProcessedImage"/> to add to the
        /// set.</param>
        /// <returns>true if the element is added to the set; false if the element
        /// is already in the set</returns>
        public bool Add( IProcessedImage item )
        {
            return _set.Add( item );
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">The collection of items to remove from the set.</param>
        public void ExceptWith( IEnumerable<IProcessedImage> other )
        {
            _set.ExceptWith( other );
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that
        /// are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void IntersectWith( IEnumerable<IProcessedImage> other )
        {
            _set.IntersectWith( other );
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of
        /// a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a proper subset of other;
        /// otherwise, false.</returns>
        public bool IsProperSubsetOf( IEnumerable<IProcessedImage> other )
        {
            return _set.IsProperSubsetOf( other );
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of
        /// a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a proper superset of other;
        /// otherwise, false.</returns>
        public bool IsProperSupersetOf( IEnumerable<IProcessedImage> other )
        {
            return _set.IsProperSupersetOf( other );
        }

        /// <summary>
        /// Determines whether a set is a subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a subset of other; otherwise, false.</returns>
        public bool IsSubsetOf( IEnumerable<IProcessedImage> other )
        {
            return _set.IsSubsetOf( other );
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified
        /// collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a superset of other;
        /// otherwise, false.</returns>
        public bool IsSupersetOf( IEnumerable<IProcessedImage> other )
        {
            return _set.IsSupersetOf( other );
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set and other share at least one
        /// common element; otherwise, false.</returns>
        public bool Overlaps( IEnumerable<IProcessedImage> other )
        {
            return _set.Overlaps( other );
        }

        /// <summary>
        /// Determines whether the current set and the specified collection contain
        /// the same elements.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is equal to other; otherwise, false.</returns>
        public bool SetEquals( IEnumerable<IProcessedImage> other )
        {
            return _set.SetEquals( other );
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present
        /// either in the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void SymmetricExceptWith( IEnumerable<IProcessedImage> other )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies the current set so that it contains all elements that are
        /// present in either the current set or the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void UnionWith( IEnumerable<IProcessedImage> other )
        {
            _set.UnionWith( other );
        }

        void ICollection<IProcessedImage>.Add( IProcessedImage item )
        {
            Add( item );
        }

        /// <summary>
        /// Removes all items from the <see cref="ProcessedImageSet"/>.
        /// </summary>
        public void Clear()
        {
            _set.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="ProcessedImageSet"/> contains a
        /// cerain value.
        /// </summary>
        /// <param name="item">The object to locate within the
        /// <see cref="ProcessedImageSet"/>.</param>
        /// <returns><c>true</c> if the item is present within the
        /// <see cref="ProcessedImageSet"/>; otherwise, <c>false</c></returns>
        public bool Contains( IProcessedImage item )
        {
            return _set.Contains( item );
        }

        /// <summary>
        /// Copies the elements of a <see cref="ProcessedImageSet"/> into
        /// the <see cref="System.Array"/>.
        /// </summary>
        /// <param name="array">The one-dimensional destination array.</param>
        /// <param name="arrayIndex">The zero-based index at which copying
        /// begins.</param>
        public void CopyTo( IProcessedImage[] array, int arrayIndex )
        {
            _set.CopyTo( array, arrayIndex );
        }

        /// <summary>
        /// Gets the number of elements contained within the
        /// <see cref="ProcessedImageSet"/>
        /// </summary>
        public int Count
        {
            get
            {
                return _set.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ProcessedImageSet"/>
        /// is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            { 
                return false;
            }
        }

        /// <summary>
        /// Removes the specified element from the <see cref="ProcessedImageSet"/>.
        /// </summary>
        /// <param name="item">The object to remove from the
        /// <see cref="ProcessedImageSet"/>.</param>
        /// <returns><c>true</c> if the object is successfully removed from the set;
        /// otherwise, <c>false</c>.</returns>
        public bool Remove( IProcessedImage item )
        {
            return _set.Remove( item );
        }

        /// <summary>
        /// Returns an enumerator used to enumerate through the collection.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerator"/> used
        /// to enumerate through the collection.</returns>
        public IEnumerator<IProcessedImage> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        /// <summary>
        /// Contains the actual set implementation.
        /// </summary>
        private HashSet<IProcessedImage> _set;
    }
}
