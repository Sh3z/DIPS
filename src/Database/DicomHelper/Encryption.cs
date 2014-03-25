using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Database.DicomHelper
{
    public class Encryption
    {
        //AES 256 bits
        private static byte[] key = { 210, 187, 19, 110, 57, 216, 115, 54, 14, 148, 207, 116, 27, 152, 242, 200, 142, 12, 185, 177, 184, 53, 196, 122, 24, 86, 117, 218, 131, 236, 77, 209 };
        private static byte[] vector = { 191, 74, 231, 204, 13, 35, 183, 219, 58, 111, 213, 162, 49, 162, 14, 176 };
        private UTF8Encoding encoder;
        private ICryptoTransform encryptor;
        private ICryptoTransform decryptor;

        public Encryption()
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            encryptor = rijndael.CreateEncryptor(key, vector);
            decryptor = rijndael.CreateDecryptor(key, vector);
            encoder = new UTF8Encoding();
        }

        protected byte[] Transform(byte[] bytes, ICryptoTransform transform)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
            cs.Write(bytes,0,bytes.Length);
            cs.FlushFinalBlock();
            ms.Position = 0;

            byte[] newValue = new byte[ms.Length];
            ms.Read(newValue, 0, newValue.Length);
            cs.Close();
            ms.Close();

            return newValue;
        }

        public String Encrypt(String value, int id)
        {
            value = value + id;
            byte[] newValue = Transform(encoder.GetBytes(value), encryptor);
            return Convert.ToBase64String(newValue);
        }

        public String Decrypt(String value, int id)
        {
            byte[] newValue = Transform(Convert.FromBase64String(value), decryptor);
            String decrypted = encoder.GetString(newValue);
            decrypted = decrypted.Substring(0, decrypted.Length - (id.ToString().Length));
            return decrypted;
        }
    }
}
