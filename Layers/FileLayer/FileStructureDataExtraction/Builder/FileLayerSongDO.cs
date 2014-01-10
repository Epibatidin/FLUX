using Common.StringManipulation;
using Interfaces;

namespace FileStructureDataExtraction.Builder
{
    public class FileLayerSongDO : ISong
    {
        public int ID { get; set; }        
        public int CD { get; set; }
        public int TrackNr { get; set; }
        public int Year { get; set; }

        public string Artist
        {
            get
            {
                return LevelValue.ToString();
            }
        }

        public string Album
        {
            get
            {
                return LevelValue.ToString();
            }
        }


        public string SongName
        {
            get
            {
                return LevelValue.ToString();
            }
        }

        public PartedString LevelValue { get; set; }

        public void SetByDepth(int depth, string value)
        {
            LevelValue = new PartedString(value);

            //switch (depth)
            //{
            //    case 0 : Artist = value; break;
            //    case 1 : Album = value; break;
            //    case 3 : SongName = value; break;                     
            //}
        }

        //public PartedString GetByDepth(int depth)
        //{
        //    return data[depth];
        //}



        //public void Update(int depth, Func<PartedString, PartedString> fun)
        //{
        //    data[depth] = fun(data[depth]);
        //}


    }
}
