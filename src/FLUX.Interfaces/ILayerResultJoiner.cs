using System.Collections.Generic;
using DataAccess.Interfaces;
using DataStructure.Tree;
using Extraction.Interfaces;
using FLUX.DomainObjects;

namespace FLUX.Interfaces
{
    public interface ILayerResultJoiner
    {
        TreeItem<MultiLayerDataContainer> Build(IList<IVirtualFile> virtualFiles,
            IList<ISongByKeyAccessor> songByKeyAccessors);
    }
}
