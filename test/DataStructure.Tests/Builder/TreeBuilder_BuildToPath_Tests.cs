using DataStructure.Tree;
using DataStructure.Tree.Builder;
using Extension.Test;
using Moq;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace DataStructure.Tests.Builder
{
    public class TreeBuilder_BuildToPath_Tests : FixtureBase<TreeBuilder>
    {
        public interface IMyMock
        {
            SomeItem GetItem(SomeItem leave, int level, bool r);
        }

        [Fact]
        public void should_invoke_the_Builder_0_based()
        {
            var myMock = new Mock<IMyMock>();

            var someItem = new SomeItem();

            var root = new TreeItem<SomeItem>();
            SUT.BuildToPath(root, new[] { 0, 0, 0, 0 }, myMock.Object.GetItem, someItem);

            myMock.Verify(c => c.GetItem(someItem, 0, It.IsAny<bool>()));
            myMock.Verify(c => c.GetItem(someItem, 1, It.IsAny<bool>()));
            myMock.Verify(c => c.GetItem(someItem, 2, It.IsAny<bool>()));
            myMock.Verify(c => c.GetItem(someItem, 3, It.IsAny<bool>()));
        }

        [Fact]
        public void should_have_the_correct_lvl_value()
        {
            var someItem = new SomeItem();

            var s0 = new SomeItem();
            var s1 = new SomeItem();
            var myMock = new Mock<IMyMock>();
            myMock.Setup(c => c.GetItem(someItem, 0, It.IsAny<bool>())).Returns(s0);
            myMock.Setup(c => c.GetItem(someItem, 1, It.IsAny<bool>())).Returns(s1);

            var root = new TreeItem<SomeItem>();
            root.Level = 0;

            SUT.BuildToPath(root, new[] { 0, 0 }, myMock.Object.GetItem, someItem);

            Assert.That(root.Value, Is.SameAs(s0));
            Assert.That(root[0].Value, Is.SameAs(s1));
        }

    }
}
