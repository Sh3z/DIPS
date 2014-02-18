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
            DAOGeneral dao2 = new DAOGeneral();

            if (DicomInfo.patientExist == false)
            {
                if (DicomInfo.logCreated == false) dao2.createLog(); 
                dao.insertPatient();
                dao.insertName();
                dao.insertImageInfo();
                dao.insertImageFile();
            }
            else if (DicomInfo.sameSeries == false)
            {
                if (DicomInfo.logCreated == false) dao2.createLog(); 
                dao.insertImageInfo();
                dao.insertImageFile();
            }
            else if (DicomInfo.imageExist == false)
            {
                if (DicomInfo.logCreated == false) dao2.createLog(); 
                dao.insertImageFile();
            }

            DicomInfo.seriesID = 0;
            DicomInfo.patientExist = false;
            DicomInfo.imageExist = false;
            DicomInfo.sameSeries = false;
        }
    }
}
