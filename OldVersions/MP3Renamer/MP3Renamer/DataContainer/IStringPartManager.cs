using System.Collections.Generic;

namespace MP3Renamer.DataContainer
{
    public interface IStringPartManager
    {
        List<string> RawDataParts { get; set; }

        void RemoveElementAt(int Position);

        string BaseData { get; }

        string RawData { get; set; }

        List<string> Split(string input);

        string Join(char JoinWith);

        bool SplitByDot{get; set;}

        //void ModifyValue(int id, string value);

    }
}
