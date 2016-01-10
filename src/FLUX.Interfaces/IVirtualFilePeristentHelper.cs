using System;
using System.Collections.Generic;
using DataAccess.Interfaces;

namespace FLUX.Interfaces
{
    public interface IVirtualFilePeristentHelper
    {
        void SaveSource(IDictionary<int, IVirtualFile> sourceData);
        void SaveProviderName(string name);
        void SaveActiveGrp(string name);

        IDictionary<int, IVirtualFile> LoadSource(Type virtualFileConcreteType);
        string LoadProviderName();
        string LoadActiveGrp();

    }
}
