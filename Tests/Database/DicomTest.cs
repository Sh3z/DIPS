using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Database;

namespace DIPS.Tests.Database
{
    [TestClass]
    public class DicomTest
    {
        [TestMethod]
        public void Test_HandleNullValues()
        {
            DicomInfo dicom = new DicomInfo();
            dicom.studyUID = null;
            dicom.seriesUID = null;
            dicom.imageUID = null;
            dicom.patientName = null;
            dicom.sex = null;
            dicom.pBday = null;
            dicom.age = null;
            dicom.imgNumber = null;
            dicom.modality = null;
            dicom.bodyPart = null;
            dicom.studyDesc = null;
            dicom.seriesDesc = null;
            dicom.sliceThickness = null;

            readDicom reader = new readDicom();
            reader.nullCheck(dicom);

            Assert.AreEqual(String.Empty, dicom.studyUID);
            Assert.AreEqual(String.Empty, dicom.seriesUID);
            Assert.AreEqual(String.Empty, dicom.imageUID);
            Assert.AreEqual(String.Empty, dicom.patientName);
            Assert.AreEqual(String.Empty, dicom.sex);
            Assert.AreEqual(String.Empty, dicom.pBday);
            Assert.AreEqual(String.Empty, dicom.age);
            Assert.AreEqual(String.Empty, dicom.imgNumber);
            Assert.AreEqual(String.Empty, dicom.modality);
            Assert.AreEqual(String.Empty, dicom.bodyPart);
            Assert.AreEqual(String.Empty, dicom.studyDesc);
            Assert.AreEqual(String.Empty, dicom.seriesDesc);
            Assert.AreEqual(String.Empty, dicom.sliceThickness);
        }

        [TestMethod]
        public void Test_HandleJoinName()
        {
            DicomInfo dicom = new DicomInfo();
            dicom.patientName = "John^Kenn";

            readDicom reader = new readDicom();
            reader.nullCheck(dicom);

            String expected = "John Kenn";
            Assert.AreEqual(expected, dicom.patientName);
        }

        [TestMethod]
        public void Test_HandleGenderInfo()
        {
            DicomInfo dicom = new DicomInfo();
            dicom.sex = "Female";

            readDicom reader = new readDicom();
            reader.nullCheck(dicom);
            Assert.AreEqual("F", dicom.sex);

            dicom.sex = "Male";
            reader.nullCheck(dicom);
            Assert.AreEqual("M", dicom.sex);
        }
    }
}
