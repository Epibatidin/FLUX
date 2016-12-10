using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.XMLStub.Serialization;
using System.Xml.Serialization;
using DataStructure.Tree.Builder;
using DataStructure.Tree;
using DataAccess.XMLStub.Config;

namespace DataAccess.XMLStub
{
    public class XmlSongWriter
    {
        private string _persistenceRoot;

        public XmlSongWriter(XMLSourcesCollection xmlConfig)
        {
            _persistenceRoot = xmlConfig.FolderForPersist;
        }


        private SourceItem CombineVFileWithPostbackData(SourceItem sourceItem, IExtractionValueFacade song)
        {
            var resultContainer = sourceItem.Results = new ResultContainer();
            resultContainer.KVs = new List<MyKv>();
            foreach (var item in song.ToValues())
            {
                resultContainer.KVs.Add(new MyKv()
                {
                    Name = item.Item1, 
                    Value = item.Item2
                });
            }
            return sourceItem;
        }

        public void Persist(IEnumerable<IVirtualFile> vfs, IEnumerable<IExtractionValueFacade> songs)
        {
            var vf = vfs.First();

            if (vf is SourceItem)
            {
                Enhance(vfs, songs);
                return;
            }

            SerializeToXml(vfs, songs);            
        }

        private class SourcePreContainer
        {
            public int Id { get; set; }
            public IVirtualFile vfs { get; set; }
            public IList<Tuple<string, string>> Values { get; set; }
        }

        private SourcePreContainer BuildTreeItem(IExtractionValueFacade facade, IDictionary<int, IVirtualFile> vfs ,  int depth)
        {
            var container = new SourcePreContainer()
            {
                Id = facade.Id,              
                vfs = vfs[facade.Id],
                Values  = facade.ToValues()
            };
            return container;
        }

        private void SerializeToXml(IEnumerable<IVirtualFile> vfs, IEnumerable<IExtractionValueFacade> songs)
        {
            var vfAsDict = vfs.ToDictionary(c => c.ID, c => c);
            var treeBuilder = new TreeBuilder();

            var tree = treeBuilder.BuildTreeFromCollection(songs, ToKey, (item, depth) => BuildTreeItem(item, vfAsDict, depth));

            string artist = tree.Value.Values[0].Item2;

            var counter = tree.Count;
            var index = new Root()
            {
                Groups = counter,
                Name = artist                
            };

            var rootDirPath = _persistenceRoot + "\\" + tree.Value.Values[0].Item2 + "\\";
            
            using (var indexStream = new FileStream(rootDirPath + "Index.xml", FileMode.Create, FileAccess.Write))
            {
                var rootSerializer = new XmlSerializer(typeof(Root));

                rootSerializer.Serialize(indexStream, index);

                indexStream.Flush();                
            }

            var sourceSerializer = new XmlSerializer(typeof(Group));

            for (int i = 0; i < counter; i++)
            {
                using (var sourceStream = new FileStream(rootDirPath + i + ".xml", FileMode.Create, FileAccess.Write))
                {
                    var sourcesContainer = new Source();
                    sourcesContainer.Name = tree[i].Value.vfs.PathParts[1];
                    sourcesContainer.Items = ToSoureItemCollection(tree[i]);

                    var group = new Group();
                    group.Source = sourcesContainer;

                    sourceSerializer.Serialize(sourceStream, group);

                    sourceStream.Flush();
                }
            }
        }

        private List<SourceItem> ToSoureItemCollection(TreeItem<SourcePreContainer> album)
        {
            var result = new List<SourceItem>();

            foreach (var cd in album.Children)
            {
                foreach (var song in cd.Children)
                {
                    var parts = song.Value.vfs.PathParts;
                    var sourceItem = new SourceItem()
                    {
                        ID = song.Value.Id,
                        VirtualPath = string.Join("\\", parts) +"." + song.Value.vfs.Extension, 
                    };
                    var results = song.Value.Values.Select(c => new MyKv()
                    {
                        Name = c.Item1, 
                        Value = c.Item2
                    }).ToList();

                    sourceItem.Results = new ResultContainer()
                    {
                        KVs = results
                    };

                    result.Add(sourceItem);
                }
            }
            return result;
        }



        private string ToKey(IExtractionValueFacade facade, int depth)
        {
            if (depth > 3) return null;

            return facade.ToValues()[depth].Item2;
        }


        private void Enhance(IEnumerable<IVirtualFile> vfs, IEnumerable<IExtractionValueFacade> songs)
        {
            var sourceAndResult = from post in songs
                                  join SourceItem vf in vfs on post.Id equals vf.ID
                                  select CombineVFileWithPostbackData(vf, post);

            var file = new FileStream(@"D:\FluxWorkBenchFiles\XML\XMLSourceProviderSource\Amduscia\44.xml", FileMode.Create, FileAccess.Write);

            var root = new Group();
            root.Source = new Source();
            root.Source.Items = sourceAndResult.ToList();

            XmlSerializer groupSerializer = new XmlSerializer(typeof(Group));

            groupSerializer.Serialize(file, root);

            file.Flush();
            file.Dispose();
            
            return;
        }
    }
}
