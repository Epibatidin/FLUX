using Extension.Test;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLUX.Web.Logic.Tests
{
    public class PostbackSongBuilderTests : FixtureBase<PostbackSongBuilder>
    {
        protected override PostbackSongBuilder CreateSUT()
        {
            return new PostbackSongBuilder();
        }

        [Test]
        public void should_add_collect_data_of_all_lvls()
        {
            var tree = new PostbackTree();

            var artistNode = new ArtistNode();
            var albumNode = new AlbumNode();
            var cdNode = new CdNode();
            var song = new SongNode();

            tree.Artists.Add(artistNode);
            artistNode.Albums.Add(albumNode);
            albumNode.Cds.Add(cdNode);
            albumNode.Cds.Add(cdNode);
            cdNode.Songs.Add(song);

            var visitor = new Mock<IPostbackTreeVisitor>();

            SUT.FlattenInternal(tree, visitor.Object);

            visitor.Verify(c => c.Add(artistNode));
            visitor.Verify(c => c.Add(albumNode));
            visitor.Verify(c => c.Add(cdNode));
            visitor.Verify(c => c.Add(song));
        }

        [Test]
        public void should_not_add_cd_data_if_only_1_cd()
        {
            var tree = new PostbackTree();

            var artistNode = new ArtistNode();
            var albumNode = new AlbumNode();
            var cdNode = new CdNode();
            var song = new SongNode();

            tree.Artists.Add(artistNode);
            artistNode.Albums.Add(albumNode);
            albumNode.Cds.Add(cdNode);
            cdNode.Songs.Add(song);

            var visitor = new Mock<IPostbackTreeVisitor>();

            SUT.FlattenInternal(tree, visitor.Object);

            visitor.Verify(c => c.Add(cdNode), Times.Never);            
        }

    }
}
