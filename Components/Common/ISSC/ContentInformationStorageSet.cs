
using System.Collections.Generic;
namespace Common.ISSC
{
    public class ContentInformationStorageSet : InformationStorageSet
    {
        public ContentInformationStorageSet()
        {
         
        }

        public ContentInformationStorageSet(Dictionary<int, object> values)
        {
            this.ValuesDict = values;
        }

        public T getByKey<T>(CIIS Key)
        {
            return base.getByKey<T>((int)Key);
        }

        public void setByKey<T>(CIIS key, T value)
        {
            base.setByKey<T>((int)key, value);
        }
    }
}
