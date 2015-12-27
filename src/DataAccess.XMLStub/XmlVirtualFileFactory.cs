using System.Collections.Generic;
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

        public XmlVirtualFileFactory(XMLSourcesCollection xmlConfig)
        {
            _xmlConfig = xmlConfig;
        }

        public bool CanHandleProviderKey(string providerId) => _xmlConfig.ID == providerId;

        private int[] getCounts(IVirtualDirectory subroot, int[] subRoots)
        {
            var index = subroot.GetFile("Index.xml");
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            var o = ser.Deserialize(index.Open());
            var root = o as Root;

            if (subRoots == null || subRoots.Length == 0)
                subRoots = Enumerable.Range(0, root.Groups).ToArray();

            return subRoots;
        }

        public Dictionary<int, IVirtualFile> BuildVirtualFiles(IVirtualDirectory root, string name, int[] subRoots)
        {
            var subrootDir = root.GetDirectory(name);
            subRoots = getCounts(subrootDir, subRoots);

            XmlSerializer ser = new XmlSerializer(typeof(Group));
            var dict = new Dictionary<int, IVirtualFile>();
            foreach (var subroot in subRoots)
            {
                var fs = subrootDir.GetFile(subroot + ".xml").Open();
                if (fs == null) continue;

                var group = ser.Deserialize(fs) as Group;
                if (group == null) continue;

                foreach (var item in group.Source.Items)
                {
                    dict.Add(item.ID, item);
                }
            }
            return dict;
        }

        

        public IDictionary<int, IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}

