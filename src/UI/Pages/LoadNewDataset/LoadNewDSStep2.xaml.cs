using System;
using System.IO;
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
using DIPS.UI.Pages.LoadNewDataset;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using DIPS.Database.Objects;

namespace DIPS.UI.Pages.LoadNewDataset
{
    /// <summary>
    /// Interaction logic for LoadNewDSStep2.xaml
    /// </summary>
    public partial class LoadNewDSStep2 : Page
    {
        private List<FileInfo> _listOfFiles;

        public List<FileInfo> ListofFiles
        {
            get { return _listOfFiles; }
            set { _listOfFiles = value; }
        }

        public ObservableCollection<Technique> ListofTechniques
        {
            get { return (ObservableCollection<Technique>)GetValue(listOfTechniquesProperty); }
            set { SetValue(listOfTechniquesProperty, value); }
        }

        public static readonly DependencyProperty listOfTechniquesProperty =
           DependencyProperty.Register("ListofTechniques",
           typeof(ObservableCollection<Technique>), typeof(Page), new UIPropertyMetadata(null));

        public ObservableCollection<Technique> selectedTechniques
        {
            get { return (ObservableCollection<Technique>)GetValue(selectedTechniquesProperty); }
            set { SetValue(selectedTechniquesProperty, value); }
        }

        public static readonly DependencyProperty selectedTechniquesProperty = 
            DependencyProperty.Register("selectedTechniques",
            typeof(ObservableCollection<Technique>), typeof(Page), new UIPropertyMetadata(null));


        public LoadNewDSStep2()
        {
            InitializeComponent();

        }

        private void btnSelection_Click(object sender, RoutedEventArgs e)
        {
            LoadNewDSStep3 loadDS3 = new LoadNewDSStep3();
            this.NavigationService.Navigate(loadDS3);
        }

        public void LoadLists()
        {
            lstSelectedFiles.ItemsSource = ListofFiles;
            lstAvailableTech.ItemsSource = ListofTechniques;
            lstSelectedTechniques.ItemsSource = selectedTechniques;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.selectedTechniques = new ObservableCollection<Technique>();
            ListofTechniques = new ObservableCollection<Technique>();
            
            LoadTechniqueObjects();
            LoadLists();
        }

        private void LoadTechniqueObjects()
        {
            Technique tech1 = new Technique();
            Technique tech2 = new Technique();
            Technique tech3 = new Technique();
            Technique tech4 = new Technique();

            tech1.ID = 1;
            tech2.ID = 2;
            tech3.ID = 3;
            tech4.ID = 4;

            tech1.Name = "Blurring";
            tech2.Name = "Shading";
            tech3.Name = "Fuzzy";
            tech4.Name = "Whitening";

            ListofTechniques.Add(tech1);
            ListofTechniques.Add(tech2);
            ListofTechniques.Add(tech3);
            ListofTechniques.Add(tech4);
        }

        private void btnSelectTech_Click(object sender, RoutedEventArgs e)
        {
            passToSelectedTech();
        }

        private void passToSelectedTech()
        {
            if (lstAvailableTech.Items.Count > 0)
            {
                Technique tempTech = new Technique();
                
                tempTech = (Technique)lstAvailableTech.SelectedItem;

                ListofTechniques.Remove((Technique)lstAvailableTech.SelectedItem);
                selectedTechniques.Add(tempTech);
            }

        }

        private void passToInactiveTech()
        {
            if (lstSelectedTechniques.Items.Count > 0)
            {
                Technique tempTech = new Technique();
                
                tempTech = (Technique)lstSelectedTechniques.SelectedItem;

                ListofTechniques.Add(tempTech);
                selectedTechniques.Remove((Technique)lstSelectedTechniques.SelectedItem);
            }
        }

        private void btnDeselectTech_Click(object sender, RoutedEventArgs e)
        {
            passToInactiveTech();
        }
    }
}
