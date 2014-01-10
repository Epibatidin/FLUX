using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Collections;

using DFRenamer.FileAccess;

namespace DFRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool DoIT = true;
            //bool DoIT = false;

            string path = @"G:\TESTBENCH";

            /*string[] artistFolder = Directory.GetDirectories(path);
            int folderCount = artistFolder.Length;
            ArrayList factories = new ArrayList();
            for(int i = 0; i< folderCount; i++)
            {
                FileFactory factory = new FileFactory(artistFolder[i], "");
                factories.Add(factory);
            }*/
            FileFactory factory = new FileFactory(path, "");

            //foreach (FileFactory ff in factories)
            //{
            //    ff.gatherInformation();               
            //}
            System.Console.Read();
        }
    }
}
