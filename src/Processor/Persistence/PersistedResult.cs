using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Persistence
{
    /// <summary>
    /// Represents a previously persisted result.
    /// </summary>
    public class PersistedResult : IProcessedImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedResult"/>
        /// class.
        /// </summary>
        /// <param name="img">The <see cref="Output"/> that was persisted.</param>
        /// <param name="identifier">The identifier originally provided by the
        /// client.</param>
        /// <exception cref="ArgumentNullException">img is null</exception>
        /// <exception cref="ArgumentException">identifier is null or
        /// empty</exception>
        public PersistedResult( Image img, object identifier )
        {
            if( img == null )
            {
                throw new ArgumentNullException( "img" );
            }

            Output = img;
            Identifier = identifier;
        }


        /// <summary>
        /// Gets the <see cref="Image"/> loaded from the persister.
        /// </summary>
        public Image Output
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the identifier provided to the image.
        /// </summary>
        public object Identifier
        {
            get;
            private set;
        }
    }
}
