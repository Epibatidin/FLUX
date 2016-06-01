using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.Interfaces;
using FLUX.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace FLUX.Web.Logic
{
    public class VirtualFilePeristentHelper : IVirtualFilePeristentHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializer _serializer;

        public VirtualFilePeristentHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _serializer = JsonSerializer.CreateDefault();
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public void SaveSource(IList<IVirtualFile> sourceData)
        {
            byte[] data = null;

            using (var memoryStream = new MemoryStream())
            using (BsonWriter writer = new BsonWriter(memoryStream))
            {
                _serializer.Serialize(writer, sourceData);
                data = memoryStream.ToArray();
            }
            
            Session.Set("Source", data);
        }

        public IList<IVirtualFile> LoadSource(Type virtualFileConcreteType)
        {
            byte[] data = null;
            
            if (!Session.TryGetValue("Source", out data)) return null;
            IEnumerable<IVirtualFile> virtualFileData = null;
            using (var ms = new MemoryStream(data))
            using (var reader = new BsonReader(ms))
            {
                reader.ReadRootValueAsArray = true;
                virtualFileData = _serializer.Deserialize(reader, virtualFileConcreteType) as IEnumerable<IVirtualFile>;
            }
            return virtualFileData?.ToList();
        }

        public void SaveProviderName(string name)
        {
            Session.SetString("provider", name);
        }

        public void SaveActiveGrp(string name)
        {
            Session.SetString("grp", name);
        }

        public string LoadProviderName()
        {
            return Session.GetString("provider");
        }

        public string LoadActiveGrp()
        {
            return Session.GetString("grp");
        }
    }
}
