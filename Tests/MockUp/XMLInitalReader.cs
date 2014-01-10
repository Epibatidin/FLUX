using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using MockUp.Config;
using System.IO;
using System.Xml.Serialization;
using MockUp.XMLItems;
using Extensions;

namespace MockUp
{
    public class XMLInitalReader : IInitalReader
    {
        public int constPathLength
        {
            get
            {
                return _result[pos].Item1.RootPathLength;
            }
        }

      
        public Dictionary<int, IVirtualFile> Current
        {
            get
            { 
                return _result[pos].Item2; 
            }
        }

        object System.Collections.IEnumerator.Current { get { return Current; } }

        Mockup _config;
        private int pos;
        private List<Tuple<Root, Dictionary<int, IVirtualFile>>> _result;

        public XMLInitalReader()
        {
            _config = Mockup.Create();            
            _result = new List<Tuple<Root, Dictionary<int, IVirtualFile>>>();
            Reset();
        }

        private Root readRoot(string artist)
        {
            var fs = new FileStream(Path.Combine(_config.XMLFolder, artist, "Index.xml"), FileMode.Open, FileAccess.Read);
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            var o = ser.Deserialize(fs);
            return o as Root;
        }
        
        private Tuple<Root, Dictionary<int, IVirtualFile>> GetRootByArtist(string artist)
        {
            foreach (var item in _result)
            {
                if(item.Item1.Name == artist)
                {
                    return item;
                }
            }
            var dummy = new Tuple<Root, Dictionary<int, IVirtualFile>>(readRoot(artist), new Dictionary<int, IVirtualFile>());
            _result.Add(dummy);
            // read root from folder ;
            return dummy;
        }
        
        public void Add(string artist, params int[] alben)
        {
            var root = GetRootByArtist(artist);
            
            // foreach read sub 
            var gCount = root.Item1.Groups;

            if (alben == null || alben.Count() == 0)
                alben = Enumerable.Range(0, gCount).ToArray();

            XmlSerializer ser = new XmlSerializer(typeof(Group));

            foreach (var album in alben)
            {
                var fs = new FileStream(Path.Combine(_config.XMLFolder, artist, album + ".xml"), FileMode.Open, FileAccess.Read);
                if (fs == null) continue;

                var group = ser.Deserialize(fs) as Group;
                if (group == null) continue;

                foreach (var item in group.Source.Items)
                {
                    root.Item2.Add(item.ID, item);
                }
            }
        }

        public bool MoveNext()
        {
            return (++pos < _result.Count);           
        }
        
        public void Reset()
        {
            pos = -1;
        }
        public void Dispose() { }
    }
}
