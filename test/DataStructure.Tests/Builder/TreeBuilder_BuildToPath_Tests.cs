using DataStructure.Tree;
using DataStructure.Tree.Builder;
using Extension.Test;
using NUnit.Framework;
using Moq;

namespace DataStructure.Tests.Builder
{
    public class TreeBuilder_BuildToPath_Tests : FixtureBase<TreeBuilder>
    {
        private Mock<IMyMock> _myMock;

        public interface IMyMock
        {
            SomeItem GetItem(SomeItem leave, int level, bool r);
        }

        protected override void Customize()
        {
            _myMock = new Mock<IMyMock>();

        }

        protected override TreeBuilder CreateSUT()
        {
            return new TreeBuilder();
        }

        [Test]
        public void should_invoke_the_Builder_0_based()
        {
            var someItem = new SomeItem();

            var root = new TreeItem<SomeItem>();
            SUT.BuildToPath(root, new[] { 0, 0, 0, 0 }, _myMock.Object.GetItem, someItem);

            _myMock.Verify(c => c.GetItem(someItem, 0, It.IsAny<bool>()));
            _myMock.Verify(c => c.GetItem(someItem, 1, It.IsAny<bool>()));
            _myMock.Verify(c => c.GetItem(someItem, 2, It.IsAny<bool>()));
            _myMock.Verify(c => c.GetItem(someItem, 3, It.IsAny<bool>()));
        }

        [Test]
        public void should_have_the_correct_lvl_value()
        {
            var someItem = new SomeItem();

            var s0 = new SomeItem();
            var s1 = new SomeItem();

            _myMock.Setup(c => c.GetItem(someItem, 0, It.IsAny<bool>())).Returns(s0);
            _myMock.Setup(c => c.GetItem(someItem, 1, It.IsAny<bool>())).Returns(s1);

            var root = new TreeItem<SomeItem>();
            root.Level = 0;

            SUT.BuildToPath(root, new[] { 0, 0 }, _myMock.Object.GetItem, someItem);

            Assert.That(root.Value, Is.SameAs(s0));
            Assert.That(root[0].Value, Is.SameAs(s1));
        }
    }
}
