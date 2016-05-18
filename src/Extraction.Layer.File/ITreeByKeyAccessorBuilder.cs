using System.Collections.Generic;
using DataAccess.Interfaces;

namespace Extraction.Layer.File
{
    public interface ITreeByKeyAccessorBuilder
    {
        TreeByKeyAccessor Build(IEnumerable<IVirtualFile> data);
    }
}