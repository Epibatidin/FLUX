using Extension.Test;
using Extraction.Base;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace ExtractionLayer.Tests
{
    public class NonOverwrittingSongDummyTests : FixtureBase<object>
    {
        [Fact]
        public void should_update_values()
        {
            var overwrite = new NonOverwrittingSongDummy();
            
            var song = Create<NonOverwrittingSongDummy>();

            overwrite.Add(song);

            Assert.That(overwrite.CD, Is.EqualTo(song.CD));
            Assert.That(overwrite.TrackNr, Is.EqualTo(song.TrackNr));
            Assert.That(overwrite.Year, Is.EqualTo(song.Year));
            Assert.That(overwrite.Album, Is.EqualTo(song.Album));
            Assert.That(overwrite.Artist, Is.EqualTo(song.Artist));
            Assert.That(overwrite.SongName, Is.EqualTo(song.SongName));
        }

        [Fact]
        public void should_not_overwrite_previous_set_values()
        {
            var overwrite = new NonOverwrittingSongDummy();
            overwrite.CD = 1;
            overwrite.Album = "sdh";

            var song = new NonOverwrittingSongDummy();
            song.CD = 0;

            overwrite.Add(song);

            Assert.That(overwrite.CD, Is.EqualTo(1));
            Assert.That(overwrite.Album, Is.EqualTo("sdh"));
        }

    }
}
