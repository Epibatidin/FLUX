using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MP3Renamer.DataContainer;

namespace MP3Renamer.Models.DataContainer
{
    public class RawDataManager : IStringPartManager
    {
        private char[] SplitByWithoutDot = new char[] {',', ' ', '-', '_' };
        private char[] SplitByWithDot = new char[] { '.', ',', ' ', '-', '_' };

        public bool SplitByDot{get; set;}


        public string BaseData { get; private set; }
        
        //-----------------------------------------------------------------------------------------------------------------------
        public RawDataManager(string RawData)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            this.RawData = RawData;            
            this.RawDataParts = Split(RawData);
        }



        //-----------------------------------------------------------------------------------------------------------------------
        private string rawdata = "";
        public string RawData
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                return rawdata;
            }
            set
            {
                rawdata = value;
            }
        }

   
        public List<string> RawDataParts { get; set; }



        public void RemoveElementAt(int Position)
        {
            RawDataParts.RemoveAt(Position);
        }



        //-----------------------------------------------------------------------------------------------------------------------
        public List<string> Split(string input)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            List<string> result = new List<string>();
            if (input == null) return result;
            string[] parts;

            if (SplitByDot)
                parts = input.Split(SplitByWithDot);
            else
                parts = input.Split(SplitByWithoutDot);

            foreach (string part in parts)
            {
                if (!String.IsNullOrWhiteSpace(part))
                {
                    result.Add(part.ToLower());
                }
            }
            return result;
        }



        //-----------------------------------------------------------------------------------------------------------------------
        public string Join(char JoinWith)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            StringBuilder builder = new StringBuilder();
            var input = RawDataParts;

            if (input != null && input.Count() > 0)
            {
                for(int i = 0 ; i< input.Count(); i++)
                {
                    if(!String.IsNullOrWhiteSpace(input[i]))
                    {
                        builder.Append(input[i]);
                        builder.Append(JoinWith);
                    }
                }
                builder.Remove(builder.Length-1, 1);
            }        
            return builder.ToString();
        }
    }
}



////-----------------------------------------------------------------------------------------------------------------------
//public void ModifyValue(int ID, string Value)
////-----------------------------------------------------------------------------------------------------------------------
//{
//    if(rawDataParts.ContainsKey(ID))
//    {
//        if (String.IsNullOrWhiteSpace(Value))
//        {
//            rawDataParts.Remove(ID);
//        }
//        else
//        {
//            rawDataParts[ID] = Value;
//        }
//    }
//}

////-----------------------------------------------------------------------------------------------------------------------
//public List<string> RawDataParts
////-----------------------------------------------------------------------------------------------------------------------
//{
//    get
//    {
//        var s = History.LastOrDefault();

//        if (s == null)
//            s = new List<string>();
//        return s;            
//    }
//    set
//    {
//        if (value != null)
//        {
//            History.Add(value);
//        }
//    }            
//}
//private Dictionary<int, string> UpdateRawData1(Dictionary<int,string> OldValues, List<string> NewValues)
//{
//    // diese funktion updateted das dict ohne dabei die ehemalige position zu verlieren
//    // so kann immer nachvollzogen werden an welcher stelle welches datum wann stand

//    // ich kann über das dict iterieren da ich nur werte entferne 
//    // dh das dict ist immer größer oder gleich groß wie newvalues

//    Dictionary<int, string> result = new Dictionary<int, string>();


//    foreach (var key in OldValues.Keys)
//    {
//        var s = OldValues[key];
//        foreach (var val in NewValues)
//        {
//            if (s.Contains(val))
//            {
//                result.Add(key,val);
//                break;
//            }
//        }
//    }
//    return result;
//}

//private Dictionary<int, string> UpdateRawDataJoin(Dictionary<int, string> OldValue, List<string> UpdateValues)
//{
//    var result = (from raw in OldValue
//                  join v in UpdateValues on raw.Value equals v
//                  select new { raw.Key, v }).Distinct().ToDictionary(c => c.Key, c => c.v);

//    return result;
//}


//private Dictionary<int, string> UpdateRawDataOrdered(Dictionary<int, string> OldValue, List<string> UpdateValues)
//{
//    var sortedKeys = OldValue.Keys.OrderBy(c => c).Select(c => c).ToList();
//    int NewValuePos = 0;

//    // oldvalue ist entweder größer oder gleich updatevalues
//    // 

//    for (int key = 0; key < sortedKeys.Count; key++)
//    {
//        if (NewValuePos < UpdateValues.Count)
//        {
//            string OldVal = OldValue[sortedKeys[key]];
//            string UpdateValue = UpdateValues[NewValuePos];

//            if (OldVal.Contains(UpdateValue))
//            {
//                OldValue[sortedKeys[key]] = UpdateValues[NewValuePos];
//                NewValuePos++;
//            }
//        }
//    }
//    return OldValue;
//}



//private Dictionary<int, string> rawDataParts;
//public List<string> RawDataParts
//{
//    get
//    {
//        return rawDataParts.Values.ToList<string>();
//    }
//    set
//    {
//        if (rawDataParts != null)
//        {
//            // TODO
//            // distinct nicht suaber aber funktionell 
//            // überdenken 
//            // achtung doppelte wörter joined er in beide dicts => kolision
//            // manuelle alternative ?! 

//            // funzt immernoch nicht weil 01. halt nicht auf 01 gejoined wird was auch klar ist 
//            // dh die alten werte sind zahlreicher es wird niemals hinzugefügt 
//            rawDataParts = UpdateRawDataOrdered(rawDataParts, value);

//        }
//        else
//        {
//            rawDataParts = new Dictionary<int, string>();
//            int i = 0;
//            foreach (string s in value)
//            {
//                if (!String.IsNullOrWhiteSpace(s))
//                {
//                    rawDataParts.Add(i, s);
//                    i++;
//                }
//            }
//        }
//    }
//}

//private Dictionary<int, string> rawDataParts;