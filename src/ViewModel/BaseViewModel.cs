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
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using DIPS.Unity;
using Microsoft.Practices.Unity;

namespace DIPS.ViewModel
{
    public abstract class BaseViewModel : ViewModel
    {
        

        public static BaseViewModel ViewModel { get; set; }
        
        public static Frame OverallFrame
        {
            get
            {
                return _frame;
            }
            set
            {
                _frame = value;
                _setupPostProcessor();
                _PostProcessingViewModel.OverallFrame = value;
            }
        }
        private static Frame _frame;

        public static ViewExistingDatasetViewModel _ViewExistingDatasetViewModel;
        readonly public static LoadNewDsStep1ViewModel _LoadNewDsStep1ViewModel = new LoadNewDsStep1ViewModel();
        readonly public static AlgorithmBuilderViewModel _AlgorithmBuilderViewModel = new AlgorithmBuilderViewModel();
        readonly public static LoadNewDsStep2ViewModel _LoadNewDsStep2ViewModel = new LoadNewDsStep2ViewModel();
        readonly public static LoadNewDsStep3ViewModel _LoadNewDsStep3ViewModel = new LoadNewDsStep3ViewModel();
        readonly public static MainViewModel _MainViewModel = new MainViewModel(OverallFrame);
        readonly public static TreeViewFilterViewModel _FilterViewModel = new TreeViewFilterViewModel();
        readonly public static ViewAlgorithmViewModel _ViewAlgorithmViewModel = new ViewAlgorithmViewModel();
        readonly public static ImageViewerViewModel _ImageViewerViewModel = new ImageViewerViewModel();
        public static PostProcessingViewModel _PostProcessingViewModel;

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

        private static void _setupPostProcessor()
        {
            if( _PostProcessingViewModel == null )
            {
                IHandlerFactory f = GlobalContainer.Instance.Container.Resolve<IHandlerFactory>();
                PostProcessingStore s = new PostProcessingStore();
                s.AddOptions( "Single", new SingleHandlerOptions() );
                s.AddOptions( "Multiple", new MultiHandlerOptions() );
                _PostProcessingViewModel = new PostProcessingViewModel( f, s );
            }
            if (_ViewExistingDatasetViewModel == null && _frame != null)
            {
                _ViewExistingDatasetViewModel = new ViewExistingDatasetViewModel(_frame);
                _frame.Content = _ViewExistingDatasetViewModel;
            }
        } 
    }
}
