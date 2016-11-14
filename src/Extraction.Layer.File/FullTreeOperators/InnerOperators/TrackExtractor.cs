using System;
using System.Collections.Generic;
using System.Linq;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Layer.File.Helper;

namespace Extraction.Layer.File.FullTreeOperators.InnerOperators
{
    public class TrackExtractor : ITrackExtractor
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

            public PossibleTrack(int foundAt, int foundValue, int stringLength)
            {
                FoundAt = foundAt;
                FoundValue = foundValue;
                FoundAtIsLastPos = stringLength == foundAt;
            }
        }

        /// <summary>
        /// der einfachheithalber werde ich tracks nur anhand der relativen position extrahieren       
        /// </summary>
        public bool Execute(IList<FileLayerSongDo> partedStrings)
        {
            var numbers = ExtractNumbers(partedStrings);
            // no numbers found ?
            if (numbers == null) return false;

            var r = (from i in
                         (from n in numbers
                          from p in n.PossibleTracks
                          group new { n, p } by (p.FoundAtIsLastPos ? -1 : p.FoundAt) into grp // map lastPos to -1 
                          select grp)
                     where i.Count() == partedStrings.Count
                     select i).FirstOrDefault().Select(c => c.p);

            foreach (var item in r)
            {
                item.UseThis = true;
            }

            return ExtractCDs(partedStrings, numbers);
        }


        private List<Container> ExtractNumbers(IList<FileLayerSongDo> data)
        {
            List<Container> result = new List<Container>();
            bool nothing = true;
            for (int i = 0; i < data.Count; i++)
            {
                var cont = new Container(i);

                var pString = data[i].LevelValue;
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

        private bool ExtractCDs(IList<FileLayerSongDo> allContent ,List<Container> numbers)
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

            bool thereWasAcdRewrite = false;

            foreach (var item in r)
            {
                var value = allContent[item.FoundIn];
                value.SetCD(item.CD);
                value.TrackNr = item.Track;
                value.LevelValue.RemoveAt(item.FoundAt);
                thereWasAcdRewrite = true;
            }
            return thereWasAcdRewrite;
        }
    }
}


