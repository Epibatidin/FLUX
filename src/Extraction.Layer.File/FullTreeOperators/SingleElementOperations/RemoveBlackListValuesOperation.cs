using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;
using Extraction.Layer.File.Config;

namespace Extraction.Layer.File.SimpleOperators
{
    public class RemoveBlackListValuesOperation : IPartedStringOperation
    {   
        readonly IBlackListConfig _config;
        public RemoveBlackListValuesOperation(FileLayerConfig config)
        {
            _config = config.BlackList;
        }

        public void Operate(PartedString original)
        {
            for (int i = 0; i < original.Count; i++)
            {
                if (_config.Contains(original[i]))
                {
                    original.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}