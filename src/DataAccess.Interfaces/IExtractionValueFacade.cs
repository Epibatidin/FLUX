using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IExtractionValueFacade
    {
        int Id { get; set; }

        IList<Tuple<string, string>> ToValues();
    }
}
