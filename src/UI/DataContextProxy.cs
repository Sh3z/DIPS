using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DIPS.UI
{
    /// <summary>
    /// Represents an object providing the ability to provide a proxy
    /// for a data context. This class cannot be inherited.
    /// </summary>
    public sealed class DataContextProxy : Freezable
    {
        /// <summary>
        /// Dependency Property initialize.
        /// </summary>
        static DataContextProxy()
        {
            DataContextProperty = DependencyProperty.Register(
                "DataContext", typeof( object ), typeof( DataContextProxy ) );
        }


        /// <summary>
        /// Identifies the DataContext Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DataContextProperty;


        /// <summary>
        /// Gets or sets the data context this object provides a proxy
        /// for.
        /// </summary>
        public object DataContext
        {
            get
            {
                return GetValue( DataContextProperty );
            }
            set
            {
                SetValue( DataContextProperty, value );
            }
        }


        /// <summary>
        /// Creates a new instance of the <see cref="DataContextProxy"/>
        /// class.
        /// </summary>
        /// <returns>A new instance of the <see cref="DataContextProxy"/>
        /// class.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new DataContextProxy();
        }
    }
}
