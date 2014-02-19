
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

        public void read()
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(DicomInfo.readFile, FileMode.Open);
                ExtractMetaData(fs);
                nullCheck();
                fs.Close();
            }
            catch (Exception e)
            {
                fs.Close();
                DicomInfo.fileReadable = false;
                Console.WriteLine("unable to read");
                Console.WriteLine(e);
            }
        }

        private void ExtractMetaData(FileStream fs)
        {
            DicomFileFormat dff = new DicomFileFormat();

            dff.Load(fs, DicomReadOptions.Default);
            pName = dff.Dataset.GetValueString(DicomTags.PatientsName);

            DicomInfo.sex = dff.Dataset.GetValueString(DicomTags.PatientsSex);
            DicomInfo.pBday = dff.Dataset.GetValueString(DicomTags.PatientsBirthDate);
            DicomInfo.age = dff.Dataset.GetValueString(DicomTags.PatientsAge);
            DicomInfo.imgNumber = dff.Dataset.GetValueString(DicomTags.InstanceNumber);
            DicomInfo.modality = dff.Dataset.GetValueString(DicomTags.Modality);
            DicomInfo.bodyPart = dff.Dataset.GetValueString(DicomTags.BodyPartExamined);
            DicomInfo.studyDesc = dff.Dataset.GetValueString(DicomTags.StudyDescription);
            DicomInfo.seriesDesc = dff.Dataset.GetValueString(DicomTags.SeriesDescription);
            DicomInfo.sliceThickness = dff.Dataset.GetValueString(DicomTags.SliceThickness);

            if (dff.Dataset.GetValueString(DicomTags.AcquisitionDate) == "") dateNull = true;
            if (dff.Dataset.GetValueString(DicomTags.AcquisitionTime) == "") timeNull = true;
            date = dff.Dataset.GetDA(DicomTags.AcquisitionDate);
            time = dff.Dataset.GetTM(DicomTags.AcquisitionTime);
        }

        private void nullCheck()
        {
            if (pName == null) DicomInfo.patientName = String.Empty;
            else
            {
                try
                {
                    var splitName = new StringBuilder(pName);
                    splitName.Replace('^', ' ');
                    DicomInfo.patientName = splitName.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    DicomInfo.patientName = pName;
                }
            }

            if (dateNull == true && timeNull == true)
            {
                DateTime dt = DateTime.MinValue;
                DicomInfo.imgDateTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                System.DateTime imgDateTime = date.GetDateTime().Date + time.GetDateTime().TimeOfDay;
                DicomInfo.imgDateTime = imgDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (DicomInfo.sex == null || (DicomInfo.sex[0] != 'M' && DicomInfo.sex[0] != 'F')) DicomInfo.sex = String.Empty;
            else DicomInfo.sex = DicomInfo.sex.Substring(0, 1);
            if (!String.IsNullOrEmpty(DicomInfo.imgNumber)) DicomInfo.imgNumber = DicomInfo.imgNumber.PadLeft(2, '0');
            if (DicomInfo.pBday == null) DicomInfo.pBday = String.Empty;
            if (DicomInfo.age == null) DicomInfo.age = String.Empty;
            if (DicomInfo.imgNumber == null) DicomInfo.imgNumber = String.Empty;
            if (DicomInfo.modality == null) DicomInfo.modality = String.Empty;
            if (DicomInfo.bodyPart == null) DicomInfo.bodyPart = String.Empty;
            if (DicomInfo.studyDesc == null) DicomInfo.studyDesc = String.Empty;
            if (DicomInfo.seriesDesc == null) DicomInfo.seriesDesc = String.Empty;
            if (DicomInfo.sliceThickness == null) DicomInfo.sliceThickness = String.Empty;
        }
    }
}
