using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.ViewModel.Unity;
using System.Windows.Media.Imaging;
using DIPS.ViewModel.UserInterfaceVM;
using DIPS.UI.Pages;

namespace DIPS.UI.Unity.Implementations
{
    public class ImageViewer : IImageView
    {
        private BitmapImage _selectedImage;

        public BitmapImage SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; }
        }

        public void HideDialog()
        {
            throw new NotImplementedException();
        }


        public void OpenDialog(BitmapImage theImage)
        {
            ImageView iv = new ImageView();
            ImageViewerViewModel vm = new ImageViewerViewModel();
            vm.SelectedImage = theImage;
            this.SelectedImage = theImage;
            iv.DataContext = vm;

            iv.ShowDialog();
        }
    }
}
