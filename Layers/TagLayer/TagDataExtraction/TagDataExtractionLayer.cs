using System.Collections.Generic;
using System.Linq;
using System.Xml;
using AbstractDataExtraction;
using FrameHandler;
using Interfaces;
using Interfaces.VirtualFile;
using TagDataExtraction.Config;

namespace TagDataExtraction
{
    public class TagDataExtractionLayer : DataExtractionLayerBase
    {
        
        // welche datenstruktur brauch ich ? 
        // eigentlich nur eine liste von Tuple<int, List<Frames>> 
        // 

        private Dictionary<int, ISong> _result;
        private Dictionary<int, IVirtualFile> _initdata;

        ContainerFactory _fac;

        protected override void ConfigureInternal(XmlReader reader)
        {
            var config = new TagLayerConfig(reader);
            _fac = new ContainerFactory(config.IgnorePrivateData);        
        }

        protected override ISongByKeyAccessor Data()
        {
            return null;                
        }
               

        public override void InitData(Dictionary<int, IVirtualFile> dirtyData)
        {
            _initdata = dirtyData;
            //_result = new Dictionary<int, ISong>();
            CreateProgress(_initdata.Count);
        }


        public override void Execute()
        {
            _result = new Dictionary<int, ISong>();
            foreach (var item in _initdata.Values)
            {
                _result.Add(item.ID, CreateSong(item));
                Update();
            }
        }

        private MP3TagSong CreateSong(IVirtualFile file)
        {
            var result = new MP3TagSong();

            result.ID = file.ID;
            var frame = _fac.Create(file.Open());
            var frames = frame.Frames;

            result.Artist = findArtist(frames);

            return result;
        }


        private string findArtist(List<Frame> frames)
        {
            List<string> result = new List<string>();
            foreach (var item in frames)
            {
                if (item.FrameID == "TCOM")
                    result.Add(item.FrameData);
            }
            result = result.Distinct().ToList();

            if(result.Count == 0) {
                return "";
            }
            else 
            {
                return result[0];
            }            
        }

    }
}
