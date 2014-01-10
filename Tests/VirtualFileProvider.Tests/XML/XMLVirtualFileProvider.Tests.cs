using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Interfaces.FileSystem;
using NUnit.Framework;
using Rhino.Mocks;
using VirtualFileProvider.XML;

namespace VirtualFileProvider.Tests.XML
{
    [TestFixture]
    public class XMLVirtualFileProviderTests
    {
        private XMLVirtualFileProvider _provider;



        [SetUp]
        public void SetUp()
        {
            //_root = MockRepository.GenerateMock<IVirtualDirectory>();
            //_provider = new XMLVirtualFileProvider(_root);
        }

        
        [Test]
        public void ShouldGetAllFolderNamesWithoutOverride()
        {
            

        }
    }
}
