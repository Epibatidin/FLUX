using System.IO;

namespace Extension.Helper
{
    //[System.Diagnostics.DebuggerStepThrough]
    public static class StreamHelper
    {
        public static byte[] Read(this Stream stream ,int Length)
        {
            var dummy = new byte[Length];
            if (stream.Read(dummy, 0, Length) == Length)
                return dummy;
            else
                return dummy;
        }


        public static void Write(this Stream stream, byte[] data)
        {
            var length = data.Length;
            stream.Write(data, 0, length);
        }


        public static byte[] SaveReadStream(this Stream stream ,int BytesToRead, long VirtualStreamEnd)
        {
            byte[] result = null;
            if (stream.Position <= BytesToRead + VirtualStreamEnd)
                result = new byte[BytesToRead];
            else
                result = new byte[VirtualStreamEnd - stream.Position];

            stream.Read(result, 0, result.Length);

            return result;
        }


        public static void Copy(this Stream target, Stream source, long StartIndex, long EndPos)
        {
            int blockSize = 4096;
            int parts = (int) ((EndPos - StartIndex - 1 + blockSize) / blockSize);

            if (parts == 0) return;

            byte[] buffer = new byte[blockSize];

            source.Seek(StartIndex, SeekOrigin.Begin);

            for (int i = 0; i < parts-1; i++)
            {
                source.Read(buffer, 0, blockSize);
                target.Write(buffer, 0, blockSize);
            }
            int remainingbytes = (int)((EndPos - StartIndex) -  (parts -1)* blockSize);
            
            source.Read(buffer, 0, remainingbytes);
            target.Write(buffer, 0, remainingbytes);
        }
    }
}
