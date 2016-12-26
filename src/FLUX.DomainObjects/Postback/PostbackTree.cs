using System.Collections.Generic;

namespace FLUX.DomainObjects
{
    public class ArtistNode
    {
        public string Artist { get; set; }

        public List<AlbumNode> Albums { get; set; } = new List<AlbumNode>();        
    }

    public class AlbumNode
    {
        public int Year { get; set; }
        public string Album { get; set; } 

        public List<CdNode> Cds { get; set; } = new List<CdNode>();        
    }


    public class CdNode
    {
        public string CD { get; set; }

        public List<SongNode> Songs { get; set; } = new List<SongNode>();
    }

    public class SongNode
    {
        public int Id { get; set; }

        public int TrackNr { get; set; }
        public string SongName { get; set; }        
    }
    
    public class PostbackTree
    {
        public List<ArtistNode> Artists { get; set; } = new List<ArtistNode>();        
    }
}
