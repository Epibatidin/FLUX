using System.Collections.Generic;
using DataAccess.Interfaces;
using Extension.IEnumerable;
using Extraction.Interfaces;

namespace FLUX.DomainObjects
{
    public class MultiLayerDataContainer
    {
        public int Id { get; set; }

        public MultiLayerDataContainer()
        {
            Data = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Data;

        public string Path { get; set; }

        public void AddVirtualFile(int depth, IVirtualFile vf)
        {
            switch (depth)
            {
                case 0:
                    {
                        AddValue("Artist", vf.Name);
                        break;
                    }
                case 1:
                    {
                        AddValue("Year", "");
                        AddValue("Album", vf.Name);
                        break;
                    }
                case 2:
                    {
                        AddValue("CD", vf.Name);
                        break;
                    }
                case 3:
                    {
                        AddValue("Track", "");
                        AddValue("Title", vf.Name);
                        break;
                    }
            }
        }


        public void AddSong(ISong song)
        {
            if (song == null) return;

            AddValue("Artist", song.Artist);
            AddValue("Year", song.Year);
            AddValue("Album", song.Album);
            AddValue("CD", song.CD);
            AddValue("Track", song.TrackNr);
            AddValue("Title", song.SongName);
        }


        private void AddValue(string key, int value)
        {
            string res = " ";
            if (value > 0)
                res = value.ToString();

            AddValue(key, res);
        }

        private void AddValue(string key, string value)
        {
            List<string> values = Data.GetOrCreate(key);
            values.Add(value);
        }
    }
}