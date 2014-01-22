using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileStructureDataExtraction.Cleaner.CursesRepair;
using NUnit.Framework;

namespace FileStructureDataExtraction.Tests.Cleaner.CursesRepair
{
    [TestFixture]
    public class ShitRepairTests
    {
        private ShitRepair _repairer;

        [SetUp]
        public void SetUp()
        {
            _repairer = new ShitRepair();
        }

        
        [TestCase("s**t")]
        public void should_repair_curse(string defectCurse)
        {
            var result = _repairer.TryFix(defectCurse);

            Assert.That(result, Is.EqualTo("shit"));
            Assert.That(_repairer.Fixed, Is.True);

        }

        [TestCase("shit")]
        [TestCase("s*i*")]
        public void should_not_change_non_curses(string defectCurse)
        {
            var result = _repairer.TryFix(defectCurse);

            Assert.That(result, Is.EqualTo(defectCurse));
            Assert.That(_repairer.Fixed, Is.False);
        }
    }
}
