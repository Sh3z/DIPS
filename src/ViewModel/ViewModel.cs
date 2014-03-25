using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represent the base class of an object providing the presentation logic
    /// of a control. This class is abstract.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/>
        /// abstract class.
        /// </summary>
        protected ViewModel()
        {
        }


        /// <summary>
        /// Occurs when a property within this <see cref="ViewModel"/> has been modified.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Signals to any listeners that a property within this <see cref="ViewModel"/>
        /// has been modified.
        /// </summary>
        /// <param name="propertyName">The name of the property within this
        /// <see cref="ViewModel"/> that has been modified.</param>
        protected void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            if( string.IsNullOrEmpty( propertyName ) == false && PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
    }
}
