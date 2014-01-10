using System.IO;
using System.Collections.Generic;

namespace MP3LibIntegrater
{
    class Program
    {
        static void Main(string[] args)
        {
            

            // jeder künster seine eigene Fabrik

            // jeder künstler hat 1 oder mehrere alben 

            List<Artist> artistList = new List<Artist>();

            string[] artistFolders = Directory.GetDirectories(Algs.badMP3BasicPath);

            foreach (string artist in artistFolders)
            {
                Artist dummy = new Artist(artist);
                dummy.getSubs();
                artistList.Add(dummy);
            }

            // jeder artist seinen eigenen executer
            // 
            


            foreach (var artist in artistList)
            {
                Executor ex = new Executor(artist);
                ex.GatherAvailableInformation();
                ex.MakePersistent();
            }
            //artist.getAllAlbumsOfArtist();
            //artist.searchRepatingInformation();
            //artist.cleanUpAlbumInformation();

            //if (!Algs.compareSourceAndTarget(null,null)) System.Console.WriteLine("ungleich");
           // else System.Console.WriteLine("gleich");


            System.Console.WriteLine();
            System.Console.WriteLine("DONE");
            System.Console.ReadKey();

        }
    }
}
