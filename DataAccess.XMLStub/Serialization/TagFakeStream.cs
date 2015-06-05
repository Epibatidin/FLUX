using System.IO;

namespace DataAccess.XMLStub.Serialization
{
    public class TagFakeStream : Stream
    {
        private TagData _data;
        public TagFakeStream(TagData data)
        {
            _data = data;
            // create memoryStream from Begin 
            DataAtBegin = new MemoryStream(_data.Begin);
            DataAtEnd = new MemoryStream(_data.End);

            _length = DataAtBegin.Length + DataAtEnd.Length + _data.ContentLength;
        }


        public override bool CanRead { get { return true; } }

        public override bool CanSeek { get { return true; } }
        
        public override bool CanWrite { get { return false; } }

        public override void Flush()
        {            
        }

        private long _length;
        public override long Length 
        {
            get
            {
                return _length;
            }
        }

        private MemoryStream DataAtBegin;
        private long NullDataLength;
        private MemoryStream DataAtEnd;

        private long startLength;

        private long _position;
        public override long Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                if (_position < DataAtBegin.Length)
                    DataAtBegin.Position = _position;

                if (_position > DataAtBegin.Length + _data.ContentLength)
                    DataAtEnd.Position = _position - DataAtBegin.Length + _data.ContentLength;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // if 
            int result = 0;
            for (int i = offset; i < count; i++)
            {
                // if pos < length 
                // man kann lesen 
                if (Position < DataAtBegin.Length)
                {
                    DataAtBegin.Read(buffer, i, 1);
                    Position++;
                    result++;
                }
                else if ((Position >= DataAtBegin.Length) && (Position < _data.ContentLength + DataAtBegin.Length))
                {
                    // der null daten bereich                     
                    buffer[i] = 0;
                    result++;
                    Position++;
                }
                else
                {
                    int dummy = DataAtEnd.Read(buffer, i, 1);
                    Position += dummy;
                    result += dummy;
                }
            }
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length - Position;
                    break;                
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            _length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
        }
    }
}
