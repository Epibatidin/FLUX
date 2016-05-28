using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;
using Extraction.Layer.File.Config;

namespace Extraction.Layer.File.Operations
{
    public class RemoveBlackListValuesOperation : IPartedStringOperation
    {   
        readonly IBlackListConfig _config;
        public RemoveBlackListValuesOperation(FileLayerConfig config)
        {
            _config = config.BlackList;
        }

        public PartedString Operate(PartedString original)
        {
            for (int i = 0; i < original.Count; i++)
            {
                if (_config.Contains(original[i]))
                {
                    original.RemoveAt(i);
                    i--;
                }
            }
            return original;
        }
    }
}