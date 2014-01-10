using System;
using System.Collections.Generic;
using MP3Renamer.FileIO;
using MP3Renamer.Helper;
using MP3Renamer.Models;


namespace MP3Renamer.ViewModels
{
    public class FileListViewModel
    {
        private List<RuleViolation> Violations;

        //-----------------------------------------------------------------------------------------------------------------------        
        public FileListViewModel()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            Violations = new List<RuleViolation>();
            if (FileManager.Get.RootCount == 0)
                Violations.Add(new RuleViolation("FileList" , "Filelist is empty verify Path"));
        }
        
        public bool IsValid 
        {
            get 
            {
                return Violations.Count == 0;
            }
        } 

        public IEnumerable<RuleViolation> Exceptions 
        {
            get
            {
                return Violations;
            }
        }


        #region EnumSelection
        //-----------------------------------------------------------------------------------------------------------------------
        //private byte SubRootCleanSum = 0;
        //private List<byte> SubRootCleanSelValues = null;
        //private List<KeyValuePair<byte, string>> SubRootCleanBaseValues = null;
        //public MultiSelectList SubRootCleanLevelAsMSL
        ////-----------------------------------------------------------------------------------------------------------------------
        //{
        //    get
        //    {
        //        if (SubRootCleanBaseValues == null)
        //        {
        //            SubRootCleanBaseValues = createListFromEnum(typeof(SubRootCleanLevel));
        //        }
        //        return new MultiSelectList(SubRootCleanBaseValues, "Key", "Value", SubRootCleanSelValues);
        //    }    
        //}
        

        //-----------------------------------------------------------------------------------------------------------------------
        //private byte SubRootExtractSum = 0;
        //private List<byte> SubRootExtractSelValues = null;
        //private List<KeyValuePair<byte, string>> SubRootExtractBaseValues = null;
        //public MultiSelectList SubRootExtractLevelAsMSL
        ////-----------------------------------------------------------------------------------------------------------------------
        //{
        //    get
        //    {
        //        if (SubRootExtractBaseValues == null)
        //        {
        //            SubRootExtractBaseValues = createListFromEnum(typeof(SubRootExtractLevel));
        //        }
        //        return new MultiSelectList(SubRootExtractBaseValues, "Key", "Value", SubRootExtractSelValues);
        //    }
        //}

        //-----------------------------------------------------------------------------------------------------------------------
        //private byte LeafCleanSum = 0;
        //private List<byte> LeafCleanSelValues = null;
        //private List<KeyValuePair<byte, string>> LeafCleanBaseValues = null;
        //public MultiSelectList LeafCleanLevelAsMSL
        ////-----------------------------------------------------------------------------------------------------------------------
        //{
        //    get
        //    {
        //        if (LeafCleanBaseValues == null)
        //        {
        //            LeafCleanBaseValues = createListFromEnum(typeof(LeafCleanLevel));                
        //        }
        //        return new MultiSelectList(LeafCleanBaseValues, "Key", "Value", LeafCleanSelValues);
        //    }    
        //}
        


        //-----------------------------------------------------------------------------------------------------------------------
        //private byte LeafExtractSum = 0;
        //private List<byte> LeafExtractSelValues = null;
        //private List<KeyValuePair<byte, string>> LeafExtractBaseValues = null;
        //public MultiSelectList LeafExtractLevelAsMSL
        ////-----------------------------------------------------------------------------------------------------------------------
        //{
        //    get
        //    {
        //        if (LeafExtractBaseValues == null)
        //        {
        //            LeafExtractBaseValues = createListFromEnum(typeof(LeafExtractLevel));
        //        }
        //        return new MultiSelectList(LeafExtractBaseValues, "Key", "Value", LeafExtractSelValues);
        //    }
        //}



        //-----------------------------------------------------------------------------------------------------------------------
        private List<KeyValuePair<byte, string>> createListFromEnum(Type type)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            string[] EnumText = Enum.GetNames(type);
            List<byte> EnumValues = new List<byte>();
            List<KeyValuePair<byte, string>> enumlist = new List<KeyValuePair<byte, string>>();

            foreach (var text in EnumText)
            {
                byte value = (byte)Enum.Parse(type, text);
                enumlist.Add(new KeyValuePair<byte, string>(value, text));
            }
            return enumlist;
        }
        #endregion

        //-----------------------------------------------------------------------------------------------------------------------
        private bool fullprocessing = true;
        public bool FullProcessing
        //-----------------------------------------------------------------------------------------------------------------------
        {
            get
            {
                return fullprocessing;
            }
            private set
            {
                fullprocessing = value;                
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------
        internal void Proceed()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            ExecutionManager manager = new ExecutionManager();
            manager.ExecuteAllFilter();           
        }



        //-----------------------------------------------------------------------------------------------------------------------
        private byte Sum(IEnumerable<byte> list)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            byte result = 0;
            foreach (var s in list)
            {
                result += s;
            }
            return result;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private List<int> SplitInt(int value)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            var bitValues = Convert.ToString(value, 2);

            List<int> result = new List<int>();

            int length = bitValues.Length;

            for (int i = length - 1; i >= 0; i--)
            {
                if (bitValues[i] == 48)
                    result.Add(0);
                else
                    result.Add(1);
            }
            return result;
        }

       
    }
}