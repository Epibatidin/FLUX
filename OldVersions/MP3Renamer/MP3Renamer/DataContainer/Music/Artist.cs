using System;
using System.Collections.Generic;
using MP3Renamer.DataContainer.EntityInterfaces;

namespace MP3Renamer.DataContainer.Music
{
    public class Artist : IRoot
    {
        private string RawData = String.Empty;


        //-----------------------------------------------------------------------------------------------------------------------
        public byte ID { get; set; }
        //-----------------------------------------------------------------------------------------------------------------------
        

        //---------------------------------------------------------------------
        public Artist(string PreProceedData)// : base(PreProceedData)
        //---------------------------------------------------------------------
        {
            // ist es sinnvoll die ur information mit aufzuheben ?! 
            // zur rekonstruktion der pfad namen => ja
            this.RawData = PreProceedData;
            this.Name = PreProceedData;

        }

        // er braucht eine liste on alben 
        // anders hat das keinen sinn
        // ich brauch ide hierachie da sie information enthält 
        // und ich brauch einzel entitäten von alben 
        //---------------------------------------------------------------------
        private List<ISubRoot> AlbumList = null;
        public List<ISubRoot> SubRoots
        //---------------------------------------------------------------------
        {
            get
            {
                if (AlbumList == null)
                {
                    AlbumList = new List<ISubRoot>();
                }
                return AlbumList;
            }
            set
            {
                AlbumList = value;
            }
        }

        //---------------------------------------------------------------------
        private string ArtistName = String.Empty;
        public string Name
        //---------------------------------------------------------------------
        {
            get 
            {
                return ArtistName;
            }
            private set
            {
                ArtistName = value;               
            }
        }


    }
}