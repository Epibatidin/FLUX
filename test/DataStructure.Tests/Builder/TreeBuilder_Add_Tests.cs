using System.Collections.Generic;
using System.Linq;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using NUnit.Framework;
using Extension.Test;

namespace DataStructure.Tests.Builder
{
    public class TreeBuilder_Add_Tests : FixtureBase<TreeBuilder>
    {
        protected override TreeBuilder CreateSUT()
        {
            return new TreeBuilder();
        }

        [Test]
        public void should_create_childs_if_has_no_childs()
        {
            var root = new TreeItem<object>();
            
            Assert.That(root.HasChildren, Is.False);

            var item = new object();
            SUT.Add(root, new [] { 0 }, item);

            Assert.That(root.Children.First().Value, Is.SameAs(item));
        }

        [Test]
        public void should_add_item_on_position_specified_by_path()
        {
            var root = new TreeItem<object>();
            var childs = new List<TreeItem<object>>();
            for (int i = 0; i < 3; i++)
            {
                childs.Add(new TreeItem<object>());
            }
            childs[1].Value = null;

            root.SetChildren(childs);

            var item = new object();
            SUT.Add(root, new [] { 1 }, item);

            Assert.That(root.Children.Skip(1).First().Value, Is.SameAs(item));
        }

        [Test]
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

        [Test]
        public void should_iterate_the_full_path()
        {
            var root = new TreeItem<object>();

            var item = new object();
            SUT.Add(root, new [] {0, 3, 2}, item);

            Assert.That(root[0][3][2].Value , Is.SameAs(item));
        }

        [Test]
        public void should_add_new_items_with_correct_depth()
        {
            var root = new TreeItem<object>();
            root.Level = 22;

            var item = new object();
            SUT.Add(root, new[] { 0, 0 }, item);

            Assert.That(root[0].Level, Is.EqualTo(23));
            Assert.That(root[0][0].Level, Is.EqualTo(24));
        }

        [Test]
        public void should_only_set_value_on_deepest_item()
        {
            var root = new TreeItem<object>();
            root.Level = 22;

            var item = new object();
            SUT.Add(root, new[] { 0, 0 }, item);

            Assert.That(root[0].Value, Is.Not.SameAs(item));
            Assert.That(root[0][0].Value, Is.SameAs(item));
        }
    }
}
