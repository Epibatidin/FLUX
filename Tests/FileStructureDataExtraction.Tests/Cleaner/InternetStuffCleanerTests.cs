using System;
using System.Collections.Generic;
using Common.ISSC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tree;

namespace Cleaner.Tests
{

    using Common.StringManipulation;
    using FileStructureDataExtraction.Extraction;
    [TestClass]
    public class InternetStuffCleanerTests
    {
       
        [TestMethod]
        public void should_kill_multiple_internet_matches()
        {
            InternetStuffCleaner cleaner = new InternetStuffCleaner();

            List<Tuple<string, string>> testCases = new List<Tuple<string, string>>()
            {
                Tuple.Create("Atreyu-Congregation_Of_The_Damned[www.mp3boo.com][blub.org]", "Atreyu-Congregation_Of_The_Damned[][]"), 
                Tuple.Create("[www.mp3boo.com]Atreyu-Congregation_Of_The_Damned[blub.org]", "[]Atreyu-Congregation_Of_The_Damned[]"), 
                Tuple.Create("{www.google.com]", "{]"),
                Tuple.Create("blub.org", "")
            };

            foreach (var item in testCases)
            {
                PartedString have = new PartedString(item.Item1);

                PartedString result = null;
                result = cleaner.Filter(have);

                Assert.IsNotNull(result);
                //Assert.AreSame(result, have);
                var resText = result.ToString();
                Assert.AreEqual(item.Item2, resText);
            }

           
        }
    }
}
