using System;
using System.Collections.Generic;
using System.Linq;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Interfaces;
using MP3Renamer.Helper;

namespace MP3Renamer.Filter.Extraction.SubrootWorkunit
{
    public class YearExtractor : IExecuteable, ISubrootWorkunit
    {
        public ISubRoot Workunit { get; set; }

        public void Execute()
        {
            List<short> allNumbers = new List<short>();
            var parts = Workunit.StringManager.RawDataParts;
            
            for (int i = 0; i < parts.Count(); i++)
            {
                var numbers = NumberExtractor.ExtractNumbers(parts[i]);
                if (numbers != null && numbers.Count() != 0)
                {
                    allNumbers.AddRange(numbers);
                    parts.RemoveAt(i);
                    i--;
                }
            }
            
            Workunit.StringManager.RawDataParts = parts;
            
            Int16? year = allNumbers.OrderByDescending(c => c).FirstOrDefault();
            
            // ordne die zahlen der größe nach und hoffe das die größe zahl das jahr ist 
            
            if (year.HasValue)                
            {
                Workunit.Year = year.Value;                
            }
        }

        
    }
}