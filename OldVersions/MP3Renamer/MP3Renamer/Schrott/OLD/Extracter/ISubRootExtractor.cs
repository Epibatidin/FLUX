using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MP3Renamer.Models.DataContainer.EntityInterfaces;

namespace MP3Renamer.Models.Extraction.Extracter
{
    public interface ISubRootExtractor
    {
        ISubRoot Extract(ISubRoot subroot);
    }
}
