//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using DataAccess.FileSystem;
//using DataAccess.Interfaces;
//using Extension.Test;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using Xunit;
//using Assert = NUnit.Framework.Assert;
//using Is = NUnit.Framework.Is;

//namespace Facade.Tests
//{
//    public class SerializerTests : FixtureBase<object>
//    {
//        [Fact]
//        public void should_can_serialize_single_item_as_json()
//        {
//            IVirtualFile file = Create<RealFile>();

//            var serialzed = JsonConvert.SerializeObject(file);
            
//            var ob = JsonConvert.DeserializeObject<RealFile>(serialzed);
//            var deserialized  = ob as RealFile;

//            Assert.That(file.Id, Is.EqualTo(deserialized.Id));
//            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
//            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
//        }

//        [Serializable]
//        public class RealFileSub : IVirtualFile
//        {
//            public int Id { get; set; }
//            public string Name { get; set; }
//            public string VirtualPath { get; set; }
//            public Stream Open()
//            {
//                throw new NotImplementedException();
//            }
//        } 


//        [Fact]
//        public void should_can_serialize_single_item_as_binary()
//        {
//            IVirtualFile file = Create<RealFileSub>();
//            var formatter = new BinaryFormatter();

//            var stream = new MemoryStream();

//            formatter.Serialize(stream, file);
//            stream.Position = 0;
//            var ob = formatter.Deserialize(stream);
//            var deserialized = ob as RealFileSub;

//            Assert.That(file.Id, Is.EqualTo(deserialized.Id));
//            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
//            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
//        }

//    }
//}
