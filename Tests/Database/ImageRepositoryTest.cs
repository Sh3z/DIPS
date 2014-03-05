using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;

namespace DIPS.Tests.Database
{
    [TestClass]
    public class ImageRepositoryTest
    {
        [TestMethod]
        public void Test_RetrieveImageProp()
        {
            ImageRepository image = new ImageRepository();
            Assert.IsNotNull(image.retrieveImageProperties("1308"));
        }
    }
}
