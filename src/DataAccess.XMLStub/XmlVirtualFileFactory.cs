using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DataAccess.Interfaces;
using DataAccess.XMLStub.Config;
using DataAccess.XMLStub.Serialization;

namespace DataAccess.XMLStub
{
    public class XmlVirtualFileFactory : IVirtualFileFactory
    {
        private readonly XMLSourcesCollection _xmlConfig;
        private XmlSerializer _serializer;

        public XmlVirtualFileFactory(XMLSourcesCollection xmlConfig)
        {
            _xmlConfig = xmlConfig;
            _serializer = new XmlSerializer(typeof(Root));
        }
        
        public bool CanHandleProviderKey(string providerId) => _xmlConfig.SectionName == providerId;
        public Type GetVirtualFileArrayType() => typeof(SourceItem[]);

        public IVirtualFileStreamReader GetReader(VirtualFileFactoryContext context)
        {
            return new XmlVirtualFileStreamReader();
        }

        private int[] getCounts(DirectoryInfo subroot, int[] subRoots)
        {
            var root = GetRoot(subroot);

            if (subRoots == null || subRoots.Length == 0)
                subRoots = Enumerable.Range(0, root.Groups).ToArray();

            return subRoots;
        }

        private Root GetRoot(DirectoryInfo subroot)
        {
            var index = subroot.GetFiles("Index.xml")[0];
            object dummy = null;
            using (var readStream = index.OpenRead())
            {
                dummy = _serializer.Deserialize(readStream);
            }
            return dummy as Root;
        }

        public IList<IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context)
        {
            var xmlSource = _xmlConfig.XmlSources.First(c => c.Name == context.SelectedSource);
            var root = new DirectoryInfo(xmlSource.XmlFolder);
            
            var temp = root.GetDirectories();
            IEnumerable<DirectoryInfo> dirs;
            if (context.OverrideRootnames == null)
                dirs = temp;
            else
            {
                dirs = from d in temp
                       join over in context.OverrideRootnames on d.Name equals over
                       select d;
            }

            XmlSerializer groupSerializer = new XmlSerializer(typeof(Group));
            var dict = new List<IVirtualFile>();
            foreach (var subrootDir in dirs.OrderBy(c => c.Name))
            {
                var xmlRootElement = GetRoot(subrootDir);

                IEnumerable<int> subRoots = context.SubRoots;
                if (context.SubRoots == null || context.SubRoots.Length == 0)
                    subRoots = Enumerable.Range(0, xmlRootElement.Groups);

                foreach (var subroot in subRoots)
                {
                    var file = subrootDir.GetFiles(subroot + ".xml")[0];
                    Group group = null;
                    using (var fs = file.OpenRead())
                    {
                        group = groupSerializer.Deserialize(fs) as Group;
                        if (group == null) continue;
                    }

                    foreach (var item in group.Source.Items)
                    {
                        item.VirtualPath = item.VirtualPath.Substring(xmlRootElement.RootPath.Length,
                            item.VirtualPath.Length - item.Name.Length - xmlRootElement.RootPath.Length -1 );
                        dict.Add(item);
                    }
                }
            }
            
            return dict;
        }
    }
}

