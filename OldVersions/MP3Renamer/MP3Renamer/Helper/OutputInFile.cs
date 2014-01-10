using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MP3Renamer.Models;
using MP3Renamer.Config;


namespace MP3Renamer.Helper
{
    public class OutputInFile
    {
        private string FilePath;


        FileStream LogFile;


        public OutputInFile()
        {
            FilePath = Configuration.TargetFolder + "/LogFile.txt";
        }

        private TextWriter GetWriteableFileStream()
        {
            FileStream F = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);

            TextWriter writer = new StreamWriter(F,System.Text.Encoding.ASCII);
            return writer;
        }


        public void WriteIGrouping<Key,Value>(IEnumerable<IGrouping<Key, Value>> Content)
        {
            // mach nen filestream auf 
            // schreib den content rein fertig 
            var writer = GetWriteableFileStream();

            foreach (var item in Content)
            {
                writer.Write(item.Key + " :"  );

                foreach (var grp in item)
                {
                    writer.WriteLine(grp);
                }
                writer.WriteLine();
            }
            writer.Flush();
            writer.Close();
        }


        public void WriteStringToFile(string Content)
        {
            var writer = GetWriteableFileStream();

            writer.Write(Content);

            writer.Flush();
            writer.Close();
        }



    }
}