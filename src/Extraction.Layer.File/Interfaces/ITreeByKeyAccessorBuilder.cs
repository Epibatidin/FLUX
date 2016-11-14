using System.Collections.Generic;
using DataAccess.Interfaces;

namespace Extraction.Layer.File.Interfaces
{
    public interface ITreeByKeyAccessorBuilder
    {
        TreeByKeyAccessor Build(IEnumerable<IVirtualFile> data);
        void BuildKeyMapping(TreeByKeyAccessor result);
    }
}