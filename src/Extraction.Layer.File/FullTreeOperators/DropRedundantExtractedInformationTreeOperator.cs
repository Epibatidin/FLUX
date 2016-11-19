using DataStructure.Tree.Iterate;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Layer.File.Interfaces;
using System;
using System.Collections.Generic;

namespace Extraction.Layer.File.FullTreeOperators
{
    public class DropRedundantExtractedInformationTreeOperator : IFullTreeOperator
    {
        public void Operate(TreeByKeyAccessor treeAccessor)
        {
            var pathIterator = new PathEnumerator<FileLayerSongDo>(treeAccessor.Tree);

            while(pathIterator.MoveNext())
            {
                var pathValues = pathIterator.CollectNodeValuesOnPath(pathIterator.Current);

                DropRedundancies(pathValues);
            }
        }

        private void DropRedundancies(IList<FileLayerSongDo> elements)
        {
            // check album against artist 
            RemoveLongestMatch(elements[1].LevelValue, elements[0].LevelValue);

            // check song against artist
            RemoveLongestMatch(elements[3].LevelValue, elements[0].LevelValue);

            // check song against album 
            RemoveLongestMatch(elements[3].LevelValue, elements[1].LevelValue);
        }


        private void RemoveLongestMatch(PartedString toBeShortend, PartedString patternContainer)
        {
            bool foundMismatchAfterMatch = false;

            var pos = FindFirstMatch(toBeShortend, patternContainer);
            if (pos != null)
            {
                // du hast den anfang 
                // jetzt lauf zusammen solange weiter 
                // bis entweder ende ist 
                // oder ein mismatch 
                int length = 0;
                int i = pos.Item1;
                int j = pos.Item2;
                int start = i;
                while (true)
                {
                    if (toBeShortend[i] == patternContainer[j])
                    {
                        i++;
                        j++;
                        length++;
                        if (toBeShortend.Count == i) break;
                        if (patternContainer.Count == j) break;
                    }
                    else
                    {
                        break;
                    }
                }
                toBeShortend.RemoveRange(start, length);
            }
        }

        private Tuple<int, int> FindFirstMatch(PartedString toBeShortend, PartedString patternContainer)
        {
            for (int i = 0; i < toBeShortend.Count; i++)
            {
                for (int j = 0; j < patternContainer.Count; j++)
                {
                    if (toBeShortend[i] == patternContainer[j])
                    {
                        // found 
                        // jetzt gleichmässig zusammen iterieren 
                        return Tuple.Create(i, j);
                    }

                }
            }
            return null;
        }
    }
}
