using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IExtractionValueFacade
    {
        int Id { get; set; }

        IEnumerable<Tuple<string, string>> ToValues();
    }
}
