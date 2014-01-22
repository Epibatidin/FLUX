using System.Collections.Generic;
using System.Linq;
using FileStructureDataExtraction.Annotations;
using FileStructureDataExtraction.Helper;
using Common.StringManipulation;
using System;
using Tree;

namespace FileStructureDataExtraction.Extraction
{
    public class TrackExtractor 
    {
        private class Container
        {
            public int FoundIn { get; private set; }

            public List<PossibleTrack> PossibleTracks; 

            public Container(int FoundIn)
            {
                this.FoundIn = FoundIn;
                PossibleTracks = new List<PossibleTrack>();
            }
        }

        private class PossibleTrack
        {
            public int FoundAt { get; private set; }
            public bool FoundAtIsLastPos { get; private set; }

            public int FoundValue { get; private set; }
            public bool UseThis { get; set; }
            
            public PossibleTrack(int FoundAt, int FoundValue, int stringLength)
            {

                this.FoundAt = FoundAt;
                this.FoundValue = FoundValue;
                FoundAtIsLastPos = stringLength == FoundAt;

            }
        }


        List<TreeItem<FileLayerSongDO>> _data;
        readonly Func<TreeItem<FileLayerSongDO>, PartedString> _getter;

        public TrackExtractor(Func<TreeItem<FileLayerSongDO>, PartedString> fun)
        {
            _getter = fun;
        }

        public void CurrentData(List<TreeItem<FileLayerSongDO>> data)
        {
            _data = data;
        }

        private List<Container> ExtractNumbers(List<TreeItem<FileLayerSongDO>> data, Func<TreeItem<FileLayerSongDO>, PartedString> fun)
        {
            List<Container> result = new List<Container>();
            bool nothing = true;
            for (int i = 0; i < _data.Count; i++)
            {
                var cont = new Container(i);

                var pString = _getter(_data[i]);
                for (int ps = 0; ps < pString.Count; ps++)
                {
                    var numbers = NumberExtractor.ExtractNumbers(pString[ps]);

                    // in diesem context ist es nicht möglich das mehrere nummern in einem string sind 
                    if (numbers.Count == 1)
                    {
                        cont.PossibleTracks.Add(new PossibleTrack(ps, numbers[0], pString.Count));
                        nothing = false;
                    }
                }
                result.Add(cont);
            }
            if (nothing) return null;
            return result;
        }
        
        private bool CheckIfAllTitlesHasExactlyOneNumber(List<Container> numbers)
        {
            foreach (var item in numbers)
            {
                if (item.PossibleTracks.Count != 1)
                    return false;
            }
            return true;
        }

        private void ExtractCDs(List<Container> numbers)
        {
            // groupiere numbers nach mod blub 
            // nimm aber nur die wo usethis = true 
            var r = from i in numbers
                    from pT in i.PossibleTracks
                    where pT.UseThis
                    group new { i, pT } by pT.FoundValue / 100 into CDs
                    from cd in CDs
                    select new
                    {
                        Track = cd.pT.FoundValue - CDs.Key * 100,
                        CD = CDs.Key, 
                        FoundIn = cd.i.FoundIn,
                        FoundAt = cd.pT.FoundAt
                    };

            foreach (var item in r)
            {
                var value = _data[item.FoundIn].Value;
                value.CD = item.CD;
                value.TrackNr = item.Track;
                value.LevelValue.RemoveAt(item.FoundAt);
            }
        }

        /// <summary>
        /// der einfachheit halber werde ich tracks nur anhand der relativen position extrahieren       
        /// </summary>
        public void Execute()
        {
            var numbers = ExtractNumbers(_data, _getter);
            // no numbers found ?
            if (numbers == null) return;

            var r = (from i in
                         (from n in numbers
                          from p in n.PossibleTracks
                          group new { n, p } by (p.FoundAtIsLastPos ? -1 : p.FoundAt) into grp // map lastPos to -1 
                          select grp)
                    where i.Count() == _data.Count
                    select i).FirstOrDefault().Select(c => c.p);

            foreach (var item in r)
            {
                item.UseThis = true;
            }

            ExtractCDs(numbers);

            var z = _data;
        }
 
    }
}


