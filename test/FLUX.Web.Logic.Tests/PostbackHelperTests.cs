using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace FLUX.Web.Logic.Tests
{
    public class PostbackHelperTests
    {
        
        [TestCase("POST")]
        [TestCase("pOsT")]
        public void should_be_postback_for_(string value)
        {
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            context.Setup(c => c.Request).Returns(request.Object);
            request.Setup(c => c.Method).Returns(value);

            var sut = new PostbackHelper();

            var result = sut.IsPostback(context.Object);

            Assert.True(result);
        }

        [Test]
        public void should_not_be_postback_for_get()
        {
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            context.Setup(c => c.Request).Returns(request.Object);
            request.Setup(c => c.Method).Returns("get");

            var sut = new PostbackHelper();

            var result = sut.IsPostback(context.Object);

            Assert.False(result);
        }
    }
}
