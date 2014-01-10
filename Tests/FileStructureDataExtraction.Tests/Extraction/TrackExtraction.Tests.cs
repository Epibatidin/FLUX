using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tree;
using FileStructureDataExtraction.Builder;
using Child = Tree.TreeItem<FileStructureDataExtraction.Builder.FileLayerSongDO>;
using FileStructureDataExtraction.Extraction;
using Common.StringManipulation;

namespace FileStructureDataExtraction.Tests.Extraction
{
    [TestClass]
    public class TrackExtractionTests
    {
        private List<Child> GetTracksWithoutCD(int Tracks)
        {
            var result = new List<Child>();
             for (int t = 0; t < Tracks; t++)
            {
                var c = new Child();
                c.Value = new FileLayerSongDO();
                c.Value.LevelValue = new Common.StringManipulation.PartedString(((t + 1)) + " value");
                result.Add(c);
            }
            return result;
        }

        private List<Child> GetMultiCDTracks(int CDs,int TracksPerCD)
        {
            var result = new List<Child>();

            for (int cd = 0; cd < CDs; cd++)
            {
                for (int t = 0; t < TracksPerCD; t++)
                {
                    var c = new Child();
                    c.Value = new FileLayerSongDO();
                    c.Value.LevelValue = new PartedString(((cd + 1) * 100 + (t + 1)) + " value");
                    result.Add(c);
                }
            }
            return result;
        }

        [TestMethod]
        public void Should_Find_Track()
        {
            TrackExtractor extractor = new TrackExtractor(c =>
            {
                return c.Value.LevelValue;
            });

            int count = 7;

            var data = GetTracksWithoutCD(count);
            extractor.CurrentData(data);

            extractor.Execute();

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i+1, data[i].Value.TrackNr);
            }
        }
       

        [TestMethod]
        public void Should_Extract_CD()
        {
            TrackExtractor extractor = new TrackExtractor(c =>
            {
                return c.Value.LevelValue;
            });

            int count = 3;

            var data = GetMultiCDTracks(2, count);
            extractor.CurrentData(data);

            extractor.Execute();

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i + 1, data[i].Value.TrackNr);
            }
        }
    }
}
