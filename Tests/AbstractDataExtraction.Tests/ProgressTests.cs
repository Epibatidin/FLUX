using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbstractDataExtraction.Tests
{
    [TestClass]
    public class ProgressTests
    {
        [TestMethod]
        public void DoProgress_Should_Increase_Current()
        {
            Progress p = new Progress(4);

            p.DoProgress();

            Assert.AreEqual(1, p.Current);
            Assert.IsFalse(p.Done);
        }

        [TestMethod]
        public void DoProgress_Should_Set_Done_If_Current_Equals_Max()
        {
            Progress p = new Progress(1);

            p.DoProgress();

            Assert.AreEqual(1, p.Current);
            Assert.IsTrue(p.Done);
        }


        [TestMethod]
        public void DoProgress_full_Progress()
        {
            Progress p = new Progress(3);

            p.DoProgress();

            Assert.AreEqual(1, p.Current);
            Assert.IsFalse(p.Done);

            p.DoProgress();

            Assert.AreEqual(2, p.Current);
            Assert.IsFalse(p.Done);

            p.DoProgress();

            Assert.AreEqual(3, p.Current);
            Assert.IsTrue(p.Done);
        }

        [TestMethod]
        public void Should_Throw_If_Max_Is_Zero()
        {
            bool thrown = false;
            try
            {
                Progress p = new Progress(0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException E)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void Should_Throw_If_Max_Is_Less_Then_Zero()
        {
            bool thrown = false;
            try
            {
                Progress p = new Progress(-1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException E)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void Percent_Should_Calculate_Percent()
        {
            Progress p = new Progress(2);
            p.DoProgress();
            Assert.AreEqual(0.5f, p.Percent);
        }

    }
}
