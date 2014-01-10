using System;
using System.Collections.Generic;
using System.IO;

namespace MP3LibIntegrater
{
    class Artist : AbstractInstance
    {
        public string ArtistName = String.Empty;
        string ArtistPath = String.Empty;

        #region initSubs
        public Artist(string artistPath)
        {
            //if (WhiteList.Count == 0)
            this.ArtistPath = artistPath;
            // das letzte stück vom pfad ist der artistname 
            string[] dummy = artistPath.Split('\\');
            ArtistName = dummy[dummy.Length-1];
            relevantInformation = chopGivenInformation();
            oneInstanceUp = null;
        }

        public void getSubs()
        {
            //erzeuge alle Alben 
            string[] AlbumFolders = Directory.GetDirectories(ArtistPath);
            Entities = new List<AbstractInstance>();
 
            foreach (string albumPath in AlbumFolders)
            {
                Album dummy = new Album(albumPath, this);
                List<Album> s = dummy.getSubs();
                foreach (var k in s)
                {
                    Entities.Add(k);
                }
            }
        }
        #endregion

        protected override string[] chopGivenInformation()
        {
            string[] dummy = Algs.Split(ArtistName);
            return dummy;
        }

        public void ProduceCleanInformation()
        {            
            // hier muss ich dafür sorgen das er das gleiche album mit 2 cds nicht doppelt verwendet
            var dummyEntList = new List<AbstractInstance>();

            foreach(var k in Entities)
            {
                if (((Album)k).OffCD == 1)
                    dummyEntList.Add(k);
            }

            List<string> repInfo = searchRepeatingInformation(dummyEntList); 
            
            foreach (Album alb in Entities)
            {
                alb.ProduceCleanInformation(repInfo);
            }
        }

        protected override void extractInstanceSpecificNumber()
        {
        }


    }
}
#region alterMüll

/*public void findAlbumPattern()
        {
            // zerschneide den pfad aller alben 
            // und dann such nach parallelen
            int albumCount = AlbumList.Count();
            if (albumCount <= 1) return;

            for (int i = 0; i < albumCount; i++)
            {
                string[] aInfo = AlbumList[i].getAlbumInfoParts;
                string[] bInfo = AlbumList[(i + 1) % albumCount].getAlbumInfoParts;
                
                if (aInfo.Length >= bInfo.Length)
                    findDifferences(aInfo, bInfo);
                else
                    findDifferences(bInfo, aInfo); 
            }            
        }

        public void cleanUpAlbumInformation()
        {
            foreach (var album in AlbumList)
            {
                album.ProcessUnCleanedInformation(EqualStrings);

                //album.clearEquals(EqualStrings);
            }
        }

        private void findDifferences(string[] aparts, string[] bparts)
        {
            foreach (string a in aparts)
            {
                foreach (string b in bparts)
                {
                    if (String.Compare(a, b, true) == 0)
                    {
                        if (!EqualStrings.Contains(a))
                        {
                            if (!isYearRegex.IsMatch(a))
                            {
                                if (CheackIsNotInWhiteList(a))
                                {
                                    EqualStrings.Add(a);
                                }
                            }                           
                        }
                    }                   
                }
            }
        }

        private bool CheackIsNotInWhiteList(string a)
        {
            if (WhiteList.Contains(a.ToLower()))
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        */
#endregion
/*public List<string> searchRepatingInformation()
       {
           Dictionary<string, short> allWordCount = new Dictionary<string, short>();

           foreach (Album alb in AlbumList)  // für jedes Lied
           {
               foreach (string word in alb.getAlbumInfoParts) // nimm alle wörter
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
               if (allWordCount[word] >= AlbumList.Count)
               {
                   equals.Add(word);
               }
           }
           return equals;
       }*/