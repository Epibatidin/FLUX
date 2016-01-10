using System;
using System.IO;
using DataAccess.Interfaces;
using DataAccess.XMLStub.Serialization;

namespace DataAccess.XMLStub
{
    public class XmlVirtualFileStreamReader : IVirtualFileStreamReader
    {
        public Type GetVirtualFileArrayType() => typeof (SourceItem[]);

        public Stream OpenStreamForReadAccess(IVirtualFile virtualFile)
        {
            var asxml = virtualFile as SourceItem;
            if (asxml == null)
                throw new NotSupportedException("Non SourceItem Virtualfiles are not Supported by " +
                                                nameof(XmlVirtualFileStreamReader));

            return new TagFakeStream(asxml.TagData);
        }
    }
}
