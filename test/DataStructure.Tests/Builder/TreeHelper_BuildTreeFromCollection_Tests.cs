using System.Collections.Generic;
using System.Linq;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using Extension.Test;
using NUnit.Framework;
using Xunit;
using Is = NUnit.Framework.Is;
using Assert = NUnit.Framework.Assert;

namespace DataStructure.Tests.Builder
{
    public class SomeItem
    {
        public SomeItem(params string[] values)
        {
            Values = values.ToList();
        }

        public IList<string> Values { get; set; }

        public string GetKey(int depth)
        {
            if (Values == null) return null;

            if (Values.Count > depth)
                return Values[depth];

            return null;
        }

    }

    [TestFixture]
    public class TreeHelper_BuildTreeFromCollection_Tests : FixtureBase<TreeBuilder>
    {
        public TreeItem<SomeItem> Act(IList<SomeItem> someItems)
        {
            var result = SUT.BuildTreeFromCollection(someItems, (c,i) => c.GetKey(i));

            return result;
        }

        [Test]
        public void some_easy_use_case()
        {
            var list = new List<SomeItem>();
            list.Add(new SomeItem("A", "B"));
            list.Add(new SomeItem("A", "C"));

            var tree = Act(list)[0];

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

            var tree = Act(list)[0];

            Assert.That(tree[0][0].Value, Is.SameAs(list[0]));
            Assert.That(tree[0][2].Value, Is.SameAs(list[1]));

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

            var tree = Act(list)[0];

            Assert.That(tree[0][0].Value, Is.SameAs(list[0]));
            Assert.That(tree[0][1].Value, Is.SameAs(list[2]));
            Assert.That(tree[0][2].Value, Is.SameAs(list[4]));

            Assert.That(tree[1][0].Value, Is.SameAs(list[1]));
            Assert.That(tree[1][1].Value, Is.SameAs(list[3]));
        }
    }
}
