using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the main presentation layer within the application.
    /// </summary>
    public interface IFemoreViewModel : INotifyPropertyChanged
    {
        Bitmap ImageToProcess
        {
            set;
        }
    }
}
