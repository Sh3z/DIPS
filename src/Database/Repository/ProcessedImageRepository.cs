using Database.Connection;
using Database.Objects;
using Dicom;
using Dicom.Data;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class ProcessedImageRepository
    {
        //extract identifier(UID) from file
        private String getIdentifier(FileStream fs)
        {
            String identifier = String.Empty;
            try
            {
                DicomFileFormat dff = new DicomFileFormat();
                dff.Load(fs, DicomReadOptions.Default);
                identifier = dff.Dataset.GetValueString(DicomTags.SOPInstanceUID);
            }
            catch (Exception e) { Console.WriteLine(e); }

            return identifier;
        }

        //Saves the processed image to the database
        public void saveImage(FileInfo file, byte[] blob)
        {
            String identifier = String.Empty;
            if (file != null) identifier = getIdentifier(file.OpenRead());

            if (ConnectionManager.ValidConnection == true)
            {
                ProcessRepository repo = new ProcessRepository();
                repo.processDicom(file.FullName);
                _saveImage(blob, identifier);
            }
        }


        private void _saveImage( byte[] blob, String identifier )
        {
            try
            {
                int seriesID = 0;
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertProcessedImages_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@processMethod", SqlDbType.VarChar).Value = Log.ProcessName;
                    cmd.Parameters.Add("@imageUID", SqlDbType.VarChar).Value = identifier;
                    cmd.Parameters.Add("@imageBlob", SqlDbType.VarBinary, blob.Length).Value = blob;
                    seriesID = (Int32)cmd.ExecuteScalar();
                }

                //update series "Last Modified Date" column
                DAOLog log = new DAOLog();
                log.update(seriesID);
            }
            catch { }
        }
    }
}
