using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP3LibIntegrater
{
    public abstract class AbstractInstance
    {
        protected abstract string[] chopGivenInformation();
        private string[] ri = null;

        protected string[] relevantInformation
        {
            get
            {
                if (ri == null)
                    ri = chopGivenInformation();
                return ri;
            }
            set
            {
                ri = value;
            }
        }
        public List<AbstractInstance> Entities { get; protected set; }
        protected AbstractInstance oneInstanceUp = null;


        protected void removeKnownInformation()
        {
            // die informationen die vorher extrahiert wurden sind wahrscheinlich valid
            // erst muss das jahr raus 
            //if (oneInstanceUp != null)
            //{
            //    string knwonInformation = Algs.joinString(oneInstanceUp.relevantInformation).ToLower();

            //    string info = Algs.joinString(relevantInformation).ToLower();

            //    // so jetzt muss ich in info nach knowninfo suchen und es genau einmal rauschmeißen

            //    relevantInformation = SeekAndDestroy(knwonInformation, info);

            //    oneInstanceUp.removeKnownInformation();
            //}
            clearKnownInformation(this,oneInstanceUp);


            if (this.Entities != null)
            {
                foreach (var s in this.Entities)
                {
                    clearKnownInformation(oneInstanceUp ,s);
                }
            }

           
        }


        //das problem ist das immer nur nachbar instanzen verglichen werden 
        protected void clearKnownInformation(AbstractInstance root, AbstractInstance sub)
        {
            if (root == null || sub == null) return;

            sub.relevantInformation = SeekAndDestroy
                (Algs.joinString(root.relevantInformation),
                Algs.joinString(sub.relevantInformation));
        }


        private string[] SeekAndDestroy(string searchFor, string searchIn)
        {
            int start = searchIn.IndexOf(searchFor);
            string dummy = searchIn;

            if (start >= 0)
                if(searchIn.Length != searchFor.Length)
                    searchIn = searchIn.Remove(start, searchFor.Length);


            return Algs.Split(searchIn);
        }


        protected abstract void extractInstanceSpecificNumber();



        protected void removeEquals(List<string> EqualSet)
        {
            //if (relevantInformation == null) relevantInformation = getChoppedInfo;
            var s = relevantInformation;
            removeKnownInformation();

            if (EqualSet == null || EqualSet.Count == 0) return;

            for (int i = 0; i < relevantInformation.Length; i++)
            {
                if (EqualSet.Contains(relevantInformation[i].ToLower() ))
                {
                    if(!Algs.WhiteList.Contains(relevantInformation[i]))
                        relevantInformation[i] = String.Empty;
                }
            }
            s = relevantInformation;
        }


        /// <summary>
        /// Sucht anhand von Duplicationen nach gleichnissen 
        /// jedes vielfach vorkommende wort ist kandidat aber nur wenn 
        /// die duplicate so häufig sind wie die anzahl der songs wird es zum entfernen freigegeben
        /// mitanderen Wort: alles was ich jedem vorkommt fliegt raus
        /// </summary>
        /// <param name="instanceList"> die Liste der auf duplicate zu kontrollierende Liste</param>
        /// <returns></returns>
        protected List<string> searchRepeatingInformation(List<AbstractInstance> instanceList)
        {
            if (instanceList.Count < 2) return null;

            Dictionary<string, short> allWordCount = new Dictionary<string, short>();

            foreach (var instance in instanceList)  // für jedes Lied
            {
                foreach (string word in instance.relevantInformation) // nimm alle wörter
                {
                    if (allWordCount.ContainsKey(word)) // füge sie dem dict hinzu schon drin ?
                    {
                        allWordCount[word]++; // dann erhöhe den counter
                    }
                    else
                    {
                        allWordCount.Add(word, 1); // sonst füge hinzu
                    }
                }
            }

            List<string> equals = new List<string>();

            foreach (string word in allWordCount.Keys)
            {
                if (allWordCount[word] >= instanceList.Count)
                {
                    equals.Add(word.ToLower());
                }
            }
            return equals;
        }
    }
}