using System.Linq;
using DataAccess.Base;
using Extension.Test;
using NUnit.Framework;

namespace DataAccess.Tests.Base
{
    public class PathDataHelperTests : FixtureBase<PathDataHelper>
    {
        protected override PathDataHelper CreateSUT()
        {
            return new PathDataHelper();
        }

        [Test]
        public void should_find_extension()
        {
            string path =
                "D:\\Musik\\Musik - Die geordnet werden muss\\Amduscia\\2011 Amduscia-Death_Thou_Shalt_Die\\01_amduscia-damn_punks.mp3";

            var result = SUT.FullPathToVirtualPathData(path, "");

            Assert.That(result.Extension, Is.EqualTo("mp3"));
        }

        [Test]
        public void should_support_non_extension()
        {
            string path =
                "D:\\Musik\\Musik - Die geordnet werden muss\\Amduscia\\2011 Amduscia-Death_Thou_Shalt_Die\\01_amduscia-damn_punks";

            var result = SUT.FullPathToVirtualPathData(path, "");

            Assert.That(result.Extension, Is.EqualTo(""));
        }

        [Test]
        public void should_split_by_slashes()
        {
            string path =
                "D:\\Musik\\Musik - Die geordnet werden muss\\Amduscia\\2011 Amduscia-Death_Thou_Shalt_Die\\01_amduscia-damn_punks.mp3";

            var result = SUT.FullPathToVirtualPathData(path, "");

            Assert.That(result.PathParts.Contains("2011 Amduscia-Death_Thou_Shalt_Die"), Is.True);
        }

        [Test]
        public void should_remove_root_part()
        {
            string path =
                "D:\\Musik\\01_amduscia-damn_punks.mp3";

            var result = SUT.FullPathToVirtualPathData(path, "D:\\Musik");

            Assert.That(result.PathParts.Length, Is.EqualTo(1));
        }

        [Test]
        public void should_remove_root_part_and_set_path_part()
        {
            string path =
                "D:\\Musik\\01_amduscia-damn_punks.mp3";

            var result = SUT.FullPathToVirtualPathData(path, "D:\\Musik");

            Assert.That(result.PathParts[0], Is.EqualTo("01_amduscia-damn_punks"));
        }

        [Test]
        public void should_remove_root_part_and_set_path_part_without_extension()
        {
            string path = "D:\\Musik\\01_amduscia-damn_punks";

            var result = SUT.FullPathToVirtualPathData(path, "D:\\Musik");

            Assert.That(result.PathParts[0], Is.EqualTo("01_amduscia-damn_punks"));
        }

       
    }
}
