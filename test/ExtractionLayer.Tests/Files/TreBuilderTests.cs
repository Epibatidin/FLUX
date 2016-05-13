using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.FileSystem;
using Extension.Test;
using Extraction.Layer.File;
using FLUX.Web.Logic;
using Xunit;

namespace ExtractionLayer.Tests.Files
{
    public class TreBuilderTests : FixtureBase<TreeBuilder>
    {
        [Fact]
        public void should_()
        {
            var treeitems = Create<List<RealFile>>();
            
            SUT.Build(treeitems);


        }


    }
}
