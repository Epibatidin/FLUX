using NUnit.Framework;
using VirtualFileProvider.Config;
using VirtualFileProvider.Directory;

namespace VirtualFileProvider.Tests.Config
{
    [TestFixture]
    public class DirectorySourcesCollectionTests
    {
        private DirectorySourcesCollection _collection;

        [SetUp]
        public void setup()
        {
            var adapter = VirtualFileAccessorSectionGroup.Get(new StaticConfigurationLocator("VirtualFileAccessor"));
            _collection = adapter.Sources.Directory;
        }

        [Test]
        public void ShouldFindFolderSourceByKey()
        {
            string key = "MOQ";

            var folderSource = _collection.Item(key);

            Assert.That(folderSource, Is.Not.Null);
            Assert.That(folderSource, Is.TypeOf<FolderSource>());
            Assert.That(folderSource.Key, Is.EqualTo(key));
        }


        [Test]
        public void ShouldReturnNullIfWrongKey()
        {
            string key = "cxvbgjdxgh dghjd fg d";

            var folderSource = _collection.Item(key);

            Assert.That(folderSource, Is.Null);
        }
    }
}
