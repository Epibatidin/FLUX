using System.Configuration;
using System.IO;
using System.Linq;
using Interfaces.Config;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;
using NUnit.Framework;
using TestHelpers;
using VirtualFileProvider.Config;

//  http://robseder.wordpress.com/articles/the-complete-guide-to-custom-configuration-sections/

namespace VirtualFileProvider.Tests.Config
{
    [TestFixture]
    public class VirtualFileAccessorSectionGroupTests
    {
        private VirtualFileAccessorSectionGroup _vfa;

        [SetUp]
        public void Setup()
        {
            VirtualFileAccessorSectionGroup.Reset();
            _vfa =  VirtualFileAccessorSectionGroup.Get(new StaticConfigurationLocator("VirtualFileAccessor"));
            Assert.That(_vfa, Is.Not.Null);
        }


        [Test]
        public void ShouldReadGeneralSection()
        {
            var global = _vfa.General;

            Assert.That(global, Is.Not.Null);

            Assert.That(global.Active, Is.EqualTo("XMLSource"));
        }
        

        [Test]
        public void ShouldReadSources()
        {
            var sources = _vfa.Sources;

            Assert.That(sources , Is.Not.Null);
        }

        [Test]
        public void ShouldReadXMLSources()
        {
            var xml = _vfa.Sources.XML;

            Assert.That(xml, Is.Not.Null);
            Assert.That(xml.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldReadDirectorySources()
        {
            var dir = _vfa.Sources.Directory;

            Assert.That(dir, Is.Not.Null);
            Assert.That(dir.Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldReadDebugSection()
        {
            var debug = _vfa.Debug;
            
            Assert.That(debug, Is.Not.Null);
        }

        [Test]
        public void ShouldReadRootNamesInDebugSection()
        {
            var roots = _vfa.Debug.RootNames;
            Assert.That(roots.Length, Is.EqualTo(1));
            Assert.That(roots[0], Is.EqualTo("Amduscia"));
        }

        [Test]
        public void ShouldReadSubRootPosesInDebugSection()
        {
            var subposes = _vfa.Debug.SubRootPos;

            Assert.That(subposes.Length, Is.EqualTo(3));
        }


        [Test]
        public void ShouldBeIterateAble()
        {
            int count = 0;
            while (_vfa.Sources.MoveNext())
            {
                Assert.That(_vfa.Sources.Current, Is.InstanceOf<IVirtualFileProviderFactory>());
                count++;
            }
            Assert.That(count, Is.EqualTo(2));
        }

    }
}
