using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.StringManipulation;
using Cleaner.Tests.MOCK;
using Cleaner.Tests.Data;
using FileStructureDataExtraction.Cleaner;

namespace Cleaner.Tests
{
    [TestClass]
    public class EqualityCleanerTests
    {
        [TestMethod]
        public void equality_should_kill_jim()
        {
            MultiFilterTestData testData = new MultiFilterTestData() 
            {
                { "06_wizo_-_der_lustige_tagedieb-jim", "06 der lustige tagedieb" },
                { "07_wizo_-_heut_nacht-jim", "07 heut nacht" },
                { "01_wizo_-_kopf_ab_schwanz_ab_has-jim", "01 kopf ab schwanz ab has" },
                { "02_wizo_-_nana-jim", "02 nana" },
                { "03_wizo_-_jimmy-jim", "03 jimmy" },
                { "04_wizo_-_unsichtbare_frau-jim", "04 unsichtbare frau" },
                { "05_wizo_-_kleines_missgestueck-jim", "05 kleines missgestueck" },
                { "11_wizo_-_schlau_versaut_und_gutsaussehend-jim", "11 schlau versaut und gutsaussehend" },
                { "12_wizo_-_raumgleita-jim", "12 raumgleita" },
                { "13_wizo_-_miss_pickafight-jim", "13 miss pickafight" },
                { "14_wizo_-_z.g.v.-jim", "14 z.g.v." },
                { "09_wizo_-_phlughaphoem-jim", "09 phlughaphoem" },
                { "10_wizo_-_chezus-jim", "10 chezus" },
                { "08_wizo_-_egon-jim", "08 egon" }
            };

            MockedWhiteListConfig config = new MockedWhiteListConfig("the,a,you,die,an");


            EqualityCleaner cleaner = new EqualityCleaner(config);
            var result = cleaner.Filter(testData.Have);

            testData.AreEqual(result, "equality_should_kill_jim");
        
        }
    }
}
