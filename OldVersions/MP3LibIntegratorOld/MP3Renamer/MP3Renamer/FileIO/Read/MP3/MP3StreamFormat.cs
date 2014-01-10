using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using MP3Renamer.Helper;

namespace MP3Renamer.FileIO.Read.MP3
{
    public class MP3StreamFormat : StreamFormatBase
    {
        private string path;

        public MP3StreamFormat(string FullAbsolutePathWithExtension)
        {
            path = FullAbsolutePathWithExtension;
        }

        public void Open()
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            ExtractInformation(stream);
        }



        private void ExtractInformation(Stream stream)
        {
            MP3Frame frame = new MP3Frame();
            // der frame weiß wo header , body , und footer sind 
            frame.Read(stream);
            // ab jetzt sind body informationen verfügbar 

            // ab hier können daten in das restliche system zurück gegeben werden.

        }






   

    }
}