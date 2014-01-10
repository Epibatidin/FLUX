using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MP3Renamer.FileIO.WriteExecution
{
    public class FileHelper
    {
        public string From { get; private set; }
        public string To { get; private set; }


        public FileHelper(string _from, string _to, DirectoryInfo baseDir)
        {
            From = _from;
            int pos = _from.LastIndexOf('.');
            if(pos > 0)
            {
                string temp = _from.Substring(pos);

                To = Path.Combine(baseDir.FullName, _to) + temp;
            }
        }


    }
}