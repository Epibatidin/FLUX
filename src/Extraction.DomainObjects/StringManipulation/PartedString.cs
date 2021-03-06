﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Extraction.DomainObjects.StringManipulation
{
    //[System.Diagnostics.DebuggerStepThrough]
    public class PartedString : IEnumerable<string>, IComparable<PartedString>
    {
        public int Count
        {
            get
            {
                if (_rawDataParts == null)
                    return 0;
                else
                    return _rawDataParts.Count();
            }
        }

        public bool Changed {get; private set;}

        public string StringValue { get; private set; }
        
        private List<string> _rawDataParts;
                
        public PartedString(string rawData)
        {
            StringValue = rawData;
        }
            
        public PartedString Split()
        {
            Split(StringValue);
            return this;
        }

        public void Split(string newData)
        {
            _rawDataParts = Splitter.ComplexSplit(newData);
            StringValue = newData;
        }

        public void RemoveAt(int position)
        {
            Changed = true;
            _rawDataParts.RemoveAt(position);
        }

        public void RemoveRange(int position, int length)
        {
            Changed = true;
            _rawDataParts.RemoveRange(position, length);
        }
        
        public override string ToString()
        {
            if (Changed)
            {
                Changed = false;
                StringValue = Splitter.Join(_rawDataParts, ' ');
            }

            return StringValue;
        }

        public string this[int i]
        {
            get
            {
                return _rawDataParts[i];
            }
            set
            {
                Changed = true;
                _rawDataParts[i] = value;
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
