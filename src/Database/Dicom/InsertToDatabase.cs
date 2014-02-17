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
        public void insert()
        {
            DAOInsertPatient dao = new DAOInsertPatient();

            if (DicomInfo.patientExist == false)
            {
                dao.insertPatient();
                dao.insertName();
                dao.insertImageInfo();
                dao.insertImageFile();
            }
            else if (DicomInfo.sameSeries == false)
            {
                dao.insertImageInfo();
                dao.insertImageFile();
            }
            else if (DicomInfo.imageExist == false)
            {
                dao.insertImageFile();
            }

            DicomInfo.seriesID = 0;
            DicomInfo.patientExist = false;
            DicomInfo.imageExist = false;
            DicomInfo.sameSeries = false;
        }
    }
}
