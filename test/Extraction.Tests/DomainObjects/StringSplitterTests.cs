using Extraction.DomainObjects.StringManipulation;
using NUnit.Framework;
using System.Text;

namespace Extraction.Tests.DomainObjects
{
    [TestFixture]
    public class StringSplitterTests
    {
        [Test]
        public void should_split_by_space()
        {
            var result = Splitter.ComplexSplit("1 2");

            Assert.That(result[0], Is.EqualTo("1"));
            Assert.That(result[1], Is.EqualTo("2"));
        }

        [TestCase("(1)")]
        [TestCase("[1]")]
        [TestCase("{1}")]
        public void should_keep_all_kind_of_braces(string source)
        {
            var result = Splitter.ComplexSplit(source);

            Assert.That(result[0], Is.EqualTo(source));
        }

        [Test]
        public void should_find_all_braces_and_inject_place_holders()
        {
            var builder = new StringBuilder("09.evil song{123} (paradise mix) -  skfdhg [456]");

            var result = Splitter.SplitStringInBracesBlocks(builder);

            Assert.That(result[0], Is.EqualTo("(paradise mix)"));
            Assert.That(result[1], Is.EqualTo("{123}"));
            Assert.That(result[2], Is.EqualTo("[456]"));

            Assert.That(builder.ToString(), Is.EqualTo("09.evil song$$$1 $$$0 -  skfdhg $$$2"));
        }

        [Test]
        public void should_split_by_delimiters_only_if_outside_braces()
        {
            var result = Splitter.ComplexSplit("09.evil song (paradise mix)");

            Assert.That(result[0], Is.EqualTo("09"));
            Assert.That(result[1], Is.EqualTo("evil"));
            Assert.That(result[2], Is.EqualTo("song"));
            Assert.That(result[3], Is.EqualTo("(paradise mix)"));
        }
    }
}
