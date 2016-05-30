using System.Collections.Generic;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace DataStructure.Tests.Builder
{
    [TestFixture]
    public class TreeHelper_BuildTreeFromCollection_Tests
    {
        private TreeBuilder SUT;

        [SetUp]
        public void Setup()
        {
            SUT = new TreeBuilder();
        }


        public TreeItem<SomeItem> Act(IList<SomeItem> someItems)
        {
            var result = SUT.BuildTreeFromCollection(someItems, (c, i) => c.GetKey(i), (item, i) => item);

            return result;
        }

        [Test]
        public void some_easy_use_case()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B"));
            list.Add(new SomeItem("A", "C"));

            var tree = Act(list);

            Assert.That(tree.Value, Is.SameAs(list[0]));

            Assert.That(tree[0].Value, Is.SameAs(list[0]));
            Assert.That(tree[1].Value, Is.SameAs(list[1]));
        }

        [Test]
        public void some_more_advanced_use_case()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B", "D"));
            list.Add(new SomeItem("A", "B", "E"));
            list.Add(new SomeItem("A", "C", "F"));
            list.Add(new SomeItem("A", "C", "G"));

            var tree = Act(list);

            Assert.That(tree.Value, Is.SameAs(list[0]));

            Assert.That(tree[0].Value, Is.SameAs(list[0]));

            Assert.That(tree[0][0].Value, Is.SameAs(list[0]));
            Assert.That(tree[0][1].Value, Is.SameAs(list[1]));

            Assert.That(tree[1].Value, Is.SameAs(list[2]));



            Assert.That(tree[1][0].Value, Is.SameAs(list[2]));
            Assert.That(tree[1][1].Value, Is.SameAs(list[3]));
        }

        [Test]
        public void some_more_advanced_use_case_without_sorting()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B", "D"));
            list.Add(new SomeItem("A", "C", "F"));
            list.Add(new SomeItem("A", "B", "E"));
            list.Add(new SomeItem("A", "C", "G"));
            list.Add(new SomeItem("A", "B", "H"));

            var tree = Act(list);

            Assert.That(tree[0][0].Value, Is.SameAs(list[0]));
            Assert.That(tree[0][1].Value, Is.SameAs(list[2]));
            Assert.That(tree[0][2].Value, Is.SameAs(list[4]));

            Assert.That(tree[1][0].Value, Is.SameAs(list[1]));
            Assert.That(tree[1][1].Value, Is.SameAs(list[3]));
        }


        [Test]
        public void should_support_key_uniqueness_by_path_simple()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B", "D"));
            list.Add(new SomeItem("A", "C", "D"));

            var tree = Act(list);

            Assert.That(tree.Value, Is.SameAs(list[0]));

            // B Node
            Assert.That(tree[0].Value, Is.SameAs(list[0]));
            // C Node
            Assert.That(tree[1].Value, Is.SameAs(list[1]));


            // B-D Node
            Assert.That(tree[0][0].Value, Is.SameAs(list[0]));
            // C-D Node
            Assert.That(tree[1][0].Value, Is.SameAs(list[1]));
        }

        [Test]
        public void should_support_key_uniqueness_by_path()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B", "D", "AA"));
            list.Add(new SomeItem("A", "C", "D", "BB"));
            list.Add(new SomeItem("A", "B", "D", "AB"));
            list.Add(new SomeItem("A", "C", "D", "BC"));
            list.Add(new SomeItem("A", "B", "D", "GT"));

            var tree = Act(list);

            Assert.That(tree.Value, Is.SameAs(list[0]));

            // B Node
            Assert.That(tree[0].Value, Is.SameAs(list[0]));

            // B-D Node
            Assert.That(tree[0][0].Value, Is.SameAs(list[0]));

            Assert.That(tree[0][0][0].Value, Is.SameAs(list[0]));
            Assert.That(tree[0][0][1].Value, Is.SameAs(list[2]));
            Assert.That(tree[0][0][2].Value, Is.SameAs(list[4]));

            // C Node
            Assert.That(tree[1].Value, Is.SameAs(list[1]));

            // C-D Node
            Assert.That(tree[1][0].Value, Is.SameAs(list[1]));

            Assert.That(tree[1][0][0].Value, Is.SameAs(list[1]));
            Assert.That(tree[1][0][1].Value, Is.SameAs(list[3]));
        }


        [Test]
        public void should_support_key_uniqueness_by_path_keys()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B", "D", "AA"));
            list.Add(new SomeItem("A", "C", "D", "BB"));
            list.Add(new SomeItem("A", "B", "D", "AB"));
            list.Add(new SomeItem("A", "C", "D", "BC"));
            list.Add(new SomeItem("A", "B", "D", "GT"));

            var tree = new Dictionary<string, TreeBuilder.DictItem>();

            SUT.BuildTreeFromCollectionForTests(list, (c, i) => c.GetKey(i), (item, i) => item, tree);

            var bValue = tree["B"];

            Assert.That(bValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(bValue.ActivePath.Path, Is.EqualTo(new[] { 0 }));


            var cValue = tree["C"];

            Assert.That(cValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(cValue.ActivePath.Path, Is.EqualTo(new[] { 1 }));

            var dValue = tree["D"];

            Assert.That(dValue.KnownPathes.Count, Is.EqualTo(2));
            Assert.That(dValue.KnownPathes[0].Path, Is.EqualTo(new[] { 0, 0 }));
            Assert.That(dValue.KnownPathes[1].Path, Is.EqualTo(new[] { 1, 0 }));

            var aaValue = tree["AA"];
            Assert.That(aaValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(aaValue.ActivePath.Path, Is.EqualTo(new[] { 0, 0, 0 }));

            var bbValue = tree["BB"];
            Assert.That(bbValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(bbValue.ActivePath.Path, Is.EqualTo(new[] { 1, 0, 0 }));

            var abValue = tree["AB"];
            Assert.That(abValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(abValue.ActivePath.Path, Is.EqualTo(new[] { 0, 0, 1 }));

            var bcValue = tree["BC"];
            Assert.That(bcValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(bcValue.ActivePath.Path, Is.EqualTo(new[] { 1, 0, 1 }));

            var gtValue = tree["GT"];
            Assert.That(gtValue.KnownPathes.Count, Is.EqualTo(1));
            Assert.That(gtValue.ActivePath.Path, Is.EqualTo(new[] { 0, 0, 2 }));
        }
    }
}