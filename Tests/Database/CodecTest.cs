using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database.DicomHelper;
using Database.Objects;

namespace DIPS.Tests.Database
{
    [TestClass]
    public class CodecTest
    {
        [TestMethod]
        public void Test_RegisterCodec()
        {
            Logger log = new Logger();
            log.start();
            Assert.AreEqual(true, Log.CodecRegistration);
        }
    }
}
