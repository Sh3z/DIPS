using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Database;
using Database.Repository;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewItemViewModel : BaseViewModel
    {
        static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

        private readonly ObservableCollection<TreeViewItemViewModel> _children;
        private readonly TreeViewItemViewModel _parent;

        private bool _isExpanded;
        private bool _isSelected;

        protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
        {
            _parent = parent;

            _children = new ObservableCollection<TreeViewItemViewModel>();

            if (lazyLoadChildren)
            {
                _children.Add(DummyChild);
            }
        }

        private TreeViewItemViewModel()
        {
        }

        public ObservableCollection<TreeViewItemViewModel> Children
        {
            get { return _children; }
        }

        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }

                if (_isExpanded && _parent != null)
                {
                    _parent.IsExpanded = true;
                }

                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    ImageViewModel = this;

                    if (ImageViewModel is TreeViewImageViewModel)
                    {
                        TreeViewImageViewModel tivm = (TreeViewImageViewModel) ImageViewModel;
                        setImage(tivm.ImageName.ToString());
                        _ViewExistingDatasetViewModel.ListOfAlgorithms = SelectedImage.AlgorithmCollection;
                        _ViewExistingDatasetViewModel.ImageInfo = "Please Select An Image To View Image Information Here.";
                        GetImageInfo(tivm.ImageName.ToString());
                    }
                    
                }
            }
        }

        protected virtual void LoadChildren()
        {
        }

        public TreeViewItemViewModel Parent
        {
            get { return _parent; }
        }

        private void GetImageInfo(String fileID)
        {
            ImageRepository imgRepository = new ImageRepository();
            BaseImageInfo = string.Empty;

            ObservableCollection < String > listOfInfo = new ObservableCollection<String>(); 
            BaseImageInfo = imgRepository.retrieveImageProperties(fileID);
        }

        public void setImage(String fileID)
        {
            SelectedImageRepository sir = new SelectedImageRepository();
            byte[] image = sir.getUnprocessedImage(fileID);
            sir.updateAlgorithmList(SelectedImage.UID);

            BitmapImage theBmp = ToImage(image);
            BaseUnProcessedImage = theBmp;
            SelectedImage.ImageNumberSelected = fileID;
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
