using DIPS.UI.Pages;
using DIPS.ViewModel;
using DIPS.Database;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace DIPS.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class ViewExistingDataset : Window
    {
        //declarion of properties
        private Navigation windowNav;
        //getters and setters
        public Navigation WindowNav
        {
            get
            {
                return windowNav;
            }
            set
            {
                windowNav = value;
            }
        }

        IFemoreViewModel viewModel = null;
        /// <summary>
        /// Initializes a new <see cref="ViewExistingDataset"/> using the provided <see cref="IFemoreViewModel"/>
        /// as its presentation logic.
        /// </summary>
        /// <param name="vm">The presentation layer-level view-model.</param>
        public ViewExistingDataset(IFemoreViewModel vm)
        {
            InitializeComponent();

            loadDicom();
            generateTreeView();
            viewModel = vm;
            
            // Set the Data Context as the view-model.
            DataContext = vm;

            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"Bitmaps|*.bmp|Jpgs|*.jpg";
            bool? result = dialog.ShowDialog();
            
            if( result.HasValue && result.Value )
            {
                Bitmap theBmp = new Bitmap( dialog.FileName );
                vm.ImageToProcess = theBmp;
            }
            else
            {
                Close();
            }*/
        }

        public void loadDicom()
        {
            readDicom dicom = new readDicom();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string[] files = Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories);
            System.Windows.Forms.MessageBox.Show(files.Length + " Files to Process");

            foreach (String s in files)
            {
                staticVariables.readFile = s;
                dicom.read();
            }
            System.Windows.Forms.MessageBox.Show("Complete");
        }

        public void generateTreeView()
        {
            try
            {
                SqlConnection con = new SqlConnection(staticVariables.sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("spr_TreeView_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();

                List<String> imgNumber = new List<String>();
                TreeViewItem item = new TreeViewItem();
                string currentID;
                string temp = null;
                int count = 0;

                while (data.Read())
                {
                    currentID = data.GetString(0);
                    if (count == 0) temp = currentID;
                    if (!currentID.Equals(temp))
                    {
                        item.Header = temp;
                        item.ItemsSource = imgNumber;
                        treeView.Items.Add(item);
                        
                        imgNumber = null;
                        item = null;
                        imgNumber = new List<String>();
                        item = new TreeViewItem();
                        temp = currentID;
                    }

                    imgNumber.Add(data.GetString(1));
                    count++;
                }
                data.Close();
                con.Close();
            }
            catch (Exception e) { }
        }

        public void setImage(String fileID)
        {
            SqlConnection con = new SqlConnection(staticVariables.sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("spr_RetrieveImage_v001", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = fileID;
            byte[] image = (byte[])cmd.ExecuteScalar();

            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap theBmp = (Bitmap)tc.ConvertFrom(image);
            viewModel.ImageToProcess = theBmp;
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var Text = e.NewValue.ToString();
                setImage(Text);
            }
            catch (Exception e2) { }
        }
        /// <summary>
        /// Contains the reference to the view-model.
        /// </summary>
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IFemoreViewModel _viewModel;
    }
}
