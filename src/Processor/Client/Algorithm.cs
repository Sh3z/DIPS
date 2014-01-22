using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the series of steps to apply to a set of incoming images. This class
    /// cannot be inherited.
    /// </summary>
    public sealed class Algorithm : IEnumerable<IAlgorithmStep>
    {
        /// <summary>
        /// Private initializer.
        /// </summary>
        private Algorithm()
        {
            IsSealed = false;
            _steps = new List<IAlgorithmStep>();
        }

        /// <summary>
        /// Initializes a new <see cref="Algorithm"/> with the provided set of steps.
        /// </summary>
        /// <param name="steps">The set of steps in which to initialize the new
        /// <see cref="Algorithm"/> instance with.</param>
        /// <returns>An <see cref="Algorithm"/> object containing the steps returned by
        /// the enumerator.</returns>
        public static Algorithm CreateWithSteps( IEnumerable<IAlgorithmStep> steps )
        {
            Algorithm theAlgorithm = new Algorithm();
            if( steps != null )
            {
                steps.ToList().ForEach( theAlgorithm.Add );
            }

            return theAlgorithm; 
        }


        /// <summary>
        /// Gets the number of <see cref="IAlgorithmStep"/>s contained within this
        /// <see cref="Algorithm"/>.
        /// </summary>
        public int NumberOfSteps
        {
            get
            {
                return _steps.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Algorithm"/> has been made
        /// read-only, and cannot be modified.
        /// </summary>
        public bool IsSealed
        {
            get;
            private set;
        }


        /// <summary>
        /// Seals this <see cref="Algorithm"/> from future modification.
        /// </summary>
        public void Seal()
        {
            lock( this )
            {
                IsSealed = true;
            }
        }

        /// <summary>
        /// Adds an additional <see cref="IAlgorithmStep"/> to this <see cref="Algorithm" />.
        /// </summary>
        /// <param name="item">The <see cref="IAlgorithmStep"/> to append to the end of this
        /// <see cref="Algorithm"/>.</param>
        /// <exception cref="InvalidOperationException">This <see cref="Algorithm"/> has been sealed
        /// and has become read-only.</exception>
        public void Add( IAlgorithmStep item )
        {
            _guardSealed();

            if( item != null )
            {
                _steps.Add( item );
            }
        }

        /// <summary>
        /// Removes an <see cref="IAlgorithmStep"/> from this <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="item">The <see cref="IAlgorithmStep"/> to remove from this
        /// <see cref="Algorithm"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="IAlgorithmStep"/> is removed
        /// from this <see cref="Algorithm"/>.</returns>
        public bool Remove( IAlgorithmStep item )
        {
            _guardSealed();
            return _steps.Remove( item );
        }

        /// <summary>
        /// Gets an enumerator used to enumerate through the collection.
        /// </summary>
        /// <returns>An enumerator used to enumerate through the collection.</returns>
        public IEnumerator<IAlgorithmStep> GetEnumerator()
        {
            return _steps.GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator used to enumerate through the collection.
        /// </summary>
        /// <returns>An enumerator used to enumerate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Determines whether this Algorithm is sealed. If so, it throws an
        /// InvalidOperationException.
        /// </summary>
        private void _guardSealed()
        {
            if( IsSealed )
            {
                throw new InvalidOperationException( "Cannot modify sealed Algorithm." );
            }
        }


        /// <summary>
        /// Contains the step of steps represented by this Algorithm.
        /// </summary>
        private List<IAlgorithmStep> _steps;
    }
}
