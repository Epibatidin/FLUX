using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Extension.Test;
using Microsoft.Extensions.OptionsModel;
using Moq;
using Ploeh.AutoFixture;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;


namespace FLUX.Configuration.Tests.Config
{
    public class VirtualFileRootConfiguration : IVirtualFileRootConfiguration
    {
        public string ID { get; set; }
        public IEnumerable<string> Keys { get; set; }
    }


    public class VirtualFileConfigurationReaderTests : FixtureBase<VirtualFileConfigurationReader>
    {
        private VirtualFileAccessorSectionGroup _virtualFileAccessorSectionGroup;
        private VirtualFileRootConfiguration _firstRootConfig;
        private VirtualFileRootConfiguration _secondRootConfig;

        protected override void Customize()
        {
            var virtualFileSectionGroup = Fixture.Freeze<Mock<IOptions<VirtualFileAccessorSectionGroup>>>();
            _virtualFileAccessorSectionGroup = Fixture.Create<VirtualFileAccessorSectionGroup>();
            virtualFileSectionGroup.Setup(c => c.Value).Returns(_virtualFileAccessorSectionGroup);

            _firstRootConfig = Create<VirtualFileRootConfiguration>();
            _secondRootConfig = Create<VirtualFileRootConfiguration>();

            var configs = new List<IVirtualFileRootConfiguration>();
            configs.Add(_firstRootConfig);
            configs.Add(_secondRootConfig);

            Fixture.Register<IEnumerable<IVirtualFileRootConfiguration>>(() => configs);
        }

        [Fact]
        public void should_set_current_provider_from_configuration()
        {
            var result = SUT.ReadToDO();

            Assert.That(result.CurrentProviderName, Is.EqualTo(_virtualFileAccessorSectionGroup.General.Active));
        }

        [Fact]
        public void should_iterate_root_configs_and_create_ProviderGroupDo_from_each()
        {
            var result = SUT.ReadToDO();
    
            Assert.That(result.ProviderNames.Count, Is.EqualTo(2));
        }


        [Fact]
        public void should_set_values_for_providergroupDo_from_configurations()
        {
            var result = SUT.ReadToDO();
            
            Assert.That(result.ProviderNames[0].ProviderKey, Is.EqualTo(_firstRootConfig.ID));

            Assert.That(result.ProviderNames[0].VirtualFileProviderNames.Count, Is.EqualTo(_firstRootConfig.Keys.Count()));
        }


    }
}
