using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.ViewModel.UserInterfaceVM;

namespace DIPS.Tests.ViewModel
{
    [TestClass]
    public class LoadNewDatasetTests
    {
        [TestMethod]
        public void TestLoadNewStep1_Constructor()
        {
            LoadNewDsStep1ViewModel step1 = new LoadNewDsStep1ViewModel();
            Assert.IsNotNull(step1);
        }

        [TestMethod]
        public void TestLoadNewStep1_ClearList()
        {
            LoadNewDsStep1ViewModel step1 = new LoadNewDsStep1ViewModel();
            step1.ListOfFiles = new System.Collections.ObjectModel.ObservableCollection<System.IO.FileInfo>();
            step1.ListOfFiles.Add(new System.IO.FileInfo(null));

            step1.ListOfFiles.Clear();

            Assert.IsTrue(step1.ListOfFiles.Count == 0);
        }

        [TestMethod]
        public void TestLoadNewStep2_Constructor()
        {
            LoadNewDsStep2ViewModel step2 = new LoadNewDsStep2ViewModel();
            Assert.IsNotNull(step2);
            Assert.IsNotNull(step2.ListofTechniques);
            Assert.IsNotNull(step2.TechniqueAlgorithms);
        }

        [TestMethod]
        public void TestLoadNewStep3_Constructor()
        {
            LoadNewDsStep3ViewModel step3 = new LoadNewDsStep3ViewModel();
            Assert.IsNotNull(step3.PipelineAlgorithms);
            Assert.IsNotNull(step3.ListOfFiles);
        }


    }
}
