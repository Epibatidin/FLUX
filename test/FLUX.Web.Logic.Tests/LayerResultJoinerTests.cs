using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Extension.Test;
using Xunit;

namespace FLUX.Web.Logic.Tests
{
    public class LayerResultJoinerTests : FixtureBase<LayerResultJoiner>
    {
        [Fact]
        public void should_build_tree_on_id_length()
        {
            var dict = new Dictionary<int, IVirtualFile>();
            dict.Add(1, new VFile());
            dict.Add(2, new VFile());

            //SUT.Add(dict).Build();



        }

    }
}
