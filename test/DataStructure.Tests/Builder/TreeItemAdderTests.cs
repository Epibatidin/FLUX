using System.Collections.Generic;
using System.Linq;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using Extension.Test;
using Xunit;
using Is = NUnit.Framework.Is;
using Assert = NUnit.Framework.Assert;

namespace DataStructure.Tests.Builder
{
    public class TreeItemAdderTests : FixtureBase<TreeItemAdder>
    {
        [Fact]
        public void should_create_childs_if_has_no_childs()
        {
            var root = new TreeItem<object>();
            
            Assert.That(root.HasChildren, Is.False);

            var item = new object();
            SUT.Add(root, new [] { 0 }, item);

            Assert.That(root.Children.First().Value, Is.SameAs(item));
        }

        [Fact]
        public void should_add_item_on_position_specified_by_path()
        {
            var root = new TreeItem<object>();
            var childs = Create<List<TreeItem<object>>>();
            childs[1].Value = null;

            root.SetChildren(childs);

            var item = new object();
            SUT.Add(root, new [] { 1 }, item);

            Assert.That(root.Children.Skip(1).First().Value, Is.SameAs(item));
        }

        [Fact]
        public void should_fill_with_empty_tree_items_if_child_collection_is_shorter_then_index()
        {
            var root = new TreeItem<object>();
            var childs = new List<TreeItem<object>>();
            childs.Add(new TreeItem<object>());
            root.SetChildren(childs);

            var item = new object();
            SUT.Add(root, new [] { 4 }, item);

            Assert.That(childs[0], Is.Not.Null);
            Assert.That(childs[1], Is.Not.Null);
            Assert.That(childs[2], Is.Not.Null);
            Assert.That(childs[3], Is.Not.Null);
            Assert.That(root.Children.Count(), Is.EqualTo(5));
        }

        [Fact]
        public void should_iterate_the_full_path()
        {
            var root = new TreeItem<object>();
            var lvlOne = Create<List<TreeItem<object>>>();
            var lvlTwo = new List<TreeItem<object>>();
            lvlOne[0].SetChildren(lvlTwo);
            root.SetChildren(lvlOne);

            var item = new object();
            SUT.Add(root, new [] {0, 3, 2}, item);

            Assert.That(root[0][3][2].Value , Is.SameAs(item));
        }

    }
}
