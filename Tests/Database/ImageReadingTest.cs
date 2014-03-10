using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Database;

namespace DIPS.Tests.Database
{
    [TestClass]
    public class ImageReadingTest
    {
        [TestMethod]
        public void Test_RetrieveDicomImage()
        {
            readImage read = new readImage();
            Assert.IsNotNull(read.blob(@"C:\Bodrill\00020012"));
            Assert.IsNotNull(read.blob(@"C:\Bod\IM-0001-0012.dcm"));
        }
    }
}
