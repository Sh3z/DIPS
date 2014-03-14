﻿using Database.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public static class SelectedImage
    {
        private static ObservableCollection<Algorithm> _algoCollection;

        public static ObservableCollection<Algorithm> AlgorithmCollection
        {
            get { return _algoCollection; }
            set { _algoCollection = value; }
        }

        private static String _selectedImageUID;

        public static String UID
        {
            get { return _selectedImageUID; }
            set { _selectedImageUID = value; }
        }
        

        private static String _imageNumberSelected;

        public static String ImageNumberSelected
        {
            get { return _imageNumberSelected; }
            set { _imageNumberSelected = value; }
        }

        private static String _algorithmSelected;

        public static String AlgorithmSelected
        {
            get { return _algorithmSelected; }
            set { _algorithmSelected = value; }
        }

        public static byte[] updateProcessedImage()
        {
            SelectedImageRepository sir = new SelectedImageRepository();
            return sir.getProcessedImage(_imageNumberSelected, _algorithmSelected);
        }
        
    }
}
