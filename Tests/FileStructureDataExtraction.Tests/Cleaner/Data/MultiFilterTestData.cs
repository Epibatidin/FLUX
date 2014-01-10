using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.StringManipulation;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cleaner.Tests.Data
{
    //[System.Diagnostics.DebuggerStepThrough]
    public class MultiFilterTestData : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator() { return null; }
        public List<PartedString> Have { get; private set; }
        public List<PartedString> expected { get; private set; }
        public List<PartedString> Extra { get; private set; }


        public MultiFilterTestData()
        {
            Have = new List<PartedString>();
            expected = new List<PartedString>();
            Extra = new List<PartedString>();
        }

        public void Add(string have, string should)
        {
            Have.Add(new PartedString(have));
            expected.Add(new PartedString(should));
        }

        public void Add(string have, string should, string extra)
        {
            Have.Add(new PartedString(have));
            expected.Add(new PartedString(should));
            Extra.Add(new PartedString(extra));
                
        }


        public void SingleItemsAreEqual(Action<PartedString, PartedString> processor, string TestName)
        {
            for (int i = 0; i < Have.Count; i++)
            {
                processor(Have[i], Extra[i]);
                PartedAreEqual(expected[i], Have[i], TestName);
            }
        }

        public void SingleItemsAreEqual(Action<PartedString, PartedString> processor, PartedString B, string TestName)
        {
            for (int i = 0; i < Have.Count; i++)
            {
                processor(Have[i], B);
                PartedAreEqual(expected[i], Have[i], TestName);
            }
        }

        public void SingleItemsAreEqual(Func<PartedString, PartedString> processor, string TestName)
        {
            for (int i = 0; i < Have.Count; i++)
			{
                var result = processor(Have[i]);
                PartedAreEqual(expected[i], result, TestName);
			}
        }


        public void AreEqual(List<PartedString> result, string TestName)
        {
            Assert.AreEqual(expected.Count, result.Count, "Size check failed for " + TestName);

            for (int i = 0; i < expected.Count ; i++)
            {
                PartedAreEqual(expected[i], result[i], TestName);
            }
        }

        private void PartedAreEqual(PartedString expect, PartedString actual, string TestName)
        {
            var exp = expect.ToString();
            var res = actual.ToString();
            Assert.AreEqual(exp, res, TestName);
        }



    }
}
