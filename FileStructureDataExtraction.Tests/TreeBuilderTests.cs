using System.Collections.Generic;
using Interfaces.VirtualFile;
using NUnit.Framework;
using TestHelpers;

namespace FileStructureDataExtraction.Tests
{
    [TestFixture]
    public class TreeBuilderTests
    {
        private TreeBuilder _treeBuilder;
        private List<IVirtualFile> _files;

        [SetUp]
        public void Setup()
        {
            _files = new List<IVirtualFile>();
            _treeBuilder = new TreeBuilder();

        }
        private void Add(int id, string virtualPath, string fName)
        {
            _files.Add(new VirtualFileDummy() { ID = id, Name = fName, VirtualPath = virtualPath });
        }

        [Test]
        public void should_build_tree_through_directory_delimiter()
        {
            Add(1, "Artist\\Album1", "Song1" );
            Add(2, "Artist\\Album2", "Song1");

            var result = _treeBuilder.Build(_files);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Value.Artist, Is.EqualTo("Artist"));
            Assert.That(result[0].Value.Album, Is.EqualTo("Album1"));
            Assert.That(result[1].Value.Album, Is.EqualTo("Album2"));
        }


        [Test]
        public void should_always_add_cd()
        {
            Add(1, "Artist\\Album1\\CD1", "Song1");
            Add(2, "Artist\\Album2", "Song1");

            var result = _treeBuilder.Build(_files);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Value.Artist, Is.EqualTo("Artist"));
            Assert.That(result[0].Value.Album, Is.EqualTo("Album1"));
            Assert.That(result[1].Value.Album, Is.EqualTo("Album2"));

            Assert.That(result[0][0].Value.Album, Is.EqualTo("CD1"));
            Assert.That(result[1][0].Value.Album, Is.EqualTo("CD1"));

            Assert.That(result[0][0][0].Value.SongName, Is.EqualTo("Song1"));
            Assert.That(result[0][0][0].Value.ID, Is.EqualTo(1));
            Assert.That(result[1][0][0].Value.SongName, Is.EqualTo("Song1"));
            Assert.That(result[1][0][0].Value.ID, Is.EqualTo(2));
        }

        [Test]
        public void should_only_set_ids_for_songs()
        {
            Add(27, "Artist\\Album1\\CD1", "Song1");
            var result = _treeBuilder.Build(_files);

            Assert.That(result.Value.ID, Is.EqualTo(0));
            Assert.That(result[0].Value.ID, Is.EqualTo(0));
            Assert.That(result[0][0].Value.ID, Is.EqualTo(0));
            Assert.That(result[0][0][0].Value.ID, Is.EqualTo(27));
        }
    }
}
