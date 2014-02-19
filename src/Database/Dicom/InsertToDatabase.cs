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
            DAOLog log = new DAOLog();

            if (DicomInfo.patientExist == false)
            {
                if (DicomInfo.logCreated == false) log.create();
                if (DicomInfo.logNeedUpdate == true) log.update();
                dao.insertPatient();
                dao.insertName();
                dao.insertImageInfo();
                dao.insertImageFile();
            }
            else if (DicomInfo.sameSeries == false)
            {
                if (DicomInfo.logCreated == false) log.create();
                if (DicomInfo.logNeedUpdate == true) log.update();
                dao.insertImageInfo();
                dao.insertImageFile();
            }
            else if (DicomInfo.imageExist == false)
            {
                if (DicomInfo.logCreated == false) log.create();
                dao.insertImageFile();
            }

            DicomInfo.seriesID = 0;
            DicomInfo.patientExist = false;
            DicomInfo.imageExist = false;
            DicomInfo.sameSeries = false;
        }
    }
}
