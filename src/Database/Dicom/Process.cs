using Database.Connection;
using Database.Dicom;
using Database.DicomHelper;
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
        public void processDicom()
        {
            if (DicomInfo.codecRegistration == false) registerCodec();
            Console.WriteLine(DicomInfo.readFile);

            verifyDicom verify = new verifyDicom();
            Boolean validDicom = verify.verify();

            if (validDicom == true)
            {
                DicomInfo.fileReadable = true;
                readDicom dicom = new readDicom();
                dicom.read();

                if (DicomInfo.fileReadable == true)
                {
                    SimpleEncryption AES = new SimpleEncryption();
                    DicomInfo.patientName = AES.Encrypt(DicomInfo.patientName);

                    CheckIfPatientExist();
                    InsertToDatabase database = new InsertToDatabase();
                    database.insert();
                }
            }
            else Console.WriteLine("Not a Valid DICOM file");
        }

        private void registerCodec()
        {
            Dicom.Codec.DcmRleCodec.Register();
            Dicom.Codec.Jpeg.DcmJpegCodec.Register();
            Dicom.Codec.Jpeg2000.DcmJpeg2000Codec.Register();
            Dicom.Codec.JpegLs.DcmJpegLsCodec.Register();
            DicomInfo.codecRegistration = true;
        }

        private void CheckIfPatientExist()
        {
            DAOGeneral dao = new DAOGeneral();
            dao.patientExist();

            if (DicomInfo.patientExist == false)
            {
                PatientID id = new PatientID();
                DicomInfo.pID = id.generate();
            }
            else if (DicomInfo.sameSeries == false)
            {
                dao.updatePatientSeries();
            }
            else if (DicomInfo.sameSeries == true)
            {
                dao.retrieveImageNumber();
            }
        }
    }
}
