using Extension.Test;
using Extraction.Interfaces;
using FLUX.DomainObjects;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using DataStructure.Tree.Builder;

namespace FLUX.Web.Logic.Tests
{
    public static class MultiL
    {
        public static MultiLayerDataContainer Add(this MultiLayerDataContainer container, string key, params string[] values)
        {
            if (container.Data == null)
                container.Data = new Dictionary<string, List<string>>();

            var dict = container.Data;
            dict.Add(key, values.ToList());

            container.Data = dict;

            return container;
        }
    }


    public class LayerResultJoinerTests : FixtureBase<LayerResultJoiner>
    {
        private Mock<ITreeBuilder> _treeBuilder;

        protected override void Customize()
        {
            _treeBuilder = FreezeMock<ITreeBuilder>();

        }


        protected override LayerResultJoiner CreateSUT()
        {
            return new LayerResultJoiner(_treeBuilder.Object);
        }


        [Test]
        public void should_iterate_the_virtual_files_and_build_multiLayerContainer()
        {
            var vFiles = Create<VFile>();

            var result = SUT.BuildInitalFlatMultiLayerCollection(new [] { vFiles }, new List<ISongByKeyAccessor>());

            Assert.That(result[0].Id, Is.EqualTo(vFiles.ID));
        }

        [Test]
        public void should_invoke_by_key_Accessors_and_join_result_on_multilayer_container()
        {
            var vFiles = Create<VFile>();
            var songByKey0 = new Mock<ISongByKeyAccessor>();
            var songByKey1 = new Mock<ISongByKeyAccessor>();
            var song0 = new Mock<ISong>();
            var song1 = new Mock<ISong>();

            songByKey0.Setup(c => c.GetByKey(vFiles.ID)).Returns(song0.Object).Verifiable();
            songByKey1.Setup(c => c.GetByKey(vFiles.ID)).Returns(song1.Object).Verifiable();
            
            var result = SUT.BuildInitalFlatMultiLayerCollection(new[] { vFiles }, new[] { songByKey0.Object , songByKey1.Object });

            songByKey1.Verify();
        }
    }
}
