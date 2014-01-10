using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Renamer.FileIO.Read.MP3;

namespace MP3Renamer.Tests
{
    [TestClass]
    public class MP3TagReader
    {
        [TestMethod]
        public void blub()
        {
            MP3StreamFormat format = new MP3StreamFormat(@"F:\MP3Renamer\1.mp3");
            format.Open();
        }

        

      

        private bool[] ConvertByteToBits(byte input = 255)
        {
            bool[] result = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                byte s = (byte)(1 << i);
                var k = (input & (s)) == s;
                
                result[i] = k;
            }
            return result;
        }

    }
}
