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
            if (Log.CodecRegistration == false)
            {
                registerCodec();
                Log.CodecRegistration = true;
            }

            verifyDicom verify = new verifyDicom();
            Boolean validDicom = verify.verify(filePath);

            if (validDicom == true)
            {
                dicom.fileReadable = true;
                readDicom reader = new readDicom();
                reader.read(dicom);

                if (dicom.fileReadable == true)
                {
                    SimpleEncryption AES = new SimpleEncryption();
                    dicom.patientName = AES.Encrypt(dicom.patientName);

                    CheckIfPatientExist(dicom);
                    InsertToDatabase database = new InsertToDatabase();
                    database.insert(dicom,filePath);
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
        }

        private void CheckIfPatientExist(DicomInfo dicom)
        {
            DAOGeneral dao = new DAOGeneral();
            dao.patientExist(dicom);

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
