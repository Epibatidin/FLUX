using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractDataExtraction
{
    public class Progress
    {
        private float Max;

        public int Current { get; private set; }
        public float Percent
        {
            get
            {
                return Current / Max;
            }        
        }

        public Progress(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException("Max can not be Zero or less");

            Max = max;
            Reset();
        }

        public bool Done { get; private set; }

        public void DoProgress()
        {
            Done = ++Current == Max;            
        }

        public void Reset()
        {
            Done = false;
            Current = 0;
        }
    }
}
