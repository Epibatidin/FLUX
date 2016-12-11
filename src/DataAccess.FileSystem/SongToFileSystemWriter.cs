using DataAccess.Interfaces;
using System.Collections.Generic;

namespace DataAccess.FileSystem
{
    public class SongToFileSystemWriter
    {
        public void Write(IVirtualFileStreamReader streamReader,IEnumerable<IVirtualFile> vfs, IExtractionValueFacade facade)
        {
            // 
            var filePath = @"D:\FluxWorkBenchFiles\Working\Result";
            foreach (var item in vfs)
            {
                var sourceStream = streamReader.OpenStreamForReadAccess(item);

                var values = facade.ToValues();

            }



        }


    }
}
