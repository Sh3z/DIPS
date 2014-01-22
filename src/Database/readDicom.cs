
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
        String temp = null;
        Boolean dateNull = false;
        Boolean timeNull = false;
        DcmDate date = null;
        DcmTime time = null;

        public void read()
        {
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
                    temp = dff.Dataset.GetValueString(DicomTags.PatientsName);

                    staticVariables.sex = dff.Dataset.GetValueString(DicomTags.PatientsSex);
                    staticVariables.pBday = dff.Dataset.GetValueString(DicomTags.PatientsBirthDate);
                    staticVariables.age = dff.Dataset.GetValueString(DicomTags.PatientsAge);
                    staticVariables.imgNumber = dff.Dataset.GetValueString(DicomTags.InstanceNumber).PadLeft(2, '0');
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

        void nullCheck()
        {
            //temp = patient's name
            if (temp == null)
            {
                staticVariables.firstName = "NULL";
                staticVariables.lastName = "NULL";
            }
            else
            {
                String[] name = temp.Split('^');
                staticVariables.firstName = name[1];
                staticVariables.lastName = name[0];
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
            if (staticVariables.sex == null) staticVariables.sex = "N";
            if (staticVariables.pBday == "") staticVariables.pBday = "NULL";
            if (staticVariables.age == null) staticVariables.age = "NULL";
            if (staticVariables.imgNumber == "") staticVariables.imgNumber = "NULL";
            if (staticVariables.bodyPart == null) staticVariables.bodyPart = "NULL";
            if (staticVariables.studyDesc == "") staticVariables.studyDesc = "NULL";
            if (staticVariables.seriesDesc == null) staticVariables.seriesDesc = "NULL";
            if (staticVariables.sliceThickness == null) staticVariables.sliceThickness = "NULL";
        }
    }
}
