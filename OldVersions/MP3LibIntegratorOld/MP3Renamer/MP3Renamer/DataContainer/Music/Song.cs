using System;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.DataContainer;

namespace MP3Renamer.Models.DataContainer.Music
{
    public class Song : ILeaf
    {

        //-----------------------------------------------------------------------------------------------------------------------
        public byte ID { get; set; }
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        private string RemExtension(string FileName)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (String.IsNullOrWhiteSpace(FileName)) return "";

            int pointPos = FileName.LastIndexOf('.');
            
            if (pointPos > 0)
            {
                return FileName.Remove(pointPos);
            }
            return "";
        }
          


        //-----------------------------------------------------------------------------------------------------------------------
        public Song(string PreProceedData, string FullFilePath)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            this.Name = RemExtension(PreProceedData);
            this.FullFilePath = FullFilePath;
            StringManager = new RawDataManager(Name);
        }


        public IStringPartManager StringManager { get; set; }


        //-----------------------------------------------------------------------------------------------------------------------
        private string SongName = String.Empty; 
        public string Name
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get 
            {
                return SongName;
            }
            set
            {
                SongName = value;
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private byte TrackNumber = 0;
        public byte Number
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get 
            {
                return TrackNumber;    
            }
            set
            {
                TrackNumber = value;
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private byte cdCount = 0;
        public byte Count
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                return cdCount;
            }
            private set
            {
                cdCount = value;
            }

        }


        //-----------------------------------------------------------------------------------------------------------------------
        private string fullfilepath = String.Empty;
        public string FullFilePath
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                return fullfilepath;
            }
            private set
            {
                fullfilepath = value;
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------
        public void assumedCDCount(byte assumedCount)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (assumedCount > 0)
            {
                Count = assumedCount;
            }        
        }
    }
}