using DIPS.Processor.Client;
using DIPS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DIPS.UI.Pages
{
    /// <summary>
    /// Interaction logic for ViewAlgorithms.xaml
    /// </summary>
    public partial class ViewAlgorithms : Page
    {
        public ViewAlgorithms()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets or sets the <see cref="IProcessingService"/> connected to
        /// the active DIPS processor.
        /// </summary>
        public IProcessingService Service
        {
            get;
            set;
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CreateAlgorithm createAlgorithmScreen = new CreateAlgorithm();
            AlgorithmBuilderViewModel vm = new AlgorithmBuilderViewModel();
            foreach( var algorithm in Service.GetAlgorithmDefinitions() )
            {
                AlgorithmViewModel viewModel = new AlgorithmViewModel( algorithm );
                vm.AvailableAlgorithms.Add( viewModel );
            }

            createAlgorithmScreen.DataContext = vm;

            this.NavigationService.Navigate(createAlgorithmScreen);
        }
    }
}
