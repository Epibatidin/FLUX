using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.XMLStub.Config
{
    public class XMLSourcesCollection : IVirtualFileRootConfiguration
    {
        public string SectionName { get; set; }

        public string FolderForPersist { get; set; }

        public List<XMLSource> XmlSources { get; set; }

        public IEnumerable<string> Keys => XmlSources.Select(c => c.Name);
    }
}
