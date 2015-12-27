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
            _dataAtBegin = new MemoryStream(_data.Begin);
            _dataAtEnd = new MemoryStream(_data.End);

            _length = _dataAtBegin.Length + _data.ContentLength + _dataAtEnd.Length;
        }


        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;
        public override long Length => _length;

        public override void Flush()
        {
        }

        private long NullDataLength;
        private long _length;
        

        private readonly MemoryStream _dataAtBegin;
        private readonly MemoryStream _dataAtEnd;

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
                if (_position < _dataAtBegin.Length)
                    _dataAtBegin.Position = _position;

                if (_position > _dataAtBegin.Length + _data.ContentLength)
                    _dataAtEnd.Position = _position - _dataAtBegin.Length + _data.ContentLength;
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
                if (Position < _dataAtBegin.Length)
                {
                    _dataAtBegin.Read(buffer, i, 1);
                    Position++;
                    result++;
                }
                else if ((Position >= _dataAtBegin.Length) && (Position < _data.ContentLength + _dataAtBegin.Length))
                {
                    // der null daten bereich                     
                    buffer[i] = 0;
                    result++;
                    Position++;
                }
                else
                {
                    int dummy = _dataAtEnd.Read(buffer, i, 1);
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
