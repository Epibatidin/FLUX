using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;
using NUnit.Framework;
using Rhino.Mocks;
using VirtualFileProvider.Directory;

namespace VirtualFileProvider.Tests.Directory
{
    [TestFixture]
    public class DirectoryVirtualFileProviderTests
    {
        private DirectoryVirtualFileProvider _provider;
        private IVirtualDirectory _root;
        
        [SetUp]
        public void setup()
        {
            _provider = new DirectoryVirtualFileProvider();
            _root = MockRepository.GenerateMock<IVirtualDirectory>();

            _provider.Setup(_root);
        }

        private IVirtualDirectory createDirectoryWithFiles(int name, int filesCount)
        {
            List<IVirtualFile> files = new List<IVirtualFile>();
            
            var dir = new MockDir()
            {
                DirectoryName = name.ToString(),
                FileCount = filesCount
            };
            return dir;
        }

        private void initDefaultStruct(int folderCount)
        {
            List<IVirtualDirectory> alben = new List<IVirtualDirectory>();
            for (int i = 0; i < folderCount; i++)
            {
                alben.Add(createDirectoryWithFiles(i, 2));
            }

            var artist = MockRepository.GenerateMock<IVirtualDirectory>();
            artist.Stub(c => c.GetDirectories()).Return(alben);
            artist.Stub(c => c.DirectoryName).Return("Name");
            List<IVirtualDirectory> artists = new List<IVirtualDirectory>();
            artists.Add(artist);

            _root.Stub(c => c.GetDirectories()).Return(artists);
            _root.Stub(c => c.GetDirectory(Arg<string>.Is.Anything)).Return(artist);

        }

        [Test]
        public void ShouldFindAllFoldersIfNoRestrictions()
        {
            initDefaultStruct(3);
            _provider.Init(null, null);
            var result = _provider["Name"];

            Assert.That(result.Keys.Count, Is.EqualTo(6));

            Assert.That(result.Keys.Min(), Is.EqualTo(1001));
            Assert.That(result.Keys.Max(), Is.EqualTo(3002));
        }

        [Test]
        public void ShouldFindFilesWithRestrictions()
        {
            initDefaultStruct(6);
            _provider.Init(null, new [] { 1, 4 });
            var result = _provider["Name"];

            Assert.That(result.Keys.Count, Is.EqualTo(4));

            Assert.That(result.Keys.Min(), Is.EqualTo(2001));
            Assert.That(result.Keys.Max(), Is.EqualTo(5002));
        }




    }
}
