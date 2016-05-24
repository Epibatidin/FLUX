using DataAccess.Interfaces;
using Extension.Test;
using Extraction.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using DataStructure.Tree;
using FLUX.DomainObjects;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

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
        [Fact]
        public void should_iterate_the_virtual_files_and_build_multiLayerContainer()
        {
            var vFiles = Create<VFile>();

            var result = SUT.BuildInitalFlatMultiLayerCollection(new [] { vFiles }, new List<ISongByKeyAccessor>());

            Assert.That(result[0].Id, Is.EqualTo(vFiles.ID));
            Assert.That(result[0].Path, Is.EqualTo(vFiles.VirtualPath));
        }

        [Fact]
        public void should_invoke_by_key_Accessors_and_join_result_on_multilayer_container()
        {
            var vFiles = Create<VFile>();
            var songByKey0 = new Mock<ISongByKeyAccessor>();
            var songByKey1 = new Mock<ISongByKeyAccessor>();
            var song0 = Create<Mock<ISong>>();
            var song1 = Create<Mock<ISong>>();

            songByKey0.Setup(c => c.GetByKey(vFiles.ID)).Returns(song0.Object).Verifiable();
            songByKey1.Setup(c => c.GetByKey(vFiles.ID)).Returns(song1.Object).Verifiable();
            
            var result = SUT.BuildInitalFlatMultiLayerCollection(new[] { vFiles }, new[] { songByKey0.Object , songByKey1.Object });

            songByKey1.Verify();

            Assert.That(result[0].Data["Artist"][0], Is.EqualTo(song0.Object.Artist));
            Assert.That(result[0].Data["Artist"][1], Is.EqualTo(song1.Object.Artist));
        }

        //[Fact]
        //public void should_group_on_lists_and_build_tree()
        //{
        //    var list = new List<MultiLayerDataContainer>();
        //    list.Add(new MultiLayerDataContainer().Add("Artist", "a", "b").Add("Album", "c", "d").Add("Song", "1"));
        //    list.Add(new MultiLayerDataContainer().Add("Artist", "a", "b").Add("Album", "c", "d").Add("Song", "2"));
        //    list.Add(new MultiLayerDataContainer().Add("Artist", "a", "b").Add("Album", "e", "f").Add("Song", "3"));
        //    list.Add(new MultiLayerDataContainer().Add("Artist", "a", "b").Add("Album", "e", "f").Add("Song", "4"));

        //    var result = SUT.TraverseListToTree(list);

        //    //Assert.That(result.Value, Is.SameAs(list[0]));

        //    //Assert.That(result[0].Value, Is.SameAs(list[0]));
        //    Assert.That(result[0][0].Value, Is.SameAs(list[0]));
        //    Assert.That(result[0][1].Value, Is.SameAs(list[1]));

        //    //Assert.That(result[1].Value, Is.SameAs(list[2]));
        //    Assert.That(result[1][0].Value, Is.SameAs(list[2]));
        //    Assert.That(result[1][1].Value, Is.SameAs(list[3]));
        //}

    }
}
