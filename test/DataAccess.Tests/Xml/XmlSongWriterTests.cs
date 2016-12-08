using Extension.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.XMLStub;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests.Xml
{
    public class XmlSongWriterTests : FixtureBase<XmlSongWriter>
    {
        protected override XmlSongWriter CreateSUT()
        {
            return new XmlSongWriter();                
        }

        public void should_return_stream_for_write_access()
        {
            //var song = new Mock<ISong>().Object;

            //var stream = SUT.OpenForWriteAccess();

            //Assert.That(stream.CanWrite, Is.True);
        }
    }
}
