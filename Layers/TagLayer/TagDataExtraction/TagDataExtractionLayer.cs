using System.Collections.Generic;
using AbstractDataExtraction;
using Interfaces;
using FrameHandler;
using System.Configuration;
using Interfaces.VirtualFile;
using TagDataExtraction.Config;
using System.Linq;
using Common;

namespace TagDataExtraction
{
    public class TagDataExtractionLayer : DataExtractionLayerBase
    {
        
        // welche datenstruktur brauch ich ? 
        // eigentlich nur eine liste von Tuple<int, List<Frames>> 
        // 

        private Dictionary<int, ISong> _result;
        private Dictionary<int, IVirtualFile> _initdata;

        ContainerFactory fac;

        public override void Configure(ConfigurationSection config)
        {
            var _config = (TagLayerConfig)config;
            fac = new ContainerFactory(_config.IgnorePrivateData);        
        }

        protected override object Data()
        {
            return _result;                
        }
               

        public override void InitData(int constPathLength, Dictionary<int, IVirtualFile> _dirtyData)
        {
            _initdata = _dirtyData;
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
            var frame = fac.Create(file.Open());
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
