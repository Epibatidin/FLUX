﻿using System.Collections.Generic;
using System.IO;
using DataAccess.FileSystem;
using DataAccess.XMLStub.Serialization;
using DataAccess.Interfaces;
using Extension.Test;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using NUnit.Framework;

namespace Facade.Tests
{
    public class SerializerTests : FixtureBase<object>
    {
        protected override object CreateSUT()
        {
            return null;
        }

        [Test]
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

        [Test]
        public void should_can_serialize_single_SourceItem_as_json()
        {
            var file = Create<SourceItem>();
            file.TagData = new TagData();

            var serialzed = JsonConvert.SerializeObject(file);

            var ob = JsonConvert.DeserializeObject(serialzed, typeof(SourceItem));
            var deserialized = ob as SourceItem;

            Assert.That(file.ID, Is.EqualTo(deserialized.ID));
            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
            Assert.That(deserialized.TagData, Is.Not.Null);
        }

        [Test]
        public void should_can_serialize_collection_SourceItem_as_json()
        {
            var file = Create<SourceItem>();
            file.TagData = new TagData();
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

        private SourceItem BuildSourceItem()
        {
            var sourceItem = Create<SourceItem>();
            sourceItem.TagData = new TagData()
            {
                Begin = new byte[] { 1, 2 },
                End = new byte[] { 3, 4 },
                ContentLength = 7
            };
            return sourceItem;
        }


        [Test]
        public void should_can_serialize_collection_SourceItem_as_binary()
        {
            var file = BuildSourceItem();
            file.ID = 2;
            file.Name = "sdf";
            var source = BuildSourceItem();


            var collection = new List<SourceItem>()
            {
                file,source
            };

            var jsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings());
            var memoryStream = new MemoryStream();
            var jsonWritr = new BsonWriter(memoryStream);

            jsonSerializer.Serialize(jsonWritr, collection);
            memoryStream.Position = 0;

            var reader = new BsonReader(memoryStream);
            reader.ReadRootValueAsArray = true;
            var deserialized = jsonSerializer.Deserialize<SourceItem[]>(reader)[0];

            Assert.That(file.ID, Is.EqualTo(deserialized.ID));
            Assert.That(file.Name, Is.EqualTo(deserialized.Name));
            Assert.That(file.VirtualPath, Is.EqualTo(deserialized.VirtualPath));
            Assert.That(deserialized.TagData, Is.Not.Null);
        }
    }
}
