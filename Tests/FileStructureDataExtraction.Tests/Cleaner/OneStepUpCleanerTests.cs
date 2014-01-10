using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cleaner.Tests.Data;


using ISSC = Common.ISSC.InformationStorageSetContainer;
using TI = Tree.TreeItem<Common.ISSC.InformationStorageSetContainer>;
using Common.StringManipulation;
using Tree;
namespace Cleaner.Tests
{
    [TestClass]
    public class OneStepUpCleanerTests
    {
        private OneStepUpCleaner_Accessor Filter = new OneStepUpCleaner_Accessor();

        private void TestCases()
        {   
            /*
             * L1                       L2                      result
             * The Cure                 
             * 
             * 
             */
        }


        //[TestMethod]       
        //public void TestMethod1()
        //{
        //    TreeGenerator gen = new TreeGenerator(0, "Berzerk");
        //    gen.Add("2007 Berzerk on Rampage FYWH", new[] {
        //        "Berzerk Rampage", 
        //        "Berzerk on Rampage", 
        //        "Rampage 4"
        //    });    
        


        //    //Algo(gen.Root);
        //}


        public void TestMethod()
        {
            


        }


        //[TestMethod]
        //public void TestMethod2()
        //{
        //    var b = new PartedString("2007 Berzerk on Rampage FYWH");          

        //    MultiFilterTestData data = new MultiFilterTestData() 
        //    {
        //        //{"Hier sinnlos Berzerk on Rampage text" ,"hier sinnlos text" }, 
        //        {"Berzerk Rampage","berzerk rampage" }
        //    };

        //    data.SingleItemsAreEqual(Filter.RemoveLongestMatch, b, "LongestMatch");           

        //    //    Assert longest match > min (2, partedstring.length )

            
        //}

        [TestMethod]
        public void BLUB()
        {

            MultiFilterTestData data = new MultiFilterTestData()
            {
                {"Are You Ready To Die!","Are You Ready To Die!", "2006 from abuse to apostasy" },
                {"2003 Amduscia -melodies for the devil", "2003 melodies for the devil", "Amduscia" } ,
                {"06.melodies for the devil","06",  "2003  -melodies for the devil"}
            };

            data.SingleItemsAreEqual(Filter.RemoveLongestMatch, "BLUB");

        }
    }
}
