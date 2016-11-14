using System.Collections.Generic;
using System.Linq;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Layer.File.Config;
using Extraction.Layer.File.Operations.Interfaces;

namespace Extraction.Layer.File.Cleaner
{
    public class DropInformationInAllElementsOnThisLvlOperation : IDropInformationInAllElementsOnThisLvlOperation
    {
        private ISet<string> _whiteList;

        public DropInformationInAllElementsOnThisLvlOperation(FileLayerConfig config)
        {
            _whiteList = config.WhiteList;
        }

        public void Operate(List<PartedString> stringList)
        {
            if (stringList.Count == 1) return;
                        
            var repeatings = new Dictionary<string, int>();
            foreach (var phrase in stringList)
            {
                foreach (string word in phrase)
                {
                    if (_whiteList.Contains(word)) continue;

                    TellTheDict(repeatings ,word);                    
                }
            }

            var stringsToDelete = new HashSet<string>();
            foreach (var key in repeatings.Keys)
            {
                if (repeatings[key] >= stringList.Count())
                {
                    stringsToDelete.Add(key);
                }
            }
            foreach (var phrase in stringList)
            {
                SeekAndDestroy(stringsToDelete , phrase);
            }
        }


        private void TellTheDict(IDictionary<string,int> repeatings, string s)
        {
            if (repeatings.ContainsKey(s))
            {
                repeatings[s]++;
            }
            else
            {
                repeatings.Add(s, 1);
            }
        }
        
        private void SeekAndDestroy(ISet<string> stringsToDelete, PartedString ps)
        {
            for (int i = 0; i < ps.Count; i++)
            {
                if (!stringsToDelete.Contains(ps[i])) continue;

                ps.RemoveAt(i);
                i--;                
            }
        }
    }
}