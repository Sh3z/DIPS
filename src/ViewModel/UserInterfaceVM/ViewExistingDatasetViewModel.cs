using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Database;
using Database.Unity;
using DIPS.Database.Objects;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;
using Database.Repository;
using DIPS.ViewModel.Unity;
using DIPS.Util.Commanding;
using DIPS.Processor.Client;
using System.Windows.Controls;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using Database.Connection;
using Database.Objects;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewExistingDatasetViewModel :BaseViewModel, IPipelineInfo
    {

        #region Properties

        private Technique _listViewItemAlgorithm;

        public Technique ListViewItemAlgorithm
        {
            get { return _listViewItemAlgorithm; }
            set { 
                _listViewItemAlgorithm = value;
                OnPropertyChanged();
                
                if (_listViewItemAlgorithm != null)
                {
                    SelectedImage.AlgorithmSelected = _listViewItemAlgorithm.ID;
                }
                
                setImage();
                _updateAlgorithmsInTechnique(value);
                }
        }

        private ObservableCollection<Patient> _PatientsList;

        private ObservableCollection<Technique> _listOfAlgorithms;

        public ObservableCollection<Technique> ListOfAlgorithms
        {
            get { return _listOfAlgorithms; }
            set 
                { 
                  _listOfAlgorithms = value;
                  OnPropertyChanged();
                }
        }

        private ObservableCollection<AlgorithmViewModel> _techniqueAlgorithms;

        public ObservableCollection<AlgorithmViewModel> TechniqueAlgorithms
        {
            get { return _techniqueAlgorithms; }
            set
            {
                _techniqueAlgorithms = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Patient> PatientsList
        {
            get { return _PatientsList; }
            set
            {
                _PatientsList = value;
                OnPropertyChanged();
            }
        }
        private IProcessingService _service;
        public IProcessingService Service
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
            }
        }

        private ObservableCollection<ImageProperties> _propertiesList;

        public ObservableCollection<ImageProperties> PropertiesList
        {
            get { return _propertiesList; }
            set { _propertiesList = value; }
        }

        public TreeViewGroupPatientsViewModel TopLevelViewModel
        {
            get { return _topLevel; }
            set
            {
                _topLevel = value;
                OnPropertyChanged();
            }
        }
        private TreeViewGroupPatientsViewModel _topLevel;

        private BitmapImage _imgUnprocessed;

        public BitmapImage ImgUnprocessed
        {
            get { return _imgUnprocessed; }
            set
            {
                _imgUnprocessed = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _imgProcessed;

        public BitmapImage ImgProcessed
        {
            get { return _imgProcessed; }
            set
            {
                _imgProcessed = value;
                OnPropertyChanged();
            }
        }

        private String _imageInfo;

        public String ImageInfo
        {
            get { return _imageInfo; }
            set
            {
                _imageInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFilterDialogCommand { get; set; }
        public ICommand RefreshTreeviewCommand { get; set; }
        public ICommand ViewLargerImageProcessedCommand { get; set; }
        public ICommand ViewLargerImageUnProcessedCommand { get; set; }
        public UnityCommand OpenQueueCommand { get; set; }

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }
        private IUnityContainer _container;

        private Boolean _isSelected;

        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                TreeViewPatientViewModel tvpv = new TreeViewPatientViewModel(null);
                _isSelected = value;
                OnPropertyChanged();
                Boolean transform = true;
                AdminRepository admin = new AdminRepository();
                if (_isSelected == true) transform = admin.verified();

                if (transform) GetPatientsForTreeview(null);
                else
                {
                    _isSelected = false;
                    OnPropertyChanged();
                }
            }
        }

        private Boolean _nameFilterSelected;

        public Boolean NameFilterSelected
        {
            get { return _nameFilterSelected; }
            set
            {
                _nameFilterSelected = value;
                
            }
        }
        
        private IFilterTreeView _filterImageView;

        public IFilterTreeView FilterTreeView
        {
            get { return _filterImageView; }
            set { _filterImageView = value; }
        }

        private IImageView _imageView;
        public IImageView ImageView
        {
            get { return _imageView; }
            set { _imageView = value; }
        }

        private IQueueDialog _queueDialog;

        public IQueueDialog QueueDialog
        {
            get { return _queueDialog; }
            set { _queueDialog = value; }
        }
        

        private Boolean _toggleFilter;

        public Boolean ToggleFilter
        {
            get { return _toggleFilter; }
            set
            {
                _toggleFilter = value;
                OnPropertyChanged();

                if (_toggleFilter == true)
                {
                    if (FilterTreeView != null)
                    {
                        GetPatientsFiltered();
                    }
                }
                else
                {
                    GetPatientsForTreeview(null);
                }
            }
        }

        ObservableCollection<AlgorithmViewModel> IPipelineInfo.SelectedProcesses
        {
            get { return TechniqueAlgorithms; }
        }
        #endregion

        #region Constructor
        public ViewExistingDatasetViewModel(Frame theFrame)
        {
            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            GlobalContainer.Instance.Container.RegisterInstance<IJobTracker>(vm);

            Container = GlobalContainer.Instance.Container;

            ValidateConnection.validateConnection();
            
            SetupCommands();
            GetPatientsForTreeview(null);

            ImageInfo = "Please Select An Image To View Image Information Here.";
        } 
        #endregion

        #region Methods
        private void SetupCommands()
        {
            OpenFilterDialogCommand = new RelayCommand(new Action<object>(OpenFilterDialog));
            ViewLargerImageProcessedCommand = new RelayCommand(new Action<object>(OpenLargerImageDialogProcessed));
            ViewLargerImageUnProcessedCommand = new RelayCommand(new Action<object>(OpenLargerImageDialogUnProcessed));
            RefreshTreeviewCommand = new RelayCommand(new Action<object>(GetPatientsForTreeview));
            OpenQueueCommand = new PresentQueueCommand();
            OpenQueueCommand.Container = GlobalContainer.Instance.Container;
        }

        private void OpenFilterDialog(object obj)
        {

            if (FilterTreeView == null)
            {
                Container = GlobalContainer.Instance.Container;
                FilterTreeView = Container.Resolve<IFilterTreeView>();
            }
           
            FilterTreeView.OpenDialog();
        }

        private void OpenLargerImageDialogProcessed(object obj)
        {
            if (ImageView == null)
            {
                Container = GlobalContainer.Instance.Container;
                ImageView = Container.Resolve<IImageView>();
            }
            BaseViewModel._ImageViewerViewModel.Container = Container;
            BaseViewModel._ImageViewerViewModel.SelectedImage = ImgProcessed;
            //ImageView.SelectedImage = ImgProcessed;
            ImageView.OpenDialog(ImgProcessed);
        }

        private void OpenLargerImageDialogUnProcessed(object obj)
        {
            if (ImageView == null)
            {
                Container = GlobalContainer.Instance.Container;
                ImageView = Container.Resolve<IImageView>();
            }

            BaseViewModel._ImageViewerViewModel.Container = Container;
            BaseViewModel._ImageViewerViewModel.SelectedImage = ImgUnprocessed;
            //ImageView.SelectedImage = ImgUnprocessed;
            ImageView.OpenDialog(ImgUnprocessed);
        }

        private void GetPatientsForTreeview(object obj)
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = repo.generateTreeView(_isSelected);
            TreeViewGroupPatientsViewModel tvpv = new TreeViewGroupPatientsViewModel(PatientsList);

            ImgUnprocessed = null;
            ImgProcessed = null;
            ImageInfo = "Please Select An Image To View Image Information Here.";
            
            TopLevelViewModel = tvpv;
        }

        private void GetPatientsFiltered()
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = FilterTreeView.ApplyFilter();
            TreeViewGroupPatientsViewModel tvpv = new TreeViewGroupPatientsViewModel(PatientsList);

            TopLevelViewModel = tvpv;
        }

        public void setImage()
        {
            
            byte[] processed = SelectedImage.updateProcessedImage();
            if (processed != null)
            {
                BitmapImage processedBmp = ToImage(processed);
                BaseProcessedImage = processedBmp;
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

        private void _updateAlgorithmsInTechnique(Technique value)
        {
            if (TechniqueAlgorithms != null)
            {
                TechniqueAlgorithms.Clear();
            }
            else
            {
                TechniqueAlgorithms = new ObservableCollection<AlgorithmViewModel>();
            }
            
            if (value != null)
            {
                IUnityContainer c = GlobalContainer.Instance.Container;
                IPipelineManager manager = c.Resolve<IPipelineManager>();

                var restoredPipeline = manager.RestorePipeline(value.xml);

                if (TechniqueAlgorithms != null)
                {
                    TechniqueAlgorithms.Clear();
                }

                foreach (var process in restoredPipeline)
                {
                    if (process != null)
                    {
                             AlgorithmViewModel algVM = new AlgorithmViewModel(process);
                             algVM.IsRemovable = false;
                             TechniqueAlgorithms.Add(algVM);
                    }
                   
                }
            }
        } 
        
        #endregion



        public string PipelineName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string PipelineID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
