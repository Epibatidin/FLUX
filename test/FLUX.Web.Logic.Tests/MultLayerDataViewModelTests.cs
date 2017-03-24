using FLUX.DomainObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLUX.Web.Logic.Tests
{
    [TestFixture]
    public class MultLayerDataViewModelTests 
    {
        public MultiLayerDataViewModel SUT { get; private set; }

        [SetUp]
        public void Setup()
        {
            SUT = new MultiLayerDataViewModel(null, null, 0, null);
        }


        [Test]
        public void should_foo_for_bar()
        {

            


        }


    }
}
