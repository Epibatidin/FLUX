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

        [Test]
        public void should_not_add_single_letter_as_acronym()
        {
            var result = Splitter.ComplexSplit("01 - A Pleasure Without End");

            Assert.That(result[1], Is.EqualTo("A"));
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
        public void should_find_all_braces_and_inject_place_holders_or_empty()
        {
            var builder = new StringBuilder("09.evil song{}{1} [456](paradise mix) -  skfdhg ");
                                                      //g   ?1    ?2            ?0 -  skfdhg "     
            var result = Splitter.SplitStringInBracesBlocks(builder);

            Assert.That(result[0], Is.EqualTo("(paradise mix)"));
            Assert.That(result[1], Is.EqualTo("{1}"));
            Assert.That(result[2], Is.EqualTo("[456]"));

            Assert.That(builder.ToString(), Is.EqualTo("09.evil song   ?1    ?2            ?0 -  skfdhg "));
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

        [Test]
        public void should_support_acronyms()
        {
            var result = Splitter.ComplexSplit("09.a.b.c");

            Assert.That(result[0], Is.EqualTo("09"));
            Assert.That(result[1], Is.EqualTo("a.b.c."));
        }

        [Test]
        public void should_keep_acronyms()
        {
            var splitted = Splitter.ComplexSplit("a.b.c.");

            Assert.That(splitted[0], Is.EqualTo("a.b.c."));
        }

        [TestCase("11.a.b.c.")]
        [TestCase("11.a.b.c")]
        public void should_find_non_acronym_parts(string source)
        {
            var splitted = Splitter.ComplexSplit(source);
            Assert.That(splitted[0], Is.EqualTo("11"));
            Assert.That(splitted[1], Is.EqualTo("a.b.c."));
        }

        [Test]
        public void should_find_non_acronym_parts_2()
        {
            var splitted = Splitter.ComplexSplit("a.b.c.11");

            Assert.That(splitted[0], Is.EqualTo("a.b.c."));
            Assert.That(splitted[1], Is.EqualTo("11"));
        }

        [Test]
        public void should_stand_a_real_world_example()
        {
            //07 - Animal Instinct(Part 2)(Nightmares Of Conscience)
            var splitted = Splitter.ComplexSplit("07 - Animal Instinct(Part 2)(Nightmares Of Conscience)");

            Assert.That(splitted[0], Is.EqualTo("07"));
            Assert.That(splitted[1], Is.EqualTo("Animal"));
            Assert.That(splitted[2], Is.EqualTo("Instinct"));
            Assert.That(splitted[3], Is.EqualTo("(Part 2)"));
            Assert.That(splitted[4], Is.EqualTo("(Nightmares Of Conscience)"));

        }
    }
}
