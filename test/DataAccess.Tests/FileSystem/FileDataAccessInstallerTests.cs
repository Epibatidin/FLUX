using DataAccess.FileSystem.Config;
using Extension.Test;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests.FileSystem
{
    public class FileDataAccessInstallerTests : FixtureBase<FileDataAccessInstaller>
    {
        protected override FileDataAccessInstaller CreateSUT()
        {
            return new FileDataAccessInstaller();
        }

        [Test]
        public void should_register_options_service()
        {
            var service = new Mock<IServiceCollection>();

            SUT.RegisterServices(service.Object);
        }

        
    }
}
