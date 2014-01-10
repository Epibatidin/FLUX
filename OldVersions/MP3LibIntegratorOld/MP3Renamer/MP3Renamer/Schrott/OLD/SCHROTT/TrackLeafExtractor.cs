using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP3Renamer.Helper;
using MP3Renamer.Models.DataContainer.EntityInterfaces;

namespace MP3Renamer.Models.Extraction.Extracter
{
    public class TrackLeafExtractor : ILeafExtractionHelper
    {
        private ISubRoot SubRoot;

        private bool TracksDone = false;

        public void WorkUnit(ISubRoot workunit)
        {
            SubRoot = workunit;
        }

        private class Container
        {
            public int FoundIn { get; private set; }
            public int FoundAt { get; private set; }
            public int FoundValue { get; private set; }

            public bool UseThis { get; set; }

            public Container(int FoundIn, int FoundAt, int FoundValue)
            {
                this.FoundAt = FoundAt;
                this.FoundIn = FoundIn;
                this.FoundValue = FoundValue;
            }
        }

        /// <summary>
        /// funktioniert nur in geordneten listen
        /// ansatzpkt für erweiterungen wäre diese beschräünkung aufzuheben 
        /// aus mangel an idee muss es erstmal so bleiben 
        /// </summary>
        /// <returns></returns>
        public ISubRoot execute()
        {
            // und mal wieder schmeißt mir irgendwer die zahlen raus 
            // na wer wills diesmal sein ?! 
            
            int TrackCount = SubRoot.Leafs.Count;

            List<Container> data = new List<Container>();

            for (int i = 0; i < SubRoot.Leafs.Count; i++)
            {
                foreach (var item in getAllNumbersInStringManager(SubRoot.Leafs[i].StringManager.RawDataParts))
                {
                    data.Add(new Container(i, item.Value, item.Key));
                }
            }

            // jetzt ein join und alle die überbleiben sind verdächtig 
            // aber was ist mit den doppelten ?! 
            // ein lied in dem die 2 und die 4 drin ist 
            // wurde dann 2 mal matchen 

            // immernoch die frage woher weiß ich elches der track its ? 
            // sortieren aber wonach ? 
            // halt das ist gut 

            // sortieren und dann mit der enum liste abgleichen 
            // ist das nicht ein db sync problem ?
            // so rein von der struktur ? 
            // iteriere beide listen und controlliere welches zuviel , zuwenig, anders ist 


            int AllNumbersCount = data.Count();

            OutputInFile file = new OutputInFile();

            // nur dann hab ich nen problem denn nur dann sind mehr zahlen da als tracks
            if (AllNumbersCount > TrackCount)
            {
                TracksDone = false;
                
                var allSortedNumbers = data.OrderBy(c => c.FoundValue).Select(c => c).ToList();
                //file.WriteStringToFile(ConvertListToString(allSortedNumbers));

                // ich muss die kleinere liste durchgehen und suchen welche in der großen doppelt drin sind 
                // am ende brauch ich ne liste von allen datensätzen die nicht zugeordnet werden konnten 
                // weil doppelt oder nicht vorhanden 
                // nicht vorhanden ist unwahrscheinlich 
                // also nur die doppelten to

                var grps = (from d in data
                            group d by d.FoundIn into grp
                            select grp).ToList();

                //file.WriteStringToFile(ConvertGrpToString(grps));

                List<Container> Result = new List<Container>();
                // iteriere über die gruppierung
                for (int i = 0; i < grps.Count; i++)
                {
                    // wenn es nur eine einzige zahl gibt ist alles cool
                    if (grps[i].Count() == 1)
                    {
                        var blub = grps[i].First();
                        Container con = new Container(blub.FoundIn, blub.FoundAt, blub.FoundValue);
                        con.UseThis = true;
                        Result.Add(con);
                        //grps.RemoveAt(i);
                        //i--;
                    }
                    else // hier kommt der fiese teil 
                    {
                        // woher weiß ich welche zahl besser passt ?  
                        // die zahlen sind doch geordnet 
                        // dh vlt geben mir ja die tracks davor und danach information 
                        
                        // sonder fälle ... 
                        // anfang 
                        // ende 
                        // doppelt nicht gefunden
                        foreach (var item in grps[i])
                        {
                            
                            // kontrolliere jede dieser verdammten zahlen ob sie in davor oder danach reinpasst 
                            // ich fang bei 0 an das ist kritisch                            
                            // ich nehme die zahl davor und danach und schau mir an ob die in der grp dabei ist

                            // gibt es eine lösung die nicht auf ordnung funktioniert ? 
                            // kann es nicht geben denn ich will eine ordnung erzeugen und das was da ist ist mein einziger anhaltspunkt

                            // nimm die durchschnitts lösung eine andere gibt es nicht 
                            // bzw fällt mir keine ein
                            // das würde aber doch bedeuten das I die zahl ist die ich suche ? 
                         
		                    if(item.FoundValue == i + 1)
                            {
                                item.UseThis = true;
                                Result.Add(item);
                                break;
                            }
                            
                        }
                    }
                }
                data = Result;
            }
            else
            {
                TracksDone = true;
                data.ForEach(c => c.UseThis = true);
            }
            ExecuteFoundTracks(data);


            return SubRoot;
        }

        private string ConvertListToString(List<Container> data)
        {
            StringBuilder b = new StringBuilder();

            foreach (var grp in data)
            {
                b.AppendLine("FoundIn : " + grp.FoundIn + " FoundAt : " + grp.FoundAt + " Value : " + grp.FoundValue );
            }
            return b.ToString();        
        }


        private string ConvertGrpToString ( IEnumerable<IGrouping<Int32, Container>> Content)
        {
            StringBuilder S = new StringBuilder();

            foreach (var item in Content)
            {
                S.AppendLine("Track : " + item.Key + " " );

                foreach (var grp in item)
                {
                    S.AppendLine("FoundIn : " + grp.FoundIn + " FoundAt : " + grp.FoundAt + " Value : " + grp.FoundValue );
                }
                S.AppendLine();
            }
            return S.ToString();
        }




        private void ExecuteFoundTracks(IEnumerable<Container> PossibleTracks)
        {
            // hier müsst ich mir eigentlich merken welche tracks noch nicht dran waren 
            foreach (var Track in PossibleTracks.Where(c => c.UseThis).OrderBy(c => c.FoundIn).Select(c => c))
            {
                SubRoot.Leafs[Track.FoundIn].Number = (byte)Track.FoundValue;
                SubRoot.Leafs[Track.FoundIn].StringManager.RemoveElementAt(Track.FoundAt);
            }
        }


        private List<KeyValuePair<int, int>> getAllNumbersInStringManager(List<string> rawdata)
        {
            List<KeyValuePair<int, int>> Result = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < rawdata.Count; i++)
            {
                var numbers = NumberExtractor.ExtractNumbers(rawdata[i]);

                foreach (var number in numbers)
                {
                    Result.Add(new KeyValuePair<int, int>(number, i));
                }
            }
            return Result;
        }

        private void Track2()
        {
            byte maxCDCount = 0;
            for (int current = 0; current < SubRoot.Leafs.Count; current++)
            {
                List<KeyValuePair<byte, short>> allNumbers = new List<KeyValuePair<byte, short>>();

                var parts = SubRoot.Leafs[current].StringManager.RawDataParts;

                for (int i = 0; i < parts.Count(); i++)
                {
                    var numbers = NumberExtractor.ExtractNumbers(parts[i]);
                    if (numbers != null && numbers.Count() == 1)
                    {
                        allNumbers.Add(new KeyValuePair<byte, short>((byte)i, numbers.First()));
                    }
                }

                //SubRoot.Leafs[current].StringManager.RawDataParts = parts;

                Int16? track = allNumbers.Select(c => c.Value).OrderBy(c => c).FirstOrDefault();
                // ordne die zahlen der größe nach und hoffe das die kleinste zahl das track ist  
                // die kleinste zahl ist lame alternatien ?! 
                // all numbers weiter aufbohren => gerade be 


                byte assumedCdCount = 0;
                if (track.HasValue)
                {
                    if (track > 99)
                    {
                        var s = track.ToString();

                        Byte.TryParse(s[0] + "", out assumedCdCount);
                    }
                    SubRoot.Leafs[current].assumedCDCount(assumedCdCount);
                    SubRoot.Leafs[current].Number = (byte)track;
                }
                if (maxCDCount < assumedCdCount)
                    maxCDCount = assumedCdCount;
            }

            SubRoot.CDCount = maxCDCount;
        }




     

        public void setFilter(byte filter)
        {
            throw new NotImplementedException();
        }

        public void activateAllFilters()
        {
            throw new NotImplementedException();
        }
    }
}