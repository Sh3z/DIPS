using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database.DicomHelper;

namespace DIPS.Tests.Database
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void Test_EncryptAndDecrypt()
        {
            Encryption test = new Encryption();
            String encrypted = null;
            String decrypted = null;
            String initial = "John";
            int addon = 123;

            encrypted = test.Encrypt(initial, addon);
            Assert.AreNotEqual(initial, encrypted);
            Assert.AreNotEqual(initial + addon.ToString(), encrypted);

            decrypted = test.Decrypt(encrypted, addon);
            Assert.AreNotEqual(initial + addon.ToString(), decrypted);
            Assert.AreEqual(initial, decrypted);
        }

    }
}
