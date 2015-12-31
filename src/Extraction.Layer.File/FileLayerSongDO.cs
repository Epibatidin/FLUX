using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File
{
    public class FileLayerSongDo : ISong
    {
        public int Id { get; set; }

        public int CD { get; set; }
        public int TrackNr { get; set; }
        public int Year { get; set; }

        public string Artist { get; set; }
        public string Album { get; set; }
        public string SongName { get; set; }

        public PartedString LevelValue { get; set; }

        public void SetByDepth(int depth, string value)
        {
            LevelValue = new PartedString(value);

            switch (depth)
            {
                case 0: Artist = value; break;
                case 1: Album = value; break;
                case 3: SongName = value; break;
            }
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
