using Database.Objects;
using DIPS.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewTechniqueViewModel : TreeViewItemViewModel
    {
        private readonly ProcessTechnique _techniques;

        public TreeViewTechniqueViewModel(ProcessTechnique techniques, TreeViewImageDatasetViewModel parent) : base(parent, true)
        {
            _techniques = techniques;
        }

        public string techniqueName
        {
            get{ return _techniques.algorithm; }
        }

        protected override void LoadChildren()
        {
            foreach (PatientImage img in _techniques.images)
                base.Children.Add(new TreeViewImageViewModel(img, this));
        }
    }
}
