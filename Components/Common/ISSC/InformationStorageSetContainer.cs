using Interfaces;

namespace Common.ISSC
{
    public class InformationStorageSetContainer : IUpdateable
    {
        public DataStatus Status;
   
        public FileInformationStorageSet FileIIS { get; set; }

        public ContentInformationStorageSet ContentIIS { get; set; }
        public ContentInformationStorageSet TagContentIIS { get; set; }

        public CleanSourceInformationStorageSet CleanIIS { get; set; }

        public void Update(object NewValues)
        {
            var issc = NewValues as InformationStorageSetContainer;

            if (issc != null)
            {
                foreach (var item in issc.FileIIS.GetKeys())
                {
                    var value = issc.FileIIS.getByKey(item);
                    if(value != null)
                        FileIIS.setByKey(item, value);
                }
            }
            Status = DataStatus.Updated;
        }
    }
}
