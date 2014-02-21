using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Database.Objects;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewImageViewModel : TreeViewItemViewModel
    {
        private readonly PatientImage _image;

        public TreeViewImageViewModel(PatientImage image, TreeViewImageDatasetViewModel parent) : base(parent, true)
        {
            _image = image;
        }

        public int ImageName
        {
            get { return _image.imgID; }
        }

    }
}
