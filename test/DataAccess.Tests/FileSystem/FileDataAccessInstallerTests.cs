using DataAccess.FileSystem.Config;
using Extension.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;

namespace DataAccess.Tests.FileSystem
{
    public class FileDataAccessInstallerTests : FixtureBase<FileDataAccessInstaller>
    {
        [Fact]
        public void should_register_options_service()
        {
            var service = new Mock<IServiceCollection>();
            
            SUT.RegisterServices(service.Object);
        }

    }
}
