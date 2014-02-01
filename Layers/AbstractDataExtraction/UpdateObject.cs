using Interfaces;

namespace AbstractDataExtraction
{
    public class UpdateObject
    {
        public bool Updated { get; private set; }
        public ISongByKeyAccessor Data { get; private set; }

        public void UpdateData(ISongByKeyAccessor data)
        {
            Data = data;
            Updated = true;
        }
    }

    
}
