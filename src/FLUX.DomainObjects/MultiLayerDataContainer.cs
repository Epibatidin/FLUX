﻿using System.Collections.Generic;
using Extension.IEnumerable;
using Extraction.Interfaces;

namespace FLUX.DomainObjects
{
    public class MultiLayerDataContainer
    {
        private readonly IList<string> _parts;
        public int Id { get; set; }

        public MultiLayerDataContainer(string[] parts)
        {
            var modParts = new List<string>(parts);
            if (modParts.Count == 3)
                modParts.Insert(2, "");
            _parts = modParts;

            Data = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Data;

        public string GetOriginalValue(int depth)
        {
            return _parts[depth];
        }
        
        public void AddSong(ISong song)
        {
            if (song == null) return;
            
            AddValue(nameof(ISong.Artist), song.Artist);
            AddValue(nameof(ISong.Year), song.Year);
            AddValue(nameof(ISong.Album), song.Album);
            AddValue(nameof(ISong.CD), song.CD);
            AddValue(nameof(ISong.TrackNr), song.TrackNr);
            AddValue(nameof(ISong.SongName), song.SongName);
        }

        public string GetGroupingKeyByDepth(int depth)
        {
            var key = GetKeyByDepth(depth);
            if (key == null) return null;

            if (!Data.ContainsKey(key))
                return null;

            var assambledkey = string.Join("-", Data[key]) + depth;

            return assambledkey;
        }

        private string GetKeyByDepth(int depth)
        {
            switch (depth)
            {
                case 0: return nameof(ISong.Artist);
                case 1: return nameof(ISong.Album);
                case 2: return nameof(ISong.CD);
                case 3: return nameof(ISong.SongName);
            }
            return null;
        }
        
        private void AddValue(string key, int value)
        {
            if (value == 0) return;
            AddValue(key, value.ToString());
        }

        private void AddValue(string key, string value)
        {
            //if(string.IsNullOrEmpty(value)) return;
            List<string> values = Data.GetOrCreate(key);
            values.Add(value);
        }
    }
}