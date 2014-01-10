using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MP3Renamer.FileIO.Read.MP3
{
    public class MP3Frame
    {
        public List<string> Information { get; private set; }

        public Header Header { get; private set; }
        public Body Body { get; private set; }
        public Footer Footer { get; private set; }

        public long DataStart { get; private set; }
        public long DataEnd { get; private set; }

        public void Read(Stream stream)
        {
            
            // der frame mus auch das ende der daten kennen ;
            MP3Pattern pattern = new MP3Pattern();
            // der frame muss wissen wo der Header sitzt
            Header = new Header(pattern);
            DataStart = Header.Read(stream);

            Footer = new Footer();
            DataEnd = Footer.Read(stream);
        }


        private long getEndPos(Stream inStream)
        {
            //byte[] header = new byte[3];
            //inStream.Seek(-128, SeekOrigin.End);

            //inStream.Read(header, 0, 3);

            //if (Encoding.Default.GetString(header).Equals("TAG"))
            //{
            //    // dann muss ich die letzten 128 byte leider wegwerfen
            //    return inStream.Length - 128;
            //    // naja vlt später auch auswerten und dann wegwerfen 
            //}
            //else return inStream.Length;
            return 0;
        }

        



        //public void Content(string input)
        //{
        //    if (headerTyp != "TCON" && headerTyp != "TLEN") content = input;
        //}




    }
}