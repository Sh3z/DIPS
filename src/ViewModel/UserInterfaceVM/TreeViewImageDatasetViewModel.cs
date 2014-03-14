using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using DIPS.Database.Objects;
using Database.Objects;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewImageDatasetViewModel : TreeViewItemViewModel
    {
        public readonly ImageDataset _imgDataset;

        public TreeViewImageDatasetViewModel(ImageDataset imgDataset, TreeViewPatientViewModel parentRegion) : base(null, true)
        {
            _imgDataset = imgDataset;
        }

        public string ImgDatasetName
        {
            get { return _imgDataset.series; }
        }

        protected override void LoadChildren()
        {
            foreach (PatientImage image in _imgDataset.relatedImages)
                base.Children.Add(new TreeViewImageViewModel(image, this));
        }
    }
}
