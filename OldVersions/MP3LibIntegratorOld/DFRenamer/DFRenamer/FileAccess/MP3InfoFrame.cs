using System;
using System.Collections.Generic;
using System.Text;

namespace DFRenamer.FileAccess
{
    class MP3InfoFrame
    {
        private string content = "";
        private string headerTyp;
        private int contentIntSize;

        private byte[] endian = new byte[3];
        //private byte[] getByteContent;

        Encoding Encoder = null; 


        public MP3InfoFrame(string headerTyp)
        {
            this.headerTyp = headerTyp;
            Encoder = setOutputEncodingTyp();
        }

        private Encoding setOutputEncodingTyp()
        {
            if (headerTyp == "TABL" || headerTyp == "TCOM" || headerTyp == "TIT2")
            {
                return Encoding.GetEncoding("ISO-8859-1");
            }
            else
                return Encoding.Unicode;  
        }

        public bool isValid()
        {
            if (content != "")
            {
                checkTypSpecifics();
            }
            return false;
        }


        

        internal void WriteFrameToStream(System.IO.FileStream outStream)
        {
            // typ
            byte[] typ = Encoding.ASCII.GetBytes(headerTyp);
            byte[] dummy = new byte[1];
            outStream.Write(typ, 0, typ.Length);
            System.Console.WriteLine(content);
            // size
            byte[] byteSize = BitConverter.GetBytes(contentIntSize);
            for (int i = byteSize.Length - 1; i >= 0; i--)
            {
                dummy[0] = byteSize[i];
                outStream.Write(dummy, 0, 1);
            }

            // flags
            dummy[0] = 0;
            outStream.Write(dummy, 0, 1);
            outStream.Write(dummy, 0, 1);

            // encoding
            if (Encoder == Encoding.Unicode)
            {
                endian[0] = 1; endian[1] = 255; endian[2] = 254;
                outStream.Write(endian, 0, 3);
            }
            else
            {
                endian[0] = 0;
                outStream.Write(endian, 0, 1);
            } 
            // content
            byte[] con = Encoder.GetBytes(content);
            outStream.Write(con,0,con.Length);
        }


        internal int getFullFrameSize()
        {
            // header ist immer 10 byte groß + das encoding signal byte 
            contentIntSize = Encoder.GetByteCount(content);
            if (Encoder == Encoding.Unicode)
                contentIntSize += 3; // bei unicode wegen endian 2 extra
            else
                contentIntSize += 1;

            return contentIntSize + 10;           
        }




        // typ
        // size
        // flags
        // content
        /*internal void Write(System.IO.FileStream outStream)
        {
            byte[] frameTyp = new byte[4];
            byte[] FrameContent = Encoding.UTF8.GetBytes(content);
            byte[] dummy = new byte[1];
            Int32 size = Encoding.UTF8.GetByteCount(content);

            byte[] byteSize = BitConverter.GetBytes(size);
            System.Console.WriteLine(content);
            foreach (var mh in byteSize)
            {
                System.Console.WriteLine(size + "Int as Byte " + mh);
            }

            
            frameTyp = Encoding.Default.GetBytes(headerTyp);

            
            outStream.Write(frameTyp, 0, frameTyp.Length);
            for (int i = byteSize.Length-1; i >= 0; i--)
            {
                dummy[0] = byteSize[i];
                outStream.Write(dummy, 0, 1);
            }
            dummy[0] = 0;
            outStream.Write(dummy, 0 , 1);
            outStream.Write(dummy, 0, 1);

            outStream.Write(FrameContent, 0, size);
        }*/


        private bool checkTypSpecifics()
        {
            switch (headerTyp)
            {
                case "TRCK":
                    short trck = checkInteger();
                    if (trck > -1)
                    {
                        // track muss zweistellig sein
                        if (trck < 100)
                        {
                            return true;
                        }
                    }
                    break;
                case "TORY": // track und year sollen zahlen sein
                    short tory = checkInteger();
                    if (tory > -1)
                    {
                        // jahr muss 4 stellig sein
                        if (tory > 1900)
                        {
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }


        private short checkInteger()
        {
            short dummy;
            if (short.TryParse(content, out dummy)) return dummy;
            else return -1;
        }

        
    }
}
