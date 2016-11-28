using Extension.Test;
using Extraction.Layer.File;
using Extraction.Layer.File.FullTreeOperators.MultiElementOperations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ExtractionLayer.Tests.Files.FullTreeOperators.InnerOperators
{
    [TestFixture]
    public class TrackExtractorTests : FixtureBase<TrackExtractor>
    {
        protected override TrackExtractor CreateSUT()
        {
            return new TrackExtractor();
        }

        private FileLayerSongDo FLS(string value)
        {
            return new FileLayerSongDo(value);
        }

        [Test]
        public void should_not_drop_first_word()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("01.beyond the darkness(raving mix)"));
            fileLayerSongs.Add(new FileLayerSongDo("02.killing mission"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].LevelValue.ToString(), Is.EqualTo("beyond the darkness (raving mix)"));
        }
                
        [Test]
        public void should_extract_track()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("01.beyond the darkness(raving mix)"));
            fileLayerSongs.Add(new FileLayerSongDo("02.killing mission"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].TrackNr, Is.EqualTo(1));
            Assert.That(fileLayerSongs[1].TrackNr, Is.EqualTo(2));           
        }

        [Test]
        public void should_take_number_by_position()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("23.beyond 12 darkness(raving mix)"));
            fileLayerSongs.Add(new FileLayerSongDo("02.killing 14"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].TrackNr, Is.EqualTo(23));
            Assert.That(fileLayerSongs[1].TrackNr, Is.EqualTo(2));
        }

        [Test]
        public void should_take_number_by_position_with_relative_end()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("word word 1 word word"));
            fileLayerSongs.Add(new FileLayerSongDo("word word word 2 word word"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].TrackNr, Is.EqualTo(1));
            Assert.That(fileLayerSongs[1].TrackNr, Is.EqualTo(2));
        }

        [Test]
        public void should_treat_duplicated_numbers_as_cd()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("01.beyond the darkness(raving mix)"));
            fileLayerSongs.Add(new FileLayerSongDo("01.killing mission"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].TrackNr, Is.EqualTo(1));
            Assert.That(fileLayerSongs[0].CDRaw, Is.EqualTo(1));

            Assert.That(fileLayerSongs[1].TrackNr, Is.EqualTo(1));
            Assert.That(fileLayerSongs[1].CDRaw, Is.EqualTo(2));
        }

        [Test]
        public void should_treat_3_digit_ints_as_cd()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("101"));
            fileLayerSongs.Add(new FileLayerSongDo("201"));
            fileLayerSongs.Add(new FileLayerSongDo("401"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].CDRaw, Is.EqualTo(1));
            Assert.That(fileLayerSongs[1].CDRaw, Is.EqualTo(2));
            Assert.That(fileLayerSongs[2].CDRaw, Is.EqualTo(4));
        }
    }
}
