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
        private String selectedImageID;

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
                        if (tivm.processed) setProcessedImage(tivm.ImageName.ToString());
                        else setImage(tivm.ImageName.ToString());
                        _ViewExistingDatasetViewModel.ImageInfo = string.Empty;
                        GetImageInfo(selectedImageID);
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
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveImage_v001", conn);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = fileID;
                SqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                byte[] image = (byte[])dataReader.GetValue(dataReader.GetOrdinal("imageBlob"));
                String UID = dataReader.GetString(dataReader.GetOrdinal("imageUID"));
                dataReader.Close();

                BitmapImage theBmp = ToImage(image);
                BaseUnProcessedImage = theBmp;


                SqlCommand cmd2 = new SqlCommand("spr_RetrieveProcessedImage_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@imageUID", SqlDbType.VarChar).Value = UID;
                byte[] processed = (byte[]) cmd2.ExecuteScalar();

                if (processed != null)
                {
                    BitmapImage processedBmp = ToImage(processed);

                    BaseProcessedImage = processedBmp;
                }
                selectedImageID = fileID;
            }
        }

        public void setProcessedImage(String fileID)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveProcessedImageB_v001", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fileID", SqlDbType.VarChar).Value = fileID;
                SqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                byte[] image = (byte[])dataReader.GetValue(dataReader.GetOrdinal("imageBlob"));
                String UID = dataReader.GetString(dataReader.GetOrdinal("imageUID"));
                dataReader.Close();

                BitmapImage theBmp = ToImage(image);
                BaseProcessedImage = theBmp;


                SqlCommand cmd2 = new SqlCommand("spr_RetrieveImageByUID_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@imageUID", SqlDbType.VarChar).Value = UID;
                SqlDataReader dataReader2 = cmd2.ExecuteReader();

                dataReader2.Read();
                byte[] processed = (byte[])dataReader2.GetValue(dataReader2.GetOrdinal("imageBlob"));
                selectedImageID = dataReader2.GetInt32(dataReader2.GetOrdinal("fileID")).ToString();
                dataReader2.Close();

                if (processed != null)
                {
                    BitmapImage processedBmp = ToImage(processed);

                    BaseUnProcessedImage = processedBmp;
                }
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
