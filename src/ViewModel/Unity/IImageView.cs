using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DIPS.ViewModel.Unity
{
    public interface IImageView
    {
        BitmapImage SelectedImage { set; get; }

        void OpenDialog(BitmapImage theImage);
        void HideDialog();
    }
}
