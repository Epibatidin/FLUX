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
            var postBack = new List<IExtractionValueFacade>();

            foreach (var artist in postBackTree.Artists)
            {
                builder.Add(artist);
                foreach (var album in artist.Albums)
                {
                    builder.Add(album);
                    foreach (var cd in album.Cds)
                    {
                        builder.Add(cd);
                        foreach (var song in cd.Songs)
                        {
                            builder.Add(song);

                            postBack.Add(builder.Current);
                        }
                    }
                }
            }

            return postBack;
        }
    }
}
