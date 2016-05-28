using System.Collections.Generic;
using System.IO;
using DataAccess.FileSystem;
using DataAccess.XMLStub.Serialization;
using DataAccess.Interfaces;
using Extension.Test;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace Facade.Tests
{
    public class SerializerTests : FixtureBase<object>
    {
        [Fact]
        public void should_can_serialize_single_RealFile_as_json()
        {
            IVirtualFile file = Create<RealFile>();

            var serialzed = JsonConvert.SerializeObject(file);

            var ob = JsonConvert.DeserializeObject(serialzed, typeof(RealFile));
            var deserialized = ob as RealFile;

            Assert.That(file.ID, Is.EqualTo(deserialized.ID));
            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
            Assert.That(file.PathParts, Is.EqualTo(deserialized.PathParts));
        }

        [Fact]
        public void should_can_serialize_single_SourceItem_as_json()
        {
            var file = Create<SourceItem>();

            var serialzed = JsonConvert.SerializeObject(file);

            var ob = JsonConvert.DeserializeObject(serialzed, typeof(SourceItem));
            var deserialized = ob as SourceItem;

            Assert.That(file.ID, Is.EqualTo(deserialized.ID));
            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
            Assert.That(deserialized.TagData, Is.Not.Null);
        }

        [Fact]
        public void should_can_serialize_collection_SourceItem_as_json()
        {
            var file = Create<SourceItem>();
            IList<IVirtualFile> collection = new List<IVirtualFile>()
            {
                file,
                Create<SourceItem>()
            };
            
            var serialzed = JsonConvert.SerializeObject(collection);

            var ob = JsonConvert.DeserializeObject(serialzed, typeof(List<SourceItem>));
            var deserialized = (ob as List<SourceItem>)[0];

            Assert.That(file.ID, Is.EqualTo(deserialized.ID));
            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
            Assert.That(deserialized.TagData, Is.Not.Null);
        }

        public class WhatEver
        {
            public string Property { get; set; }
        }

        [Fact]
        public void should_can_serialize_collection_SourceItem_as_binary()
        {
            var file = Create<SourceItem>();
            var collection = new List<SourceItem>()
            {
                Create<SourceItem>(),
                file
            };

            var jsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings());
            var memoryStream = new MemoryStream();
            var jsonWritr = new BsonWriter(memoryStream);

            jsonSerializer.Serialize(jsonWritr, collection);
            memoryStream.Position = 0;

            var reader = new BsonReader(memoryStream);
            reader.ReadRootValueAsArray = true;
            var deserialized = jsonSerializer.Deserialize<SourceItem[]>(reader)[1];

            Assert.That(file.ID, Is.EqualTo(deserialized.ID));
            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
            Assert.That(deserialized.TagData, Is.Not.Null);

        }

    }
}
