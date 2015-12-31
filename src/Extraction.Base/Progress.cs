using System;

namespace Extraction.Base
{
    public class Progress
    {
        private readonly float _max;

        public int Current { get; private set; }
        public float Percent => Current / _max;

        public Progress(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max) + " can not be Zero or less");

            _max = max;
            Reset();
        }

        public bool Done { get; private set; }

        public void DoProgress()
        {
            Done = ++Current == _max;
        }

        public void Reset()
        {
            Done = false;
            Current = 0;
        }
    }
}
