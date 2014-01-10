using System;
using System.Collections.Generic;
using System.IO;

namespace MP3LibIntegrater
{
    class Album : AbstractInstance
    {
        string AbsoluteAlbumPath = String.Empty;
        //protected override string[] relevantInformation = null;
        //public Artist offArtist;
        public string AlbumName
        {
            get
            {
                return Algs.joinString(relevantInformation);
            }
        }

        public string Year = String.Empty; 
        public byte CDcount = 1;
        public byte OffCD = 1;
        
        //List<AbstractInstance> SongList = new List<AbstractInstance>();

        #region init Subs also Songs
        public Album(string AlbumPath, AbstractInstance off)
        {
            this.oneInstanceUp = off;
            AbsoluteAlbumPath = AlbumPath;

            //relevantInformation = chopGivenInformation();
        }

        protected override string[] chopGivenInformation()
        {
            string[] dummy = null;
            
            string[] album = AbsoluteAlbumPath.Split('\\');
            if (CDcount > 1)
                dummy = Algs.Split(album[album.Length - 2]);
            else
                dummy = Algs.Split(album[album.Length - 1]);             

            return dummy;
        }
        

        public List<Album> getSubs()
        {
            // wenn es unterordner gibt 
            // und wenn diese unterordner mp3s enthalten
            // dann erhöhe cd counter 

            string[] subDirs = Directory.GetDirectories(AbsoluteAlbumPath);

            Entities = new List<AbstractInstance>();

            List<Album> result = new List<Album>();

            int subDirCount = subDirs.Length; 
            for(byte i = 0 ; i < subDirCount; i++ )
            {
                Album s = new Album(subDirs[i] , this.oneInstanceUp);
                s.CDcount = (byte)subDirCount;
                s.OffCD = (byte)(i+1);
                s.getSubs();
                result.Add(s);
            }
            
            if (addSongs(AbsoluteAlbumPath))
            {
                result.Add(this);
            }
            //CDcount = (byte)result.Count;

            return result;
        }

        private bool addSongs(string folder)
        {
            bool result = false;
            string[] files = Directory.GetFiles(folder,"*"+ Algs.extension);
                        
            //else return false;
            foreach (string filePath in files)
            {
                result = true;
                Entities.Add(new Song(filePath, OffCD,this ));
            }
            return result;
        }
        #endregion

       

        public void ProduceCleanInformation(List<string> EqualList)
        {

            extractInstanceSpecificNumber();

            removeEquals(EqualList);

            List<string> repInfo = searchRepeatingInformation(Entities);

            foreach (Song song in Entities)
            {
                song.ProduceCleanInformation(repInfo);      
            }            
        }

        #region divideFileName
        protected override void extractInstanceSpecificNumber()
        {
            for (int i = 0; i < relevantInformation.Length; i++)
            {
                if (Algs.executeRegex("year", relevantInformation[i]))
                {
                    Year = extractYear(relevantInformation[i]);
                    relevantInformation[i] = String.Empty;
                }
            }
        }

        private string extractYear(string word)
        {
            string year = String.Empty;

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] >= 48 && word[i] <= 57) // alle ziffern
                {
                    year += word[i];
                }
            }
            return year;
        }
        #endregion


    }

}

/*
private void ProcessSubs()
        {
            List<string> repInfo = searchRepeatingInformation(Entities);

            foreach (Song sond in Entities)
            {

            }


            foreach (Song song in Entities)
            {
                //song.hasMultipleCD = CDcount;
                song.removeRepeatingInformation(repInfo);                
                song.getTrackNum();
            }
        }*/

/*private List<string> searchRefpatingInformation()
  {
      Dictionary<string, short> allWordCount = new Dictionary<string, short>();

      foreach (Song song in SongList)  // für jedes Lied
      {
          foreach (string word in song.getSongNameParts) // nimm alle wörter
          {
              if (allWordCount.ContainsKey(word)) // füge sie dem dict hinzu schon drin ?
              {
                  allWordCount[word]++; // dann erhöhe den counter
              }
              else
              {
                  allWordCount.Add(word, 1); // sonst füge hinzu
              }
          }
      }

      List<string> equals = new List<string>();

      foreach (string word in allWordCount.Keys)
      {
          if (allWordCount[word] >= SongList.Count)
          {
              equals.Add(word);
          }
      }
      return equals;
  }*/