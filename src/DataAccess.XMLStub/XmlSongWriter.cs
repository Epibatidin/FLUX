using DataAccess.Base.Config;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using DataAccess.XMLStub.Serialization;
using System.Xml.Serialization;

namespace DataAccess.XMLStub
{
    public class XmlSongWriter
    {
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
