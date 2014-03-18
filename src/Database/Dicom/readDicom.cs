
using Database;
using Dicom;
using Dicom.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class readDicom
    {
        String pName = null;
        Boolean dateNull = false;
        Boolean timeNull = false;
        DcmDate date = null;
        DcmTime time = null;

        public void read(DicomInfo dicom)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(dicom.readFile, FileMode.Open);
                ExtractMetaData(fs,dicom);
                nullCheck(dicom);
                fs.Close();
            }
            catch (Exception e)
            {
                fs.Close();
                dicom.fileReadable = false;
                Console.WriteLine("unable to read");
                Console.WriteLine(e);
            }
        }

        private void ExtractMetaData(FileStream fs,DicomInfo dicom)
        {
            DicomFileFormat dff = new DicomFileFormat();

            dff.Load(fs, DicomReadOptions.Default);
            pName = dff.Dataset.GetValueString(DicomTags.PatientsName);
            dicom.studyUID = dff.Dataset.GetValueString(DicomTags.StudyInstanceUID);
            dicom.seriesUID = dff.Dataset.GetValueString(DicomTags.SeriesInstanceUID);
            dicom.imageUID = dff.Dataset.GetValueString(DicomTags.SOPInstanceUID);
            dicom.sex = dff.Dataset.GetValueString(DicomTags.PatientsSex);
            dicom.pBday = dff.Dataset.GetValueString(DicomTags.PatientsBirthDate);
            dicom.age = dff.Dataset.GetValueString(DicomTags.PatientsAge);
            dicom.imgNumber = dff.Dataset.GetValueString(DicomTags.InstanceNumber);
            dicom.modality = dff.Dataset.GetValueString(DicomTags.Modality);
            dicom.bodyPart = dff.Dataset.GetValueString(DicomTags.BodyPartExamined);
            dicom.studyDesc = dff.Dataset.GetValueString(DicomTags.StudyDescription);
            dicom.seriesDesc = dff.Dataset.GetValueString(DicomTags.SeriesDescription);
            dicom.sliceThickness = dff.Dataset.GetValueString(DicomTags.SliceThickness);
        }

        private void nullCheck(DicomInfo dicom)
        {
            if (pName == null) dicom.patientName = String.Empty;
            else
            {
                try
                {
                    var splitName = new StringBuilder(pName);
                    splitName.Replace('^', ' ');
                    dicom.patientName = splitName.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    dicom.patientName = pName;
                }
            }

            if (dicom.studyUID == null) dicom.studyUID = String.Empty;
            if (dicom.seriesUID == null) dicom.seriesUID = String.Empty;
            if (dicom.imageUID == null) dicom.imageUID = String.Empty;
            if (dicom.sex == null || (dicom.sex[0] != 'M' && dicom.sex[0] != 'F')) dicom.sex = String.Empty;
            else dicom.sex = dicom.sex.Substring(0, 1);
            if (!String.IsNullOrEmpty(dicom.imgNumber)) dicom.imgNumber = dicom.imgNumber.PadLeft(2, '0');
            if (dicom.pBday == null) dicom.pBday = String.Empty;
            if (dicom.age == null) dicom.age = String.Empty;
            if (dicom.imgNumber == null) dicom.imgNumber = String.Empty;
            if (dicom.modality == null) dicom.modality = String.Empty;
            if (dicom.bodyPart == null) dicom.bodyPart = String.Empty;
            if (dicom.studyDesc == null) dicom.studyDesc = String.Empty;
            if (dicom.seriesDesc == null) dicom.seriesDesc = String.Empty;
            if (dicom.sliceThickness == null) dicom.sliceThickness = String.Empty;
        }
    }
}
