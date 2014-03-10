using Database.Connection;
using Database.Dicom;
using Database.DicomHelper;
using Database.Objects;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class Process
    {
        public void processDicom(DicomInfo dicom,String filePath)
        {
            verifyDicom verify = new verifyDicom();
            Boolean validDicom = verify.verify(filePath);

            if (validDicom == true)
            {
                dicom.fileReadable = true;
                readDicom reader = new readDicom();
                reader.read(dicom);

                if (dicom.fileReadable == true)
                {
                    CheckIfPatientExist(dicom);
                    InsertToDatabase database = new InsertToDatabase();
                    database.insert(dicom,filePath);
                }
            }
            else Console.WriteLine("Not a Valid DICOM file");
        }

        private void CheckIfPatientExist(DicomInfo dicom)
        {
            DAOGeneral dao = new DAOGeneral();
            if (dicom.studyUID.Length != 0 && dicom.seriesUID.Length != 0)
            {
                dao.patientExist(dicom);
                if (dicom.patientExist == true) dao.seriesExist(dicom);
            }
            else dao.patientExistBackup(dicom);

            if (dicom.patientExist == false)
            {
                PatientID id = new PatientID();
                dicom.pID = id.generate();
            }
            else if (dicom.sameSeries == false)
            {
                dao.updatePatientSeries(dicom);
            }
            else if (dicom.sameSeries == true)
            {
                dao.retrieveImageNumber(dicom);
            }
        }
    }
}
