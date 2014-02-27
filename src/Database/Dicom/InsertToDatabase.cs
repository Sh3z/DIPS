using Database.Objects;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection
{
    public class InsertToDatabase
    {
        public void insert(DicomInfo dicom, String filePath)
        {
            DAOInsertPatient dao = new DAOInsertPatient();
            DAOLog log = new DAOLog();

            if (dicom.patientExist == false)
            {
                if (Log.Created == false) log.create();
                if (Log.NeedUpdate == true) log.update(dicom.seriesID);
                dao.insertPatient(dicom);
                dao.insertName(dicom);
                dao.insertImageInfo(dicom);
                dao.insertImageFile(dicom,filePath);
            }
            else if (dicom.sameSeries == false)
            {
                if (Log.Created == false) log.create();
                if (Log.NeedUpdate == true) log.update(dicom.seriesID);
                dao.insertImageInfo(dicom);
                dao.insertImageFile(dicom,filePath);
            }
            else if (dicom.imageExist == false)
            {
                if (Log.Created == false) log.create();
                dao.insertImageFile(dicom,filePath);
            }
        }
    }
}
