using NUnit.Framework;
using VirtualFileProvider.Config;
using VirtualFileProvider.XML;

namespace VirtualFileProvider.Tests.Config
{
    [TestFixture]
    public class XMLVirtualFileProviderTests
    {
        private XMLSourcesCollection _collection;

        [SetUp]
        public void setup()
        {
            var temp = ConfigurationHelper.GetConfiguration("VirtualFileAccessor").GetSectionGroup("VirtualFileAccessor") as VirtualFileAccessorSectionGroup;
            _collection = temp.Sources.XML;

        }

        [Test]
        public void ShouldFindFolderSourceByKey()
        {
            string key = "XMLSource";

            var folderSource = _collection.Item(key);

            Assert.That(folderSource, Is.Not.Null);
            Assert.That(folderSource, Is.TypeOf<XMLSource>());
            Assert.That(folderSource.Key, Is.EqualTo(key));
        }


        [Test]
        public void ShouldReturnNullIfWrongKey()
        {
            string key = "cxvbgjdxgh dghjd fg d";

            var folderSource = _collection.Item(key);

            Assert.That(folderSource, Is.Null);
        }


        [Test]
        public void ShouldBuildVirtualFileProvider()
        {
            string key = "XMLSource";

            var provider = _collection.Create(key);

            Assert.That(provider, Is.Not.Null);
            Assert.That(provider, Is.TypeOf<XMLVirtualFileProvider>());
        }
    }
}
