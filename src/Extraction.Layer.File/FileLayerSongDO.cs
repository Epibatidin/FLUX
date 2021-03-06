﻿using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File
{
    public class FileLayerSongDo : ISong
    {
        public FileLayerSongDo()
        {
            OverwrittenValues = new string[4];
        }

        public FileLayerSongDo(string levelValue) : this()
        {
            LevelValue = new PartedString(levelValue).Split();
        }

        public int Id { get; set; }
        
        public int TrackNr { get; set; }
        public int Year { get; set; }

        private string[] OverwrittenValues;

        public string Artist => ResolveValue(0);
        public string Album => ResolveValue(1);
        public string CD => ResolveValue(2);
        public string SongName => ResolveValue(3);
        
        public int CDRaw { get; set; }

        public PartedString LevelValue { get; set; }

        private int _depth;

        private string ResolveValue(int currentDepth)
        {
            if (_depth != currentDepth) return null;

            var value = OverwrittenValues[currentDepth];

            if(value == null)
                return LevelValue.ToString();

            return value;
        }

        public void SetByDepth(int depth, string value)
        {
            _depth = depth;
            LevelValue = new PartedString(value);            
        }

        public void SetCD(int i)
        {
            OverwrittenValues[2] = i.ToString();
            CDRaw = i;
        }
    }
}
