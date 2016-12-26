using DataAccess.Interfaces;
using FLUX.DomainObjects;
using System.Collections.Generic;

namespace FLUX.Interfaces.Web
{
    public interface IPostbackSongBuilder
    {
        IList<IExtractionValueFacade> Flatten(PostbackTree postBackTree);
    }
}
