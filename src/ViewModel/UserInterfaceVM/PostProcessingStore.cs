using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the object used to retain <see cref="PostProcessingOption"/>s
    /// instances and provide access to them on-demand.
    /// </summary>
    public class PostProcessingStore : IEnumerable<PostProcessingOptions>
    {
        /// <summary>
        /// Ininitializes a new instance of the <see cref="PostProcessingStore"/>
        /// class.
        /// </summary>
        public PostProcessingStore()
        {
            _options = new Dictionary<string, PostProcessingOptions>();
        }

        /// <summary>
        /// Gets an enumerable set of identifiers of the available options
        /// instances within this <see cref="PostProcessingStore"/>.
        /// </summary>
        public IEnumerable<string> AvailableOptions
        {
            get
            {
                return _options.Keys;
            }
        }

        /// <summary>
        /// Gets the <see cref="PostProcessingOption"/>s instance with
        /// the associated identifier from this <see cref="PostProcessingStore"/>.
        /// </summary>
        /// <param name="identifier">The identifier of the
        /// <see cref="PostProcessingOption"/> instance to access.</param>
        /// <returns>The <see cref="PostProcessingOptions"/> object with the
        /// corresponding identifier within this <see cref="PostProcessingStore"/>,
        /// or <c>null</c> if no options instance is found.</returns>
        public PostProcessingOptions this[string identifier]
        {
            get
            {
                if( identifier != null && _options.ContainsKey( identifier ) )
                {
                    return _options[identifier];
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Adds a set of <see cref="PostProcessingOptions"/> to this
        /// <see cref="PostProcessingStore"/>.
        /// </summary>
        /// <param name="identifier">The identifier to associate with the
        /// options instance.</param>
        /// <param name="options">The <see cref="PostProcessingOptions"/> to retain
        /// within this <see cref="PostProcessingStore"/>.</param>
        /// <returns>true if the provided options are added to this store.</returns>
        public bool AddOptions( string identifier, PostProcessingOptions options )
        {
            if( string.IsNullOrEmpty( identifier ) == false || options == null )
            {
                return false;
            }

            if( _options.ContainsKey( identifier ) )
            {
                return false;
            }
            else
            {
                _options.Add( identifier, options );
                return true;
            }
        }

        /// <summary>
        /// Returns an enumerator used to step through each registered
        /// <see cref="PostProcessingOption"/>s.
        /// </summary>
        /// <returns>An enumerator used to enumerate through the collection.</returns>
        public IEnumerator<PostProcessingOptions> GetEnumerator()
        {
            return _options.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Retains the identifier-to-options mapping.
        /// </summary>
        private IDictionary<string, PostProcessingOptions> _options;
    }
}
