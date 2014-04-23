using System;
using System.Collections.Generic;
using Database.Unity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.UI.Pages;
using System.Windows;
using Database;

namespace DIPS.UI.Unity.Implementations
{
    public class ConnectionView : IConnectionView
    {
        #region Properties
        public string DataSource { set; private get; }
        public string Catalog { set; private get; }
        public string Security { set; private get; }
        public string Extra { set; private get; }
        private ConnectionSetting ConnectionWindow { set; get; }
   
        #endregion

        #region Methods
        public void OpenDialog()
        {
            if (ConnectionWindow == null || ConnectionWindow.IsActive == false)
            {
                ConnectionWindow = new ConnectionSetting();
            }

            if (ConnectionWindow.Visibility == Visibility.Hidden)
            {
                ConnectionWindow.Visibility = Visibility.Visible;
            }
            else
            {
                ConnectionWindow.ShowDialog();
            }
        }

        public void ApplySetting()
        {
            ConnectionManager.DataSource = DataSource;
            ConnectionManager.Catalog = Catalog;
            ConnectionManager.Security = Security;
            ConnectionManager.Extra = Extra;
        }

        public void HideDialog()
        {
            if (ConnectionWindow != null)
            {
                ConnectionWindow.Hide();
            }
        }
        #endregion
    }
}
