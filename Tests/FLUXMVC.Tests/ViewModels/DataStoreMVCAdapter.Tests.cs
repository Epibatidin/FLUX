using NUnit.Framework;
using TestHelpers;

namespace FLUXMVC.Tests.ViewModels
{
    [TestFixture]
    public class DataStoreMVCAdapterTests
    {

        [Test]
        public void ShouldBuildFluentTree()
        {
            var artist = TreeBuilderHelper.Song();

            var tree = artist.Item(album => album.Items(2, cd => cd.Items(1, song => song.Items(3)))).AsTree();

            var z = tree;


            //root.Root(c =>
            //{
            //    c.Item(k =>
            //    {
            //        k.Item(r =>
            //        {
            //            r.Items(3);
            //        });
            //    });
            //    c.Item(k =>
            //    {
            //        k.Item(r =>
            //        {
            //            r.Items(3);
            //        });
            //    });
            //    c.Item(k =>
            //    {
            //        k.Item(r =>
            //        {
            //            r.Items(3);
            //        });
            //    });
            //}).AsTree();

        }





        //[TestMethod]
        //public void TestMethod1()
        //{
        //    DataStore data = new DataStore();
        //    TreeItem<Song> tree = new TreeItem<Song>();
        //    tree.Value = new Song()
        //    {
        //        ID = 1000,
        //        Artist = "Artist"
        //    };

        //    var alben = new List<TreeItem<Song>>()
        //    {
        //        new TreeItem<Song>() {
        //            Value = new Song() {
        //                ID = 1100, 
        //                Album = "Album 01", 
        //                Year = 2000                    
        //            }                
        //        }
        //    };

        //    var cd = new List<TreeItem<Song>>()
        //    {
        //        new TreeItem<Song>()
        //        {
        //            Value = new Song()
        //            {
        //                ID = 1110,
        //                CD = 1
        //            }
        //        }
        //    };

        //    var song = new List<TreeItem<Song>>()
        //    {
        //        new TreeItem<Song>() 
        //        {
        //            Value = new Song() 
        //            {
        //                SongName = "Song 01",
        //                TrackNr = 1, 
        //                ID = 1111                        
        //            }
        //        }
        //    };
        //    data.Register("Item1").UpdateData();
        //    data.Register("Item2");

        //    DataStoreMVCAdapter adapter = new DataStoreMVCAdapter(data);
        //    var merged = adapter.LayerData;


        //}
    }
}
