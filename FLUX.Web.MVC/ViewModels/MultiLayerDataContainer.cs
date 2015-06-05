//using System.Collections.Generic;

//namespace FLUX.Web.MVC.ViewModels
//{
//    public class MultiLayerDataContainer
//    {
//        public MultiLayerDataContainer()
//        {
//            Data = new Dictionary<string, List<string>>();
//        }

//        public MultiLayerDataContainer(IVirtualFile initData)
//        {
//            Data = new Dictionary<string, List<string>>();
//        }

//        public Dictionary<string, List<string>> Data;

//        public string Path { get; set; }

//        public void AddSong(int depth,ISong song)
//        {
//            if (song == null) return;

//            switch (depth)
//            {
//                case 0:
//                    {                        
//                        AddValue("Artist", song.Artist);                        
//                        break;
//                    }
//                case 1:
//                    {
//                        AddValue("Year", song.Year);
//                        AddValue("Album", song.Album);
//                        break;
//                    }
//                case 2:
//                    {
//                        AddValue("CD", song.CD);
//                        break;
//                    }
//                case 3:
//                    {
//                        AddValue("Track",song.TrackNr);
//                        AddValue("Title", song.SongName);
//                        break;
//                    }
//            }
//        }


//        private void AddValue(string key, int value)
//        {
//            string res = " ";
//            if (value > 0)
//                res = value.ToString();

//            AddValue(key, res);
//        }

//        private void AddValue(string key, string value)
//        {           
//            List<string> values = Data.GetOrCreate(key);
//            if (string.IsNullOrWhiteSpace(value)) return;
//            values.Add(value);
//        }
//    }
//}