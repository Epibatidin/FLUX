//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Extraction.Layer.File.Helper;

//namespace Extraction.Layer.File.Extraction
//{
//    public class YearExtractor
//    {
//        public void Execute(FileLayerSongDo flsdo)
//        {
//            List<short> allNumbers = new List<short>();
//            var parts = flsdo.LevelValue;

//            for (int i = 0; i < parts.Count(); i++)
//            {
//                var numbers = NumberExtractor.ExtractNumbers(parts[i]);
//                if (numbers != null && numbers.Count() != 0)
//                {
//                    allNumbers.AddRange(numbers);
//                    parts.RemoveAt(i);
//                    i--;
//                }
//            }

//            Int16? year = allNumbers.OrderByDescending(c => c).FirstOrDefault();
//            // ordne die zahlen der größe nach und hoffe das die größte zahl das jahr ist 

//            if (year.HasValue)
//                flsdo.Year = year.Value;
//        }
//    }
//}