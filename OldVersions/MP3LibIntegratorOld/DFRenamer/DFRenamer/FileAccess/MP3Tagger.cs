//using System;
//using System.IO;
//using System.Text;
//using System.Collections.Generic;

//namespace DFRenamer.FileAccess
//{
//    class MP3Tagger
//    {
//        //byte[] fileData = null;

//        long startPos = 0;
//        long endPos = 0;

//        string fileName;
//        byte[] tag = new byte[3];
//        byte[] size = new byte[4];

//        byte[] oneByte = new byte[1];

//        int headerIntSize = 0;



//        /*
//                TRCK    [#TRCK Track number/Position in set]
//                TIT2    [#TIT2 Title/songname/content description]  
//                TALB    [#TALB Album/Movie/Show title]
//                TCOM    [#TCOM Composer]
//                TORY    [#TORY Original release year]
//        */




//        internal void readMP3(string folder, string FileName, string extension)
//        {
//            this.fileName = FileName;
//            using (FileStream inStream = new FileStream(folder + FileName + extension, FileMode.Open))
//            {
//                startPos = readID3Header(inStream);
//                endPos = getEndPos(inStream);
//            }
//        }

//        private long getEndPos(FileStream inStream)
//        {
//            byte[] header = new byte[3];
//            inStream.Seek(-128, SeekOrigin.End);

//            inStream.Read(header, 0, 3);

//            if (Encoding.Default.GetString(header).Equals("TAG"))
//            {
//                // dann muss ich die letzten 128 byte leider wegwerfen
//                return inStream.Length - 128;
//                // naja vlt später auch auswerten und dann wegwerfen 
//            }
//            else return inStream.Length;
//        }

//        private long readID3Header(FileStream inStream)
//        {
//            // ersten 10 byte lesen
//            //tag = new byte[3];              // ID3v2/file identifier  "ID3" 
//            //byte[] noChange = new byte[3];         //ID3v2 version           $03 00
//            ////byte[] size      // ID3v2 size             4 * %0xxxxxxx         

//            //inStream.Read(tag, 0, tag.Length);
//            //inStream.Read(noChange, 0, noChange.Length);
//            //inStream.Read(size, 0, size.Length);

//            //int FrameSize = calculateFrameSizeWithSevenBits(size);

//            //System.Console.WriteLine("Total ID3 Frame Payload Size " + FrameSize + " Byte");
//            //getAllInfoFrames(inStream, FrameSize);

//            return FrameSize + 10;
//        }

//        private void getAllInfoFrames(FileStream inStream, int frameAreaSize)
//        {
//            byte[] frameHeader = new byte[4];
//            byte[] frameSize = new byte[4];
//            byte[] frameFlags = new byte[2];
//            int infoSize = 0;
//            byte[] frameContent;

//            while (inStream.Position < frameAreaSize)
//            {
//                // erzeugen neue InfoFrames Anhand der Informationen im Frame
//                // such die informationen im frame
//                inStream.Read(frameHeader, 0, frameHeader.Length);
//                inStream.Read(frameSize, 0, frameSize.Length);
//                inStream.Read(frameFlags, 0, frameFlags.Length);

//                infoSize = calculate(frameSize);
//                //calculateFrameSize(frameSize, 8);
//                frameContent = new byte[infoSize];
//                string frameTyp = Encoding.ASCII.GetString(frameHeader);
//                inStream.Read(frameContent, 0, infoSize);
//                if (frames.ContainsKey(frameTyp))
//                {
//                    frames[frameTyp].Content(frameContent);
//                }
//            }
//        }

//        private int calculate(byte[] bytes)
//        {
//            byte[] output = new byte[4];

//            output[0] = bytes[3];
//            output[1] = bytes[2];
//            output[2] = bytes[1];
//            output[3] = bytes[0];

//            return BitConverter.ToInt32(output, 0);
//        }






//        internal void HandlerID3(Pattern pat)
//        {
//            // get allinformation
//            foreach (string frameTyp in frames.Keys)
//            {
//                // wenn keine info da ist muss man welche beschaffen
//                // bzw es versuchen
//                // also frag mal im pattern nach 

//                string content = pat.getExtractedInformationByName(frameTyp);
//                frames[frameTyp].Content(content);
//                headerIntSize += frames[frameTyp].getFullFrameSize();
//                // hier bekomm ich die gesamt größe aller frames
//            }
//            System.Console.WriteLine("All Frame Sizes " + headerIntSize);
//            size = calculateFrameSizeWithSevenBits(headerIntSize);
//        }

//        internal void writeMP3(string folder, string NewName, string extension)
//        {
//            // zum schreiben brauch ich einen leser
//            // es sei denn ich ich merke mir den ganzen track in 
//            // einer datenstruktur in diesem object
//            // doof weil zuviel speicher verbrauch 
//            // ich kan auch jeweils byte weise lesen und schreiben 
//            // dauert halt aber andere möglichkeit erst später
//            // ich muss mir die ganze file merken weil ich sonst den header nicht anfügen kann
//            // bzw sonts müsst ich nen count machen oder ausserhalb der while !
//            byte[] oneByte = new byte[1];
//            using (FileStream inStream = new FileStream(folder + fileName + extension, FileMode.Open))
//            {
//                using (FileStream outStream = new FileStream(folder + "out" + NewName + extension, FileMode.Create))
//                {
//                    inStream.Position = startPos;

//                    writeNewID3Header(outStream);

//                    writeFrames(outStream);

//                    //while (inStream.Position <= endPos)
//                    while (inStream.Position < inStream.Length)
//                    {
//                        inStream.Read(oneByte, 0, 1);
//                        outStream.Write(oneByte, 0, 1);
//                    }
//                    outStream.Flush();
//                    System.Console.WriteLine("DONE");
//                }
//            }
//        }


//        private void writeNewID3Header(FileStream outStream)
//        {

//            outStream.Write(tag, 0, 3);// ID3v2/file identifier  "ID3" 
//            oneByte[0] = 0;  //version
//            outStream.Write(oneByte, 0, 1);
//            oneByte[0] = 3; // version 2.3.
//            outStream.Write(oneByte, 0, 1);

//            outStream.Write(size, 0, size.Length);
//        }

//        private void writeFrames(FileStream outStream)
//        {
//            foreach (string frameTyp in frames.Keys)
//            {
//                frames[frameTyp].WriteFrameToStream(outStream);
//            }
//        }


//        private void outPutAsBits(byte[] input)
//        {
//            foreach (byte mh in input)
//            {
//                outPutAsBits(mh);
//                System.Console.Write(" ");
//            }
//            System.Console.WriteLine();
//        }

//        private void outPutAsBits(byte input)
//        {
//            for (int t = 128; t > 0; t = t / 2)
//            {
//                if ((input & t) != 0) System.Console.Write("1");
//                if ((input & t) == 0) System.Console.Write("0");
//            }
//            System.Console.WriteLine();
//        }
//    }// end Class
//}// end NameSpace
///*   string fileName;
//        byte[] tag = new byte[3];
//        byte[] noChange = null;
//        byte[] size = new byte[4];*/


///*
//  private int calculate(byte[] bytes, int usedBits)
//  {
//      bytes[0] = 0;
//      bytes[1] = 0;
//      bytes[2] = 2;
//      bytes[3] = 0;

//      int sum = 0;

//      int size = bytes.Length-1;

//      for (int i = size; i >= 0; i--)
//      {
//          if (i == size) sum = bytes[size];
//          else sum += bytes[i] * ((1 << (usedBits)*(size-i))-1);
                
//          //System.Console.WriteLine("xdfg " + ((1 << (usedBits) * (size - i))-1));
//          System.Console.WriteLine("xcvf" + sum);
//      }

//      return 0;
//  }*/