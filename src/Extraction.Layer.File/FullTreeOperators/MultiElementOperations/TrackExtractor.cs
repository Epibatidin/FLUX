using System.Collections.Generic;
using System.Linq;
using Extraction.Layer.File.Helper;
using Extraction.Layer.File.Interfaces;
using System;
using Extraction.Layer.File.DomainObjects.Track;
using System.Text;

namespace Extraction.Layer.File.FullTreeOperators.MultiElementOperations
{
    public class TrackExtractor : ITrackExtractor
    {
        /// <summary>
        /// der einfachheithalber werde ich tracks nur anhand der relativen position extrahieren       
        /// </summary>
        public bool Execute(IList<FileLayerSongDo> fileLayerSongs, int actualCD)
        {
            var numbers = ExtractNumbers(fileLayerSongs);
            //// no numbers found ?
            if (numbers == null) return false;

            NormalizeTracksAndCalculatePossibleCD(numbers);

            foreach (var item in numbers.Tracks)
            {
                var song = fileLayerSongs[item.FoundIn];
                song.TrackNr = item.NormalizedTrack;
                song.SetCD(item.PossibleCd);

                song.LevelValue.RemoveAt(item.Position);
            }
            return true;
        }

        public static void NormalizeTracksAndCalculatePossibleCD(EvalutatedTrackCollection numbers)
        {
            var indexes = new Dictionary<int, int>();
            
            foreach (var track in numbers.Tracks)
            {
                // normalize track no
                var digits = track.NormalizedTrack.ToString().Select(c => c - 48).ToList();
                StringBuilder trackAsString = new StringBuilder();
                // 123
                bool hadHundreds = false;
                if(hadHundreds = digits.Count == 3)
                {
                    track.PossibleCd = digits[0];
                    digits.Remove(0);
                }

                foreach (var digit in digits)
                {
                    trackAsString.Append(digit);
                }   

                track.NormalizedTrack = int.Parse(trackAsString.ToString());

                if (!hadHundreds)
                    track.PossibleCd = AddOrUpdate(indexes, track.NormalizedTrack, 1, Increment);
            }
        }

        private static int Increment(int x)
        {
            return x + 1;
        }

        public static ProperplySureTrack BuildTrack(int foundIn, PossibleTrack possibleTrack)
        {
            return new ProperplySureTrack()
            {
                FoundIn = foundIn,
                Position = possibleTrack.RelativeBegin(),
                NormalizedTrack = possibleTrack.FoundValue
            };
        }

        private EvalutatedTrackCollection ExtractNumbers(IList<FileLayerSongDo> data)
        {
            // grp by position and take the position which count is equal to song count
            // ziel ist herrauszufinden ob die Tracks relative vorne oder relative hinten stehen 
                 
            List<AllTrackContainer> rawContainerData = new List<AllTrackContainer>();
            bool nothing = true;
            // ich muss mir den track merken und 
            // relative begin pos und relative endpos 

            var relativeBegins = new Dictionary<int, int>();
            var relativeEnds = new Dictionary<int, int>();

            // extract all numbers
            for (int i = 0; i < data.Count; i++)
            {
                var cont = new AllTrackContainer(i);

                var pString = data[i].LevelValue;
                for (int ps = 0; ps < pString.Count; ps++)
                {
                    var numbers = NumberExtractor.ExtractNumbers(pString[ps]);

                    // in diesem context ist es nicht möglich das mehrere nummern in einem string sind 
                    if (numbers.Count == 1)
                    {                        
                        var possibleTrack = new PossibleTrack(ps, numbers[0], pString.Count);

                        AddOrUpdate(relativeBegins, possibleTrack.RelativeBegin(), 1, Increment);
                        AddOrUpdate(relativeEnds, possibleTrack.RelativeEnd(), 1, Increment);

                        cont.PossibleTracks.Add(possibleTrack);
                        nothing = false;
                    }
                }
                rawContainerData.Add(cont);
            }
            if (nothing) return null;

            int pos = 0;
            var posAccessor = SelectPosAccessor(data.Count, relativeBegins, relativeEnds, ref pos);
            if (posAccessor == null) return null;

            var result = BuildEvalutedTrackCollection(rawContainerData, posAccessor, pos);
            
            return result;
        }

        private Func<PossibleTrack, int> SelectPosAccessor(int length, 
            IDictionary<int, int> relativeBegins, IDictionary<int, int> relativeEnds, ref int pos)
        {
            //int pos = 0;
            foreach (var item in relativeBegins)
            {
                if (item.Value != length) continue;
                pos = item.Key;
                return c => c.RelativeBegin();
            }

            foreach (var item in relativeEnds)
            {
                if (item.Value != length) continue;
                pos = item.Key;
                return c => c.RelativeEnd();
            }
            return null;
        }

        private EvalutatedTrackCollection BuildEvalutedTrackCollection(IList<AllTrackContainer> allTracks, 
            Func<PossibleTrack, int> posAccessor, int pos)
        {
            var result = new EvalutatedTrackCollection();
            foreach (var container in allTracks)
            {
                foreach (var item in container.PossibleTracks)
                {
                    if (posAccessor(item) != pos) continue;

                    result.Tracks.Add(BuildTrack(container.FoundIn, item));
                }
            }

            return result;
        }


        private static int AddOrUpdate(IDictionary<int, int> dict, int key, int newValue, Func<int, int> valueUpdater)
        {
            int dummy = 0;
            if (dict.TryGetValue(key, out dummy))
                dict[key] = dummy = valueUpdater(dummy);
            else
            {
                dummy = newValue;
                dict.Add(key, newValue);
            }
            return dummy;
        }        
    }
}


