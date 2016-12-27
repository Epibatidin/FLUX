using DataAccess.Interfaces;
using Extension.Test;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using NUnit.Framework;

namespace FLUX.Web.Logic.Tests
{
    public class VirtualFilePersistentHelperTests : FixtureBase<VirtualFilePeristentHelper>
    {
        private Mock<ISession> _session;
        private JsonSerializer _serializer;
        
        protected override VirtualFilePeristentHelper CreateSUT()
        {
            var sut =  new VirtualFilePeristentHelper(null);
            sut.SessionAccessor = () => _session.Object;
            return sut;
        }

        protected override void Customize()
        {
            _session = new Mock<ISession>();

            var sessionAccessor = FreezeMock<ISessionFeature>();
            sessionAccessor.Setup(c => c.Session).Returns(_session.Object);

            _serializer = new JsonSerializer();
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

        [Test]
        public void should_take_dict_values_for_serialization()
        {
            var source = new VFile()
            {
                ID = 2
            };
            var virtualFiles = new List<IVirtualFile>()
            {
                { source }
            };

            var expectedData = GetByteArray(virtualFiles);

            SUT.SaveSource(virtualFiles);
            
            _session.Verify(c => c.Set("Source", It.Is<byte[]>(r => CollectionEquals(r,expectedData, (a,b) => a != b))));
        }


        [Test]
        public void should_can_deserialize_dict_from_values()
        {
            var item0 = Create<VFile>();
            var item1 = Create<VFile>();

            var virtualFiles = new List<IVirtualFile>()
            {
                { item0 },
                { item1 }
            };

            var expectedData = GetByteArray(virtualFiles);

            _session.Setup(c => c.TryGetValue("Source", out expectedData)).Returns(true);

            var result = SUT.LoadSource(typeof(VFile[]));

            Assert.That(result[item0.ID].Name, Is.EqualTo(item0.Name));
        }

    }
}
