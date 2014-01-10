using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.StringManipulation;

namespace Common.ISSC
{
    public class CleanSourceInformationStorageSet : InformationStorageSet
    {
        public CleanSourceInformationStorageSet()
        {

        }


        public PartedString getByKey(CleanIIS Key)
        {
            return base.getByKey<PartedString>((int)Key);
        }

        public void setByKey(CleanIIS key, PartedString value)
        {
            base.setByKey<PartedString>((int)key, value);
        }

    }
}
