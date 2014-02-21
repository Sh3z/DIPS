using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DIPS.ViewModel.Objects
{
    public class ImageHandler : BaseViewModel
    {
        private static BitmapImage _unProcessedImage;

        public BitmapImage UnProcessedImage
        {
            get { return _unProcessedImage; }
            set
            {
                _unProcessedImage = value;
                OnPropertyChanged();
            }
        }
        
    }
}
