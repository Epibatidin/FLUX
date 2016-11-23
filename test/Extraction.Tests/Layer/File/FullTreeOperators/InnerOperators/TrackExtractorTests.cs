using Extension.Test;
using Extraction.Layer.File;
using Extraction.Layer.File.FullTreeOperators.MultiElementOperations;
using NUnit.Framework;
using System.Collections.Generic;

namespace ExtractionLayer.Tests.Files.FullTreeOperators.InnerOperators
{
    public class TrackExtractorTests : FixtureBase<TrackExtractor>
    {
        protected override TrackExtractor CreateSUT()
        {
            return new TrackExtractor();
        }

        [Test]
        public void should_not_drop_first_word()
        {
            var fileLayerSongs = new List<FileLayerSongDo>();
            fileLayerSongs.Add(new FileLayerSongDo("01.beyond the darkness(raving mix)"));
            fileLayerSongs.Add(new FileLayerSongDo("02.killing mission"));

            SUT.Execute(fileLayerSongs);

            Assert.That(fileLayerSongs[0].LevelValue.ToString(), Is.EqualTo("beyond the darkness(raving mix)"));
        }
    }
}
