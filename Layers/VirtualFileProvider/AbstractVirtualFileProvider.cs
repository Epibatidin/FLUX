﻿using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Config;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;

namespace VirtualFileProvider
{
    public abstract class AbstractVirtualFileProvider : IVirtualFileProvider
    {
        private int[] _subRoots;
        public string[] RootNames { get; private set; }

        protected IVirtualDirectory _root;

        private Dictionary<string, Dictionary<int, IVirtualFile>> _data;

        public AbstractVirtualFileProvider()
        {
            _data = new Dictionary<string, Dictionary<int, IVirtualFile>>();
        }

        public void Setup(IVirtualDirectory root)
        {
            _root = root;
        }

        public virtual void Init(string[] overrideRootnames, int[] subRoots)
        {
            _subRoots = subRoots;
            var temp = _root.GetDirectories();
            IEnumerable<IVirtualDirectory> dirs;
            if (overrideRootnames == null)
                dirs = temp;
            else
            {
                dirs = from d in temp
                       join over in overrideRootnames on d.DirectoryName equals over
                       select d;
            }
            RootNames = dirs
                .OrderBy(c => c.DirectoryName)
                .Select(c => c.DirectoryName)
                .ToArray();
        }

        // kann man das mit nem hashset killen bzw nem dict ? 
        public Dictionary<int, IVirtualFile> this[string name]
        {
            get
            {
                if (_data.ContainsKey(name)) return _data[name];
                if (!RootNames.Contains(name)) throw new ArgumentOutOfRangeException(name + " not found");

                Dictionary<int, IVirtualFile> result = getDataByKey(name, _subRoots);
                if (result == null) throw new ArgumentOutOfRangeException(name + " not found");

                _data.Add(name, result);
                return result;
            }
        }

        protected abstract Dictionary<int, IVirtualFile> getDataByKey(string name, int[] subRoots);
    }
}
