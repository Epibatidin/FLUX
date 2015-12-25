﻿using System.IO;

namespace DataAccess.Interfaces
{
    public interface IVirtualFile
    {
        int ID { get; }

        string Name {get;}

        string VirtualPath {get;}

        Stream Open(); 
    }
}
