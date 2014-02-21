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
                        _ViewExistingDatasetViewModel.ImageInfo = string.Empty;
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

            ObservableCollection < String > listOfInfo = new ObservableCollection<String>(); 
            listOfInfo = imgRepository.retrieveImageProperties(fileID);


            foreach (String str in listOfInfo)
            {
                BaseImageInfo += str;
                BaseImageInfo += System.Environment.NewLine;
            }
        }

        public void setImage(String fileID)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveImage_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = fileID;
                byte[] image = (byte[])cmd.ExecuteScalar();

                BitmapImage theBmp = ToImage(image);
                BaseUnProcessedImage = theBmp;
            }
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
