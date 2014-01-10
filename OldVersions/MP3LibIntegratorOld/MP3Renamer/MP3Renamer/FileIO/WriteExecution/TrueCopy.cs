using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MP3Renamer.FileIO.WriteExecution
{
    public class TrueCopy : CopyHelperBase
    {
        public override void Copy(string From, string To)
        {
            int size = 1024;
            byte[] buffer = new byte[size];
            int bytesRead;
            using (FileStream source = new FileStream(From, FileMode.Open, FileAccess.Read))
            {
                using (FileStream target = new FileStream(To, FileMode.Create, FileAccess.Write))
                {
                    while (true)
                    {
                        bytesRead = source.Read(buffer, 0, size);
                        if (bytesRead == 0)
                            break;
                        target.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public override string SuperRootFolder
        {
            get
            {
                return Config.Configuration.TargetFolder;
            }
        }
    }
}