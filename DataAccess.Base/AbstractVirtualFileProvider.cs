using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Base
{
    public abstract class AbstractVirtualFileProvider : IVirtualFileProvider
    {
        private int[] _subRoots;
        public string[] RootNames { get; private set; }

        protected IVirtualDirectory Root;

        private readonly Dictionary<string, Dictionary<int, IVirtualFile>> _data;

        public AbstractVirtualFileProvider()
        {
            _data = new Dictionary<string, Dictionary<int, IVirtualFile>>();
        }

        public void Setup(IVirtualDirectory root)
        {
            Root = root;
        }

        public virtual void Init(string[] overrideRootnames, int[] subRoots)
        {
            _subRoots = subRoots;
            var temp = Root.GetDirectories();
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

                Dictionary<int, IVirtualFile> result = GetDataByKey(name, _subRoots);
                if (result == null) throw new ArgumentOutOfRangeException(name + " not found");

                _data.Add(name, result);
                return result;
            }
        }

        protected abstract Dictionary<int, IVirtualFile> GetDataByKey(string name, int[] subRoots);
    }
}
