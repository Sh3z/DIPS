using Database;
using Database.Unity;
using DIPS.Unity;
using DIPS.Util.Commanding;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ConnectionSettingViewModel : BaseViewModel
    {
        #region Properties

        public string DataSource { set; get; }
        public string Catalog { set; get; }
        public string Security { set; get; }
        public string Extra { set; get; }

        private string _defaultConn;

        public string DefaultConnection
        {
            get { return _defaultConn; }
            set { _defaultConn = value;
            OnPropertyChanged();
            }
        }
        

        public ICommand ApplySettingCommand { get; set; }
        public ICommand CancelSettingSelection { get; set; }

        private IConnectionView _connectionView;

        public IConnectionView ConnectionView
        {
            get { return _connectionView; }
            set { _connectionView = value; }
        }

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

        private object _hideWindow;

        public object HideWindow
        {
            get { return _hideWindow; }
            set
            {
                _hideWindow = value;
                HideDialog(null);
            }
        }
        #endregion

        #region Constructor
        public ConnectionSettingViewModel()
        {
            DefaultValue();
            ConfigureCommands();
            SendParameters();
        }
        #endregion

        #region Methods

        private void DefaultValue()
        {
            DefaultConnection = ConnectionManager.getConnection;
            DataSource = ConnectionManager.DataSource;
            Catalog = ConnectionManager.Catalog;
            Security = ConnectionManager.Security;
            Extra = ConnectionManager.Extra;
        }

        private void ConfigureCommands()
        {
            ApplySettingCommand = new RelayCommand(new Action<object>(AssignSetting));
            CancelSettingSelection = new RelayCommand(new Action<object>(HideDialog));
        }

        public void HideDialog(object obj)
        {
            if (ConnectionView == null)
            {
                Container = GlobalContainer.Instance.Container;
                ConnectionView = Container.Resolve<IConnectionView>();
            }
            
            ConnectionView.HideDialog();
        }

        private void SendParameters()
        {
            if (ConnectionView != null)
            {
                ConnectionView.DataSource = DataSource;
                ConnectionView.Catalog = Catalog;
                ConnectionView.Security = Security;
                ConnectionView.Extra = Extra;
            }
            
        }
        
        private void AssignSetting(object obj)
        {
            if (ConnectionView == null)
            {
                 Container = GlobalContainer.Instance.Container;
                 ConnectionView = Container.Resolve<IConnectionView>();
            }
           
            if (ConnectionView != null)
            {
                SendParameters();
                ConnectionView.ApplySetting();
                DefaultConnection = ConnectionManager.getConnection;
            }
            
        } 
        #endregion
    }
}
