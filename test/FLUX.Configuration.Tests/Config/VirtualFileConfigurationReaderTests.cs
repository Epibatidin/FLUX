using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Extension.Test;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace FLUX.Configuration.Tests.Config
{
    public class VirtualFileRootConfiguration : IVirtualFileRootConfiguration
    {
        public string SectionName { get; set; }
        public IEnumerable<string> Keys { get; set; }
    }


    public class VirtualFileConfigurationReaderTests : FixtureBase<VirtualFileConfigurationReader>
    {
        private VirtualFileAccessorSectionGroup _virtualFileAccessorSectionGroup;
        private VirtualFileRootConfiguration _firstRootConfig;
        private VirtualFileRootConfiguration _secondRootConfig;
        
        protected override void Customize()
        {
            

            _firstRootConfig = Create<VirtualFileRootConfiguration>();
            _secondRootConfig = Create<VirtualFileRootConfiguration>();

           

            //Fixture.Register<IEnumerable<IVirtualFileRootConfiguration>>(() => configs);
        }
        
        protected override VirtualFileConfigurationReader CreateSUT()
        {
            var virtualFileSectionGroup = FreezeMock<IOptions<VirtualFileAccessorSectionGroup>>();
            _virtualFileAccessorSectionGroup = Create<VirtualFileAccessorSectionGroup>();
            virtualFileSectionGroup.Setup(c => c.Value).Returns(_virtualFileAccessorSectionGroup);

            var configs = new List<IVirtualFileRootConfiguration>();
            configs.Add(_firstRootConfig);
            configs.Add(_secondRootConfig);

            return new VirtualFileConfigurationReader(virtualFileSectionGroup.Object, configs, null);
        }

        [Test]
        public void should_set_current_provider_from_configuration()
        {
            var result = SUT.ReadToDO();

            Assert.That(result.CurrentProviderName, Is.EqualTo(_virtualFileAccessorSectionGroup.General.Active));
        }

        [Test]
        public void should_iterate_root_configs_and_create_ProviderGroupDo_from_each()
        {
            var result = SUT.ReadToDO();
    
            Assert.That(result.ProviderNames.Count, Is.EqualTo(2));
        }


        [Test]
        public void should_set_values_for_providergroupDo_from_configurations()
        {
            var result = SUT.ReadToDO();
            
            Assert.That(result.ProviderNames[0].ProviderKey, Is.EqualTo(_firstRootConfig.SectionName));

            Assert.That(result.ProviderNames[0].VirtualFileProviderNames.Count, Is.EqualTo(_firstRootConfig.Keys.Count()));
        }

    }
}
