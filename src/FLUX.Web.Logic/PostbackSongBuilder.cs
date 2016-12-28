using FLUX.Interfaces.Web;
using System.Collections.Generic;
using DataAccess.Interfaces;
using FLUX.DomainObjects;

namespace FLUX.Web.Logic
{
    public class PostbackSongBuilder : IPostbackSongBuilder
    {
        public IList<IExtractionValueFacade> Flatten(PostbackTree postBackTree)
        {
            var builder = new PostbackTreeVisitor();

            return FlattenInternal(postBackTree, builder);
        }

        public IList<IExtractionValueFacade> FlattenInternal(PostbackTree postBackTree, IPostbackTreeVisitor pseudoVisitor)
        {
            var postBack = new List<IExtractionValueFacade>();

            foreach (var artist in postBackTree.Artists)
            {
                pseudoVisitor.Add(artist);
                foreach (var album in artist.Albums)
                {
                    pseudoVisitor.Add(album);
                    foreach (var cd in album.Cds)
                    {
                        if(album.Cds.Count > 1)
                            pseudoVisitor.Add(cd);

                        foreach (var song in cd.Songs)
                        {
                            pseudoVisitor.Add(song);

                            postBack.Add(pseudoVisitor.Current);
                        }
                    }
                }
            }

            return postBack;
        }

    }
}
