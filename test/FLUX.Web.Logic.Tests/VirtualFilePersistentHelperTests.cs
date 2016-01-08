﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Extension.Test;
using Microsoft.AspNet.Http.Features;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace FLUX.Web.Logic.Tests
{
    public class VirtualFilePersistentHelperTests : FixtureBase<VirtualFilePeristentHelper>
    {
        private Mock<ISession> _session;
        private JsonSerializer _serializer;

        protected override void Customize()
        {
            _session = new Mock<ISession>();

            var sessionAccessor = FreezeMock<ISessionFeature>();
            sessionAccessor.Setup(c => c.Session).Returns(_session.Object);

            _serializer = new JsonSerializer();
        }

        public class VFile : IVirtualFile
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string VirtualPath { get; set; }
        }

        private byte[] GetByteArray(object o)
        {
            var memoryStream = new MemoryStream();
            var jsonWritr = new BsonWriter(memoryStream);

            _serializer.Serialize(jsonWritr, o);

            return memoryStream.ToArray();
        }

        private bool CollectionEquals<T>(IList<T> first, IList<T> second, Func<T,T, bool> comparer)
        {
            if (first.Count != second.Count) return false;

            for (int i = 0; i < first.Count; i++)
            {
                if (comparer(first[i],second[i]))
                    return false;
            }
            return true;
        }

        [Fact]
        public void should_take_dict_values_for_serialization()
        {
            var source = new VFile()
            {
                ID = 2
            };
            var virtualFiles = new Dictionary<string, IVirtualFile>()
            {
                { "1", source }
            };

            var expectedData = GetByteArray(virtualFiles.Values);

            SUT.SaveSource(virtualFiles);
            
            _session.Verify(c => c.Set("Source", It.Is<byte[]>(r => CollectionEquals(r,expectedData, (a,b) => a != b))));
        }

    }
}
