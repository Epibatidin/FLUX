using System.IO;
using System.Xml;

namespace Extension.Configuration
{
    public interface IXmlReaderInitializeable
    {
        void Load(XmlReader stream);
    }

}
