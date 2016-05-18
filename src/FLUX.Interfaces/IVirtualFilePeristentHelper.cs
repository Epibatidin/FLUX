using System;
using System.Collections.Generic;
using DataAccess.Interfaces;

namespace FLUX.Interfaces
{
    public interface IVirtualFilePeristentHelper
    {
        void SaveSource(IList<IVirtualFile> sourceData);
        void SaveProviderName(string name);
        void SaveActiveGrp(string name);

        IList<IVirtualFile> LoadSource(Type virtualFileConcreteType);
        string LoadProviderName();
        string LoadActiveGrp();

    }
}
