//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using MockUp.Config;
//using MockUp.XMLItems;
//using FrameHandler;
//using InFileDataProzessor.TagWriter;
//using InFileDataProzessor;
//using System;
//using System.Text;
//using System.Xml.Serialization;

//namespace MockUp
//{
//    public class XMLCreator
//    {
//        Mockup config;
//        DirectoryInfo root;

//        int count = 0;

//        public XMLCreator()
//        {
//            config = Mockup.Create();

//            count = 0;
//        }

        
//        public void SerializeFolder(string FolderName)
//        {
//            // create dir 
//            DirectoryInfo sourceFolder = new DirectoryInfo(Path.Combine(config.RawDataFolder, FolderName));

//            DirectoryInfo resultFolder = new DirectoryInfo(Path.Combine(config.XMLFolder, FolderName));

//            SerializeFolder(sourceFolder, resultFolder);
//        }

//        private void SerializeFolder(DirectoryInfo sourceFolder, DirectoryInfo resultFolder)
//        {
//            Root r = new Root();
//            r.Name = sourceFolder.Name;
//            r.RootPathLength = sourceFolder.Parent.FullName.Length;

//            var sourceSubFolders = sourceFolder.GetDirectories();
//            r.Groups = sourceSubFolders.Length;

//            XmlSerializer rootSerializer = new XmlSerializer(typeof(Root), "");
//            var index = new FileStream(Path.Combine(resultFolder.FullName, "Index.xml"), FileMode.Create, FileAccess.Write);

//            rootSerializer.Serialize(index, r);
//            index.Flush();
//            index.Close();

//            XmlSerializer groupSerializer = new XmlSerializer(typeof(Group), "");

//            Stream resultStream;

//            for (int i = 0; i < sourceSubFolders.Length; i++)
//            {
//                resultStream = new FileStream(Path.Combine(resultFolder.FullName, i + ".xml"), FileMode.Create, FileAccess.Write);
//                Group g = new Group();

//                g.Source = CreateXMLRepresentation(sourceSubFolders[i]);

//                groupSerializer.Serialize(resultStream, g);

//                resultStream.Flush();
//                resultStream.Close();
//            }
//        }
    



        
//        private Source CreateXMLRepresentation(DirectoryInfo subFolder)
//        {
//            Source S = new Source();
//            S.Name = subFolder.Name;
//            S.Items = new List<SourceItem>();
//            foreach (var file in subFolder.GetFiles("*.mp3", SearchOption.AllDirectories))
//            {
//                S.Items.Add(new SourceItem()
//                {
//                    ID = count,
//                    VirtualPath = file.FullName,
//                    TagData = getID3Tags(file),
//                    Name = file.Name
//                });
//                count++;
//            }
//            return S;
//        }

//        private TagData getID3Tags(FileInfo fi)
//        {
//            ContainerFactory fac = new ContainerFactory();
//            var stream = fi.OpenRead();
//            MP3Data data;
//            try
//            {
//                data = fac.Create(stream);
//                if (data == null) return null;
//                var outStream = new MemoryStream();
//                var writer = new ID3V23TagWriter(outStream);
//                writer.WriteFrame(data.Frames);

//                TagData tags = new TagData();
//                tags.ContentLength = stream.Length - 128 - outStream.Length;
//                outStream.Seek(0, SeekOrigin.Begin);
//                tags.Begin = outStream.ToArray();

//                return tags;
//            }
//            catch (Exception e)
//            {
//                return null;
//            }
//        }
//    }
//}
