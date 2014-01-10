using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.IO;

namespace MP3LibIntegrater
{
    class Song : AbstractInstance
    {
        //public Album offAlbum = null;

        //string[] relevantInformation = null;
        public string AbsoluteSongFilePath = String.Empty;
        string FullFileName = String.Empty;
       

        public byte TrackNum{get; private set;}
        public byte OffCD { get; private set; }
        bool hasMultipleCDs = false;
        CompareInfo Compare = CultureInfo.InvariantCulture.CompareInfo;

        public Song(string SongPath, byte OffCD, Album offAlbum)
        {
            this.oneInstanceUp = offAlbum;
            AbsoluteSongFilePath = SongPath;
            this.OffCD = OffCD;
        }

        public string SongName
        {
            get
            {
                return Algs.joinString(relevantInformation);
            }
        }

        public int hasMultipleCD
        {
            set
            {
                if (value > 1)
                {
                    hasMultipleCDs = true;
                }
            }
        }

        protected override string[] chopGivenInformation()
        {
            int punkt = AbsoluteSongFilePath.LastIndexOf('.');
            int slash = AbsoluteSongFilePath.LastIndexOf('\\') + 1;
            
            string[] dummy = Algs.Split(AbsoluteSongFilePath.Substring(slash, punkt - slash));
            
            /*FullFileName = String.Join  // multi char replacement
             * (" ", relevantInformation);*/
            return dummy;
        }

        public void ProduceCleanInformation(List<string> EqualList)
        {
            extractInstanceSpecificNumber();
            
            removeEquals(EqualList);

        }

        #region trackNum
        protected override void extractInstanceSpecificNumber()
        {
            //System.Console.WriteLine(FullFileName);        
            for (int i = 0; i < relevantInformation.Length; i++)
            {
                //string s = "D[1].M.O";
                if (Algs.executeRegex("tracknum", relevantInformation[i]))
                {
                    byte dummy = extractTrackNum(relevantInformation[i]);
                    if (dummy > 0)
                    {
                        relevantInformation[i] = "";
                        TrackNum = dummy;
                    }
                    break;
                }
            }
        }

        private byte extractTrackNum(string word)
        {
            if (word == String.Empty || word.Length > 5) return 0;
            string result = String.Empty;

            for (int i = 0; i < word.Length; i++)
            {
                if (isDigit(word[i]))
                {
                    result += word[i];
                }
            }
            if (result == String.Empty) return 0;
            if (result.Length == 3)
            {                
                OffCD = Convert.ToByte(result[0].ToString());
                ((Album)oneInstanceUp).CDcount = OffCD;
                result = String.Concat(result[1], result[2]);
                hasMultipleCDs = true;
            }
            return Convert.ToByte(result);
        }

        private bool isDigit(char p)
        {
            if (p >= 48 && p <= 57) return true;
            else return false;
        }

        public string createWriteAbleTracknum()
        {
            if (TrackNum == 0) return "";
            string result = String.Empty;
            if (TrackNum < 10)
            {
                result = "0" + TrackNum;
            }
            else result = TrackNum.ToString();
            return "[" + result + "]";
        }
        #endregion
    }
}
