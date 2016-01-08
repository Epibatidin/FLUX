using System;
using System.Collections.Generic;
using DataAccess.Interfaces;

namespace FLUX.Interfaces
{
    public interface IVirtualFilePeristentHelper
    {
        void SaveSource(IDictionary<string, IVirtualFile> sourceData);
        IDictionary<string, IVirtualFile> LoadSource(Type virtualFileConcreteType);
    }
}
