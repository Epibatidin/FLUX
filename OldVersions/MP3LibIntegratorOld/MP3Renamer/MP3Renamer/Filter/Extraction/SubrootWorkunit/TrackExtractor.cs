using System.Collections.Generic;
using System.Linq;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Interfaces;
using MP3Renamer.Helper;


namespace MP3Renamer.Filter.Extraction.SubrootWorkunit
{
    public class TrackExtractor : IExecuteable , ISubrootWorkunit
    {
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
     
   
        public ISubRoot Workunit {get; set;}
        
        
        /// <summary>
        /// funktioniert nur in geordneten listen
        /// ansatzpkt für erweiterungen wäre diese beschräünkung aufzuheben 
        /// aus mangel an idee muss es erstmal so bleiben 
        /// </summary>
        /// <returns></returns>
        public void Execute()
        {          
            // und mal wieder schmeißt mir irgendwer die zahlen raus 
            // na wer wills diesmal sein ?! 
            List<Container> data = new List<Container>();

            // preprocessing             
            List<int> TracksWithNumber = new List<int>();

            for (int i = 0; i < Workunit.Leafs.Count; i++)
            {
                var numbers = getAllNumbersInStringManager(Workunit.Leafs[i].StringManager.RawDataParts);
                // ignoriere alle lieder die keine nummern enthalten 
                if(numbers.Count > 0)
                {
                    TracksWithNumber.Add(i);
                    foreach (var item in numbers)
                    {
                        data.Add(new Container(i, item.Value, item.Key));
                    }
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

            // nur dann hab ich nen problem denn nur dann sind mehr zahlen da als tracks
            if (AllNumbersCount > TracksWithNumber.Count)
            {   
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
                data.ForEach(c => c.UseThis = true);
            }
            ExecuteFoundTracks(data, TracksWithNumber);

            AskMP3Tags();

            
        
        }

        private void AskMP3Tags()
        {
            // sollte es keine track informationen geben 
            // muss ich die mp3 tags beachten 
            // dh ich brauch ne ddl die ich einbinden kann und nach track nummern fragen kann 
            // aber 
            // wer sagt mir das die richtig sind ? 
            // wem vertrau ich mehr der ordner strukltur und der extraktion dieser oder den mp3 tags !? 
            // die ordner struktur gewinnt 
            // also wenn ich schon track information habe aus der ordnerstruktur dann geschieht keine tag abfrage 
            // ansonsten muss ich die tags befragen wenn dies auch net wissen oder mehr als nen int drin steht z.b. CD1 track 7 
            // muss ich das resultat noch durch den nummer extraktor schicken 


            // DAS ist der Plan 

            foreach (var item in Workunit.Leafs)
            {
                if (item.Number == 0 || item.Count == 0) // ich interpretier das mal als nix gefunden 
                {
                    // los frag die dll tue es 
                    var DATA = ExtractInformationFormTag( AskTagInformation(item));
                    if (item.Number == 0)
                    {
                        item.Number = DATA.Value;
                    }
                    if (item.Count == 0)
                    {
                        item.assumedCDCount(DATA.Key);
                    }
                }
            }        
        }

        private KeyValuePair<byte, byte> ExtractInformationFormTag(string TagData)
        {
            return new KeyValuePair<byte, byte>(1, 7);
        }

        private string AskTagInformation(ILeaf leaf)
        { 
            return "CD 1 TRACK 7";
        }



        private void ExecuteFoundTracks(IEnumerable<Container> PossibleTracks , IEnumerable<int> TracksWithNumber)
        {
            // hier müsst ich mir eigentlich merken welche tracks noch nicht dran waren 
            var result = from posTracks in PossibleTracks
                         join tracknumber in TracksWithNumber on posTracks.FoundIn equals tracknumber
                         where posTracks.UseThis
                         orderby posTracks.FoundIn
                         select posTracks;

            foreach (var Track in result)
            {
                Workunit.Leafs[Track.FoundIn].Number = (byte)Track.FoundValue;
                Workunit.Leafs[Track.FoundIn].StringManager.RemoveElementAt(Track.FoundAt);
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
    }
}


