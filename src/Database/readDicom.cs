
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
            if (staticVariables.codecRegistration == false) registerCodec();
            Console.WriteLine(staticVariables.readFile);
            verifyDicom verify = new verifyDicom();
            
            Boolean validDicom = verify.verify();
            if (validDicom == true)
            {
                checkExist check = new checkExist();
                storedProcedure spr = new storedProcedure();
                staticVariables.patientExist = false;

                FileStream fs = new FileStream(staticVariables.readFile, FileMode.Open);
                DicomFileFormat dff = new DicomFileFormat();

                try
                {
                    dff.Load(fs, DicomReadOptions.Default);
                    pName = dff.Dataset.GetValueString(DicomTags.PatientsName);

                    staticVariables.sex = dff.Dataset.GetValueString(DicomTags.PatientsSex);
                    staticVariables.pBday = dff.Dataset.GetValueString(DicomTags.PatientsBirthDate);
                    staticVariables.age = dff.Dataset.GetValueString(DicomTags.PatientsAge);
                    staticVariables.imgNumber = dff.Dataset.GetValueString(DicomTags.InstanceNumber).PadLeft(2, '0');
                    staticVariables.modality = dff.Dataset.GetValueString(DicomTags.Modality);
                    staticVariables.bodyPart = dff.Dataset.GetValueString(DicomTags.BodyPartExamined);
                    staticVariables.studyDesc = dff.Dataset.GetValueString(DicomTags.StudyDescription);
                    staticVariables.seriesDesc = dff.Dataset.GetValueString(DicomTags.SeriesDescription);
                    staticVariables.sliceThickness = dff.Dataset.GetValueString(DicomTags.SliceThickness);

                    if (dff.Dataset.GetValueString(DicomTags.AcquisitionDate) == "") dateNull = true;
                    if (dff.Dataset.GetValueString(DicomTags.AcquisitionTime) == "") timeNull = true;
                    date = dff.Dataset.GetDA(DicomTags.AcquisitionDate);
                    time = dff.Dataset.GetTM(DicomTags.AcquisitionTime);

                    nullCheck();
                    check.retriveDatabase();
                    spr.insert();

                }
                catch (Exception e)
                {
                    fs.Close();
                    Console.WriteLine("unable to read");
                    Console.WriteLine("");
                }
                fs.Close();
            }
            else Console.WriteLine("Not a Valid DICOM file");
        }

        void registerCodec()
        {
            Dicom.Codec.DcmRleCodec.Register();
            Dicom.Codec.Jpeg.DcmJpegCodec.Register();
            Dicom.Codec.Jpeg2000.DcmJpeg2000Codec.Register();
            Dicom.Codec.JpegLs.DcmJpegLsCodec.Register();
            staticVariables.codecRegistration = true;
        }

        void nullCheck()
        {
            if (pName == null) staticVariables.patientName = "";
            else
            {
                try
                {
                    var splitName = new StringBuilder(pName);
                    splitName.Replace('^', ' ');
                    staticVariables.patientName = splitName.ToString();
                }
                catch (Exception e)
                {
                    staticVariables.patientName = pName;
                }
            }

            if (dateNull == true && timeNull == true)
            {
                DateTime dt = DateTime.MinValue;
                staticVariables.imgDateTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                System.DateTime imgDateTime = date.GetDateTime().Date + time.GetDateTime().TimeOfDay;
                staticVariables.imgDateTime = imgDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (staticVariables.sex == null) staticVariables.sex = "";
            if (staticVariables.pBday == null) staticVariables.pBday = "";
            if (staticVariables.age == null) staticVariables.age = "";
            if (staticVariables.imgNumber == null) staticVariables.imgNumber = "";
            if (staticVariables.modality == null) staticVariables.modality = "";
            if (staticVariables.bodyPart == null) staticVariables.bodyPart = "";
            if (staticVariables.studyDesc == null) staticVariables.studyDesc = "";
            if (staticVariables.seriesDesc == null) staticVariables.seriesDesc = "";
            if (staticVariables.sliceThickness == null) staticVariables.sliceThickness = "";
        }
    }
}
