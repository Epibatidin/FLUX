using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Common.StringManipulation
{
    public class PartedStringEnumerator : IEnumerator<string> , IEnumerator
    {
        private PartedString P;

        int Pos;

        public PartedStringEnumerator(PartedString PS)
        {
            P = PS;
            Reset();
        }
        
        public bool MoveNext()
        {
            Pos++;
            return Pos < P.Count;
        }

        public void Reset()
        {
            Pos = -1;
        }

        public object Current { get { return P[Pos]; } }
        string IEnumerator<string>.Current { get { return P[Pos]; } }

        public void Dispose()
        {

        }
    }
}
