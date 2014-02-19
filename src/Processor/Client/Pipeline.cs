using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Util.Extensions;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the processing pipeline definition used by the client
    /// to construct informal pipelines.
    /// </summary>
    [DebuggerDisplay( "Count = {Count}" )]
    public class PipelineDefinition : IList<AlgorithmDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineDefinition"/> class.
        /// </summary>
        public PipelineDefinition()
        {
            _list = new List<AlgorithmDefinition>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineDefinition"/> class
        /// with the existing set of processes.
        /// </summary>
        /// <param name="elements">The set of processes to copy into the new
        /// <see cref="PipelineDefinition"/> instance.</param>
        public PipelineDefinition( IEnumerable<AlgorithmDefinition> elements )
            : this()
        {
            if( elements != null )
            {
                elements.ForEach( Add );
            }
        }


        /// <summary>
        /// Gets or sets the process definition within this <see cref="PipelineDefinition"/>
        /// at the specified index.
        /// </summary>
        /// <param name="index">The index in which to get or set the
        /// process within this <see cref="PipelineDefinition"/>.</param>
        /// <returns>The process at the specified index within this
        /// <see cref="PipelineDefinition"/>.</returns>
        public AlgorithmDefinition this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        /// <summary>
        /// Gets the number of processes within this <see cref="PipelineDefinition"/>.
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        bool ICollection<AlgorithmDefinition>.IsReadOnly
        {
            get { return false; }
        }


        /// <summary>
        /// Adds a process definition to this <see cref="PipelineDefinition"/>.
        /// </summary>
        /// <param name="item">The process to append to the end of this
        /// <see cref="PipelineDefinition"/>.</param>
        public void Add( AlgorithmDefinition item )
        {
            _list.Add( item );
        }

        /// <summary>
        /// Removes all process definitions from this <see cref="PipelineDefinition"/>.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Determines whether this <see cref="PipelineDefinition"/> contains the specified
        /// process.
        /// </summary>
        /// <param name="item">The process definition to determine is present.</param>
        /// <returns><c>true</c> if the provided process definition is present within
        /// this <see cref="PipelineDefinition"/>; <c>false</c> otherwise.</returns>
        public bool Contains( AlgorithmDefinition item )
        {
            return _list.Contains( item );
        }

        /// <summary>
        /// Copies the elements of this <see cref="PipelineDefinition"/> into the
        /// two-dimensional <c>System.Array</c>.
        /// </summary>
        /// <param name="array">The <c>System.Array</c> in which to copy
        /// elements to.</param>
        /// <param name="arrayIndex">The zero-based index in which copying
        /// begins.</param>
        public void CopyTo( AlgorithmDefinition[] array, int arrayIndex )
        {
            _list.CopyTo( array, arrayIndex );
        }

        /// <summary>
        /// Removes the first occurance of the specified process from this
        /// <see cref="PipelineDefinition"/>.
        /// </summary>
        /// <param name="item">The process definition to be removed.</param>
        /// <returns><c>true</c> if the provided process was removed from
        /// this <see cref="PipelineDefinition"/>; <c>false</c> otherwise.</returns>
        public bool Remove( AlgorithmDefinition item )
        {
            return _list.Remove( item );
        }

        /// <summary>
        /// Determines the index of a specific process definition within
        /// this <see cref="PipelineDefinition"/>.
        /// </summary>
        /// <param name="item">The procss definition in which to resolve
        /// the index of.</param>
        /// <returns>The index of the provided process definition within this
        /// <see cref="PipelineDefinition"/>.</returns>
        public int IndexOf( AlgorithmDefinition item )
        {
            return _list.IndexOf( item );
        }

        /// <summary>
        /// Inserts a process definition into this <see cref="PipelineDefinition"/> at
        /// the specified index.
        /// </summary>
        /// <param name="index">The index in which to insert the process
        /// definition into.</param>
        /// <param name="item">The process definition to insert into this
        /// <see cref="PipelineDefinition"/>.</param>
        public void Insert( int index, AlgorithmDefinition item )
        {
            _list.Insert( index, item );
        }

        /// <summary>
        /// Removes the process definition within this <see cref="PipelineDefinition"/>
        /// at the specified index.
        /// </summary>
        /// <param name="index">The index of the process to remove from
        /// within this <see cref="PipelineDefinition"/>.</param>
        public void RemoveAt( int index )
        {
            _list.RemoveAt( index );
        }

        /// <summary>
        /// Returns an enumerator that enumerates through each process within
        /// this <see cref="PipelineDefinition"/>.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerator"/>
        /// used to step through each process within this <see cref="PipelineDefinition"/>.</returns>
        public IEnumerator<AlgorithmDefinition> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Contains the internal list implementation.
        /// </summary>
        private IList<AlgorithmDefinition> _list;
    }
}
