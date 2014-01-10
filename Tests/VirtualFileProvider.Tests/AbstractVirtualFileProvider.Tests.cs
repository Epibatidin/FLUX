using System;
using System.Collections.Generic;
using System.IO;
using Common.FileSystem;
using Interfaces.FileSystem;
using NUnit.Framework;
using Rhino.Mocks;
using TestHelpers;

namespace VirtualFileProvider.Tests
{
    public class AbstractVirtualPoviderSub : AbstractVirtualFileProvider
    {
        protected override Dictionary<int, Interfaces.VirtualFile.IVirtualFile> getDataByKey(string name, int[] subRoots)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class AbstractVirtualFileProviderTests
    {
        private IVirtualDirectory _root;
        private AbstractVirtualFileProvider _provider;

        [SetUp]
        public void Setup()
        {
            _root = MockRepository.GenerateMock<IVirtualDirectory>();
            _provider = new AbstractVirtualPoviderSub();
            _provider.Setup(_root);
        }

        [Test]
        public void ShouldInitProviderWithoutDebugRestrictions()
        {
            string pattern = "DN{0}";

            FileSystemHelper.Pattern = pattern;
            int dirCount = 5;
            _root.Stub(c => c.GetDirectories())
                .Return(FileSystemHelper.CreateDirList(dirCount));

            _provider.Init(null, null);

            _root.AssertWasCalled(c => c.GetDirectories());
            Assert.That(_provider.RootNames.Length, Is.EqualTo(dirCount));

            for (int i = 0; i < dirCount; i++)
            {
                Assert.That(_provider.RootNames[i], Is.EqualTo(string.Format(pattern,i)));
            }
        }

        [Test]
        public void ShouldInitProviderWithDebugRestrictions()
        {
            string pattern = "DN{0}";

            FileSystemHelper.Pattern = pattern;
            int dirCount = 5;
            _root.Stub(c => c.GetDirectories())
                .Return(FileSystemHelper.CreateDirList(dirCount));

            int[] poses = new[] {0, 2, 5};
            string[] folders = new string[3];
            for (int i = 0; i < poses.Length; i++)
            {
                folders[i] = String.Format(pattern, poses[i]);
            }

            _provider.Init(folders, null);
            _root.AssertWasCalled(c => c.GetDirectories());
            Assert.That(_provider.RootNames.Length, Is.EqualTo(2));

            for (int i = 0; i < 2; i++)
            {
                Assert.That(_provider.RootNames[i], Is.EqualTo(folders[i]));
            }
        }



       
    }

    [TestFixture, Explicit]
    public class AbstractVirtualFileProviderIntegration
    {
        private IVirtualDirectory _root;
        private AbstractVirtualFileProvider _provider;

        [SetUp]
        public void setup()
        {
            var dir = new DirectoryInfo(@"E:\Develop\FLUX\DATA\XMLSourceProviderSource");
            Assert.That(dir.Exists, Is.True);

            _root = new RealDirectory(dir);
            _provider = new AbstractVirtualPoviderSub();
            _provider.Setup(_root);
        }

        [Test]
        public void ShouldSupportDirectoryNameArray()
        {
            var fol = new string[2];
            fol[0] = "Metallica";
            fol[1] = "Amduscia";
            _provider.Init(fol, null);

            Assert.That(_provider.RootNames.Length, Is.EqualTo(2));

        }


    }

}
