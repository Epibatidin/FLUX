using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.StringManipulation
{
    [System.Diagnostics.DebuggerStepThrough]
    public class PartedString : IEnumerable<string>, IComparable<PartedString>
    {
        public int Count
        {
            get
            {
                if (rawDataParts == null)
                    return 0;
                else
                    return rawDataParts.Count();
            }
        }

        public bool Changed {get; private set;}

        private string rawData;
        private List<string> rawDataParts;


        //-----------------------------------------------------------------------------------------------------------------------
        public PartedString(string RawData)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            InitFromString(RawData);

        }

        public void ReSplit(bool ByDot)
        {
            this.rawDataParts = Splitter.Split(ToString(), ByDot);
            Changed = true;

        }

        public void InitFromString(string Rawdata)
        {
            this.rawData = Rawdata;            
            this.rawDataParts = Splitter.Split(Rawdata);
        }

        public void RemoveAt(int Position)
        {
            Changed = true;
            rawDataParts.RemoveAt(Position);
        }

        public void RemoveRange(int Position, int length)
        {
            Changed = true;
            rawDataParts.RemoveRange(Position, length);
        }


        public override string ToString()
        {
            if (Changed)
            {
                Changed = false;
                rawData = Splitter.Join(rawDataParts, ' ');
            }

            return rawData;
        }

        public string this[int i]
        {
            get
            {
                return rawDataParts[i];
            }
            set
            {
                Changed = true;
                rawDataParts[i] = value;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new PartedStringEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new PartedStringEnumerator(this);
        }

        public int CompareTo(PartedString other)
        {
            return Compare(this, other);
        }

        private static int Compare(PartedString a, PartedString b)
        {
            //if (b == null) return -2;

            int diff = a.Count - b.Count;
            if (diff == 0)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    diff = String.Compare(a[i] , b[i], true);
                    if(diff != 0)
                        return diff;
                }
                return 0;
            }            
            return diff;
        }

        //public static bool operator ==(PartedString a, PartedString b)
        //{
        //    if(b == null) return false;
        //    return Compare(a, b) == 0;
        //}

        //public static bool operator !=(PartedString a, PartedString b)
        //{
        //    if (b == null) return true;
        //    return Compare(a, b) != 0;
        //}
    }
}
