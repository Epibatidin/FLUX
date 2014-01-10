using Cleaner.Tests.Data;
using Cleaner.Tests.MOCK;
using FileStructureDataExtraction.Cleaner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cleaner.Tests
{
    [TestClass]
    public class BlacklistCleanerTest
    {
        /// <summary>
        ///A test for Filter
        ///</summary>
        [TestMethod]
        public void FilterTest()
        {
            MockedBlackListConfig config = new MockedBlackListConfig("test,CDM", true);
            BlacklistCleaner target = new BlacklistCleaner(config); // TODO: Initialize to an appropriate value

            MultiFilterTestData data = new MultiFilterTestData()
            {
               { "dies ist ein test string der von cdm kommt", "dies ist ein string der von kommt" } , 
               { "dies ist ein TEST string der von cdm kommt", "dies ist ein string der von kommt" } , 
               { "dies ist ein tesT string der von cdm kommt", "dies ist ein string der von kommt" } , 
               { "dies ist ein teSt string der von cdm kommt", "dies ist ein string der von kommt" } , 
               { "dies ist ein tEst string der von CDM kommt", "dies ist ein string der von kommt" } 
            };

            data.SingleItemsAreEqual(target.Filter, "BlackListFilter");

        }
    }
}
