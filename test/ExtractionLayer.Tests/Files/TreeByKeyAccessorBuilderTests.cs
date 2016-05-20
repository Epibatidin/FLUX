using DataAccess.FileSystem;
using DataAccess.Interfaces;
using Extension.Test;
using Extraction.Layer.File;
using System.Collections.Generic;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace ExtractionLayer.Tests.Files
{
    public static class TreeByKeyExtension
    { 
        public static string Value(this TreeByKeyAccessor treeBy, params int[] path)
        {
            var node = treeBy.Tree;

            foreach (var i in path)
            {
                node = node[i];
            }
            return node.Value.LevelValue.ToString();
        }
    }

    


    public class TreeByKeyAccessorBuilderTests : FixtureBase<TreeByKeyAccessorBuilder>
    {

        [Fact]
        public void should_add_virtualPath_part_to_tree_item()
        {
            var treeitems = new RealFile();
            treeitems.VirtualPath = "A";

            var result = SUT.Build(new[] { treeitems });

            Assert.That(result.Tree.Value.Artist, Is.EqualTo("A"));
        }

        [Fact]
        public void should_split_by_slash_set_as_value()
        {
            var treeitems = new RealFile();
            treeitems.VirtualPath = "A-B\\C:D\\E_F";
            treeitems.Name = "Name";
            var result = SUT.Build(new[] { treeitems });

            var tree = result.Tree;

            Assert.That(result.Tree.Value.LevelValue.ToString(), Is.EqualTo("A-B"));
            Assert.That(result.Tree[0].Value.LevelValue.ToString(), Is.EqualTo("C:D"));
            Assert.That(result.Tree[0][0].Value.LevelValue.ToString(), Is.EqualTo("E_F"));
        }


        [Fact]
        public void should_add_cd1_if_path_is_shorter_then_3()
        {
            var treeitems = new RealFile();
            treeitems.VirtualPath = "A\\B";

            var result = SUT.Build(new [] { treeitems });

            Assert.That(result.Tree.Value.Artist, Is.EqualTo("A"));
            Assert.That(result.Tree[0].Value.Album, Is.EqualTo("B"));
            Assert.That(result.Tree[0][0].Value.LevelValue.ToString(), Is.EqualTo("CD1"));
        }

        private RealFile AddVFile(IList<IVirtualFile> vfs, string path)
        {
            var rf = new RealFile();
            rf.VirtualPath = path;
            vfs.Add(rf);
            return rf;
        }
        
        [Fact]
        public void should_group_by_string_parts()
        {
            var vfs = new List<IVirtualFile>();
            AddVFile(vfs, "1\\a\\c\\g");
            AddVFile(vfs, "1\\a\\c\\h");
            AddVFile(vfs, "1\\a\\d\\i");
            AddVFile(vfs, "1\\a\\d\\j");
            AddVFile(vfs, "1\\b\\e\\k");
            AddVFile(vfs, "1\\b\\e\\l");
            AddVFile(vfs, "1\\b\\f\\m");
            AddVFile(vfs, "1\\b\\f\\n");

            var result = SUT.Build(vfs);

            Assert.That(result.Value(), Is.EqualTo("1"));
            Assert.That(result.Value(0), Is.EqualTo("a"));
            Assert.That(result.Value(1), Is.EqualTo("b"));

            Assert.That(result.Value(0,0), Is.EqualTo("c"));
            Assert.That(result.Value(0,1), Is.EqualTo("d"));
            Assert.That(result.Value(1,0), Is.EqualTo("e"));
            Assert.That(result.Value(1,1), Is.EqualTo("f"));

            Assert.That(result.Value(0,0, 0), Is.EqualTo("g"));
            Assert.That(result.Value(0,0, 1), Is.EqualTo("h"));
            Assert.That(result.Value(0,1, 0), Is.EqualTo("i"));
            Assert.That(result.Value(0,1, 1), Is.EqualTo("j"));
            Assert.That(result.Value(1,0, 0), Is.EqualTo("k"));
            Assert.That(result.Value(1,0, 1), Is.EqualTo("l"));
            Assert.That(result.Value(1,1, 0), Is.EqualTo("m"));
            Assert.That(result.Value(1,1, 1), Is.EqualTo("n"));
        }

        [Fact]
        public void should_use_path_iterator_to_build_the_key_mapping()
        {
            var vfs = new List<IVirtualFile>();
            AddVFile(vfs, "1\\a\\c\\g").ID = 22;
            AddVFile(vfs, "1\\a\\c\\h").ID = 13;
            var result = SUT.Build(vfs);

            SUT.BuildKeyMapping(result);

        }
    }
}
