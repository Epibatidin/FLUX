using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockUp.XMLItems;
using System.IO;

namespace Mockup.Tests
{
    [TestClass]
    public class FakeTagStreamTests
    {
        private void compareStreams(byte[] actual, params byte[] expected)
        {
            Assert.AreEqual(expected.Length,actual.Length);

            for (int i = 0; i < expected.Length; i++)
			{
                Assert.AreEqual(expected[i], actual[i]);			 
			}
        }

        private byte[] arrayByLength(byte value, int length)
        {
            var result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = value;
            }
            return result;
        }

        private byte[] arrayByValue(params byte[] data)
        {
            return data;
        }

        [TestMethod]
        public void ShouldReadAsOne()
        {
            var data = new TagData();
            data.ContentLength = 1;
            data.Begin = arrayByLength(1, 1);
            data.End = arrayByLength(3, 1);

            var stream = new TagFakeStream(data);

            byte[] result = new byte[3];
            stream.Read(result, 0, 3);

            compareStreams(result, 1, 0, 3);        
        }


        [TestMethod]
        public void MultipleSeekShouldNotChangeResult()
        {
            var data = new TagData();
            data.ContentLength = 1;
            data.Begin = arrayByValue(0,1,2,3,4);
            data.End = arrayByValue(5,6,7,8,9);
            var stream = new TagFakeStream(data);

            // lies erst 3 
            // seek in den nächsten bereich 
            // lies 
            // lies nochmal  
            var result = new byte[2];
            stream.Seek(3, SeekOrigin.Begin);
            stream.Read(result, 0, 2);
            compareStreams(result, 3, 4);

            result = new byte[3];
            stream.Seek(3, SeekOrigin.Begin);
            stream.Read(result, 0, 3);
            compareStreams(result, 3, 4, 0);
        }


    }
}
