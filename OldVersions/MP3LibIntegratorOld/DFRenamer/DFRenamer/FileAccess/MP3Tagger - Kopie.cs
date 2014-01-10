using System.IO;
using System.Text;
using System;

namespace DFRenamer.FileAccess
{
    class MusicID3Tag
    {
        public byte[] TAGID = new byte[3];      //  3
        public byte[] Title = new byte[30];     //  30
        public byte[] Artist = new byte[30];    //  30 
        public byte[] Album = new byte[30];     //  30 
        public byte[] Year = new byte[4];       //  4 
        public byte[] Comment = new byte[30];   //  30 
        public byte[] Genre = new byte[1];      //  1
    }
    
    class MP3Tagger
    {
        //string filePath = @"C:\Documents and Settings\All Users\Documents\My Music\Sample Music\041105.mp3";

        public void readTags(string path)
        {                 
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                if (fs.Length >= 128)
                {
                    MusicID3Tag tag = new MusicID3Tag();
                    fs.Seek(-128, SeekOrigin.End);
                    fs.Read(tag.TAGID, 0, tag.TAGID.Length);
                    fs.Read(tag.Title, 0, tag.Title.Length);
                    fs.Read(tag.Artist, 0, tag.Artist.Length);
                    fs.Read(tag.Album, 0, tag.Album.Length);
                    fs.Read(tag.Year, 0, tag.Year.Length);
                    fs.Read(tag.Comment, 0, tag.Comment.Length);
                    fs.Read(tag.Genre, 0, tag.Genre.Length);
                    string theTAGID = Encoding.Default.GetString(tag.TAGID);

                    if (theTAGID.Equals("TAG"))
                    {
                        string Title = Encoding.Default.GetString(tag.Title);
                        string Artist = Encoding.Default.GetString(tag.Artist);
                        string Album = Encoding.Default.GetString(tag.Album);
                        string Year = Encoding.Default.GetString(tag.Year);
                        string Comment = Encoding.Default.GetString(tag.Comment);
                        string Genre = Encoding.Default.GetString(tag.Genre);

                        Console.WriteLine("Titel " + Title);
                        Console.WriteLine("Artist " + Artist);
                        Console.WriteLine("Album " + Album);
                        Console.WriteLine("Year " +Year);
                        Console.WriteLine("Comment " + Comment);
                        Console.WriteLine("Genre " + Genre);
                        Console.WriteLine();
                    }
                }
            }
        }

        public void writeMP3(string path, string filename)
        {
            FileStream outStream = new FileStream(path + "Out" + filename, FileMode.Create);
            BinaryWriter BitWriter = new BinaryWriter(outStream);

            //BitWriter.Write(dummy);
            BitWriter.Flush();
            BitWriter.Close();
        }



        public void readMP3(string path, string filename)
        {
            FileStream inStream = new FileStream(path+filename, FileMode.Open);            

            byte[] dummy = new byte[1];
            System.Console.WriteLine("STARTING");
            searchHeader(inStream);
         
            // von bis einlesen anhand der wert die ich von searchheader bekomme 
            // alles einlesen und in ein array schreiben 
            // kein string

            System.Console.WriteLine(inStream.Length / 100);

            // such die header und teile die menge der zu kopierenden daten in 100 stücke
            // zur not 101 das geht schon aber nix vergessen das wichtig

             
            System.Console.WriteLine("DONE");


        }

        private void searchHeader(FileStream inStream)   
        {
            // header kann am ende oder am anfang stehen 
            // beides muss kontrolliert und behandelt werden
            
            // ende ist komplizierter also anfang zuerst

            byte[] header = new byte[128];
            inStream.Seek(0, SeekOrigin.Begin);

            inStream.Read(header, 0, 128);
        
            string Tag = Encoding.Default.GetString(header);
            System.Console.WriteLine("ERSTEN 128 BYTE |" + Tag + "|END");
            inStream.Seek(-128, SeekOrigin.End);

            inStream.Read(header, 0, 128);

            Tag = Encoding.Default.GetString(header);
            System.Console.WriteLine();
            System.Console.WriteLine("LETZTEN 128 BYTE |" + Tag + "|END");                    
            
        }

        public void ReadWholeArray(Stream stream, byte[] data)
        {
            int offset = 0;
            int remaining = data.Length;
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException
                        (String.Format("End of stream reached with {0} bytes left to read", remaining));
                remaining -= read;
                offset += read;
            }
        }

        internal void HandlerID3()
        {
            throw new NotImplementedException();
        }
    } // end Class
} // end Namespace