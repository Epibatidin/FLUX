using Extraction.Layer.Tags.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Extraction.Layer.Tags
{
    public class Mp3TagVersionResolver : IMp3TagVersionResolver
    {
        private IMp3TagReader[] _supportedReaders;

        public Mp3TagVersionResolver(IServiceProvider serviceProvider)
        {
            var tagger = serviceProvider.GetServices<IMp3TagReader>();

            SetReader(tagger);
        }

        public Mp3TagVersionResolver()
        {

        }

        public void SetReader(IEnumerable<IMp3TagReader> supportedReaders)
        {
            _supportedReaders = supportedReaders.OrderBy(c => c.Order).ToArray();
        }
        
        public IMp3TagReader ResolverTagReader(Stream stream)
        {
            foreach (var reader in _supportedReaders)
            {
                stream.Seek(0, SeekOrigin.Begin);
                if (reader.Supports(stream))
                    return reader;
            }
            return null;
        }
    }

    
}