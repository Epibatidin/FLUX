using MP3Renamer.DataContainer.EntityInterfaces;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MP3Renamer.Helper;

namespace MP3Renamer.DataContainer.Music
{
    /// <summary>
    /// Kapselt eine Datei gültigen Musiktyps 
    /// bietet Methoden zum Zugriff auf unterschiedliche Stufen
    /// der Ordner Hierachie und somit auf die Rohdaten der MusicDatei
    /// </summary>
    public class MusicDataContainer : IDataContainer
    {
        //root ist ein folder 
        //oder root ist ein name 
        //beides ist sinnvoll 
        //also getAs Dir oder get as String 
        //ch denke vorerst wird nur der sting benötigt aber 
        //später wird es nötig sien alle elemnte dieser stufe zu bekommen 
        //also zb eine menge on ordnern oder Album datencontainern

        static string isCD = @"^[cC][dD][ -.]?[1-9]$";

        public MusicDataContainer(FileInfo file, string InitalPath)
        {
            // initalPfad rauswerfen und aufteilen
            List<string> fileParts = file.DirectoryName.Replace(InitalPath, "").Split('\\').ToList();
            fileParts.RemoveAt(0); // der leere string der vor dem ersten \\ stehen bleibt muss weg 
            LeafAsString = file.Name;
            extractData(fileParts);

        }

        private bool IsNotNullOrEmpty<T>(IEnumerable<T> input)
        {
            if (input == null) return false;
            if (input.Count() == 0) return false;
            return true;
        }


        private void extractData(List<string> fileParts)
        {
            int cdCount = -1;
            // so jetzt muss ich rausfinden welche daten enthalten sind 
            // kann mir die anzahl der parts dabei helfen ?
            if (IsNotNullOrEmpty(fileParts))
            {
                //datei name und initalpfad sind raus was bleibt über ? 
                // der artist das album und eventuell der cd count 
                // der hinterste part ist entweder die cd oder das album 
                fileParts = HasMultipleCDs(fileParts);

                cdCount = -1;
                // so CD is raus dh davor ist das Album               
                SubRootAsString = fileParts.LastOrDefault();

                // und an erster Stelle steht der Artist
                RootAsString = fileParts.FirstOrDefault();
            }
            fileParts = null;
        }

        private List<string> HasMultipleCDs(List<string> fileParts)
        {
            if (IsNotNullOrEmpty(fileParts))
            {
                // enthält das letzte Stück cd im namen ?
                if (Regex.IsMatch(fileParts.Last(), isCD))
                {
                    // dann haben wir ein multicd Album und müssen einen extra werte setzen 
                    // bzw muss erst die zahl extrahiert werden 
                    // dh ich brauch nen ZAhlenextraktor
                    var cdCounts = NumberExtractor.ExtractNumbers(fileParts.Last());

                    if (cdCounts.Count() == 1)
                    {
                        if (cdCounts[0] > 0)
                        {
                            ExtraInformationAsString = cdCounts[0].ToString();
                        }
                    }
                    fileParts.RemoveAt(fileParts.Count() - 1);
                }
            }
            return fileParts;
        }



        /// <summary>
        /// Gibt den Namen des Künstlers zurück
        /// </summary>
        public string RootAsString { get; private set; }

        /// <summary>
        /// Gibt den Namen des Albums zurück
        /// </summary>
        private string subRootAsString = "";
        public string SubRootAsString
        {
            get
            {
                return subRootAsString;
            }
            private set
            {
                subRootAsString = value;
            }
        }

        /// <summary>
        /// Gibt den Nummer der CD zurück
        /// </summary>
        public string ExtraInformationAsString { get; private set; }

        /// <summary>
        /// gibt den Namen des Titels zurück
        /// </summary>
        public string LeafAsString { get; private set; }


        private IRoot root = null;
        public IRoot Root
        {
            get
            {
                if (root == null)
                {
                    root = new Artist("");
                }
                return root;
            }
            private set
            {
                root = value;
            }
        }

        private ISubRoot subroot = null;
        public ISubRoot SubRoot
        {
            get
            {
                if (subroot == null)
                {
                    subroot = new Album("");
                }
                return subroot;
            }
            private set
            {
                subroot = value;
            }
        }

        private ILeaf leaf = null;
        public ILeaf Leaf
        {
            get
            {
                return leaf;
            }
            private set
            {
                leaf = value;
            }
        }
    }
}