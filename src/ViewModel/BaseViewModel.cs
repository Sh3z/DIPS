using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DIPS.ViewModel.UserInterfaceVM;

namespace DIPS.ViewModel
{
    public abstract class BaseViewModel : ViewModel
    {
        public static BaseViewModel ViewModel { get; set; }
        
        public static Frame OverallFrame { get; set; }

        readonly public static ViewExistingDatasetViewModel _ViewExistingDatasetViewModel = new ViewExistingDatasetViewModel();
        readonly public static LoadNewDsStep1ViewModel _LoadNewDsStep1ViewModel = new LoadNewDsStep1ViewModel();
        readonly public static AlgorithmBuilderViewModel _AlgorithmBuilderViewModel = new AlgorithmBuilderViewModel();
        readonly public static LoadNewDsStep2ViewModel _LoadNewDsStep2ViewModel = new LoadNewDsStep2ViewModel();
        readonly public static LoadNewDsStep3ViewModel _LoadNewDsStep3ViewModel = new LoadNewDsStep3ViewModel();
        readonly public static MainViewModel _MainViewModel = new MainViewModel(OverallFrame);
        readonly public static TreeViewFilterViewModel _FilterViewModel = new TreeViewFilterViewModel();
        readonly public static ViewAlgorithmViewModel _ViewAlgorithmViewModel = new ViewAlgorithmViewModel();

        public static object ImageViewModel { get; set; }

        private BitmapImage _baseUnprocessedImage;

        public BitmapImage BaseUnProcessedImage
        {
            get { return _baseUnprocessedImage; }
            set
            {
                _baseUnprocessedImage = value; 
                OnPropertyChanged();
                _ViewExistingDatasetViewModel.ImgUnprocessed = _baseUnprocessedImage;
                
            }
        }

        private BitmapImage _baseProcessedImage;
        public BitmapImage BaseProcessedImage
        {
            get { return _baseProcessedImage; }
            set
            {
                _baseProcessedImage = value;
                OnPropertyChanged();
                _ViewExistingDatasetViewModel.ImgProcessed = _baseProcessedImage;
            }
        }


        private String _baseimageInfo;

        public String BaseImageInfo
        {
            get { return _baseimageInfo; }
            set
            {
                _baseimageInfo = value; 
                OnPropertyChanged();
                _ViewExistingDatasetViewModel.ImageInfo = _baseimageInfo;
            }
        }
        
            
    }
}
