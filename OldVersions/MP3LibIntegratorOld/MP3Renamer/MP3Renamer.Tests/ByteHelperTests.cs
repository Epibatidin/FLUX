using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Renamer.Helper;

namespace MP3Renamer.Tests
{
    /// <summary>
    /// Summary description for ByteHelperTests
    /// </summary>
    [TestClass]
    public class ByteHelperTests
    {
        [TestMethod]
        public void TestByteHelper()
        {
            byte[] input = new byte[] { 100, 75, 80 };

            string inputAsString = "dKP";

            string result = ByteHelper.ByteArrayToString(input);
            
            Assert.AreEqual(result, inputAsString);

        }


    }
}
