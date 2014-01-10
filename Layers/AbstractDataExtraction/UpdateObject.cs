using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractDataExtraction
{
    public class UpdateObject
    {
        public bool Updated { get; private set; }
        public object Data { get; private set; }

        public void UpdateData(object data)
        {
            Data = data;
            Updated = true;
        }
    }
}
