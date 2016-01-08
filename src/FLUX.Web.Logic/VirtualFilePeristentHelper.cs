using System;
using System.Collections.Generic;
using System.IO;
using DataAccess.Interfaces;
using FLUX.Interfaces;
using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace FLUX.Web.Logic
{
    public class VirtualFilePeristentHelper : IVirtualFilePeristentHelper
    {
        private readonly ISessionFeature _sessionAccessor;
        private JsonSerializer _serializer;

        public VirtualFilePeristentHelper(ISessionFeature sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
            _serializer = JsonSerializer.CreateDefault();
            
        }

        public void SaveSource(IDictionary<string, IVirtualFile> sourceData)
        {
            byte[] data = null;

            using (var memoryStream = new MemoryStream())
            using (BsonWriter writer = new BsonWriter(memoryStream))
            {
                _serializer.Serialize(writer, sourceData.Values);
                data = memoryStream.ToArray();
            }

            var session = _sessionAccessor.Session;
            session.Set("Source", data);
        }

        public IDictionary<string, IVirtualFile> LoadSource(Type virtualFileConcreteType)
        {
            throw new NotImplementedException();
        }
    }
}
