using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;
using VirtualFileProvider.XML.Serialization;

namespace VirtualFileProvider.XML
{
    public class XMLVirtualFileProvider : AbstractVirtualFileProvider
    {
        private int[] getCounts(IVirtualDirectory subroot, int[] subRoots)
        {
            var index = subroot.GetFile("Index.xml");
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            var o = ser.Deserialize(index.Open());
            var root =  o as Root;

            if (subRoots == null || subRoots.Length == 0)
                subRoots = Enumerable.Range(0, root.Groups).ToArray();

            return subRoots;
        }
        
        protected override Dictionary<int, IVirtualFile> getDataByKey(string name, int[] subRoots)
        {
            var subrootDir = _root.GetDirectory(name);
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
    }
}

