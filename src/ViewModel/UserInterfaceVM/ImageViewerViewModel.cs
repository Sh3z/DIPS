using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ImageViewerViewModel : BaseViewModel
    {
        private BitmapImage _selectedImage;

        public BitmapImage SelectedImage
        {
            get { return _selectedImage; }
            set 
                { 
                    _selectedImage = value;
                    OnPropertyChanged();
                }
        }

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }
        private IUnityContainer _container;

        public ImageViewerViewModel()
        {

        } 
    }
}
