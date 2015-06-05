using System;
using System.Web;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;
using FLUXMVC.Windsor.Lifestyle;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TestHelpers;

namespace FLUXMVC.Tests.Windsor.Lifestyle
{
    public class PerCookieLifestyleAdapterTests : FixtureBase<PerCookieLifestyleAdapter>
    {
        private Mock<ICache<string, ILifetimeScope>> _cache;
        private Mock<HttpContextBase> _httpContext;

        protected override void Customize()
        {
            _cache = Fixture.Freeze<Mock<ICache<string, ILifetimeScope>>>();

            _httpContext = Fixture.Freeze<Mock<HttpContextBase>>();
            var cookies = new HttpCookieCollection();

            var request = new Mock<HttpRequestBase>();
            request.Setup(c => c.Cookies).Returns(cookies);
            _httpContext.Setup(c => c.Request).Returns(request.Object);
        }

        [Test]
        public void should_throw_invalid_operation_Exception_if_not_in_http_context()
        {
            HttpContext.Current = null;

            Assert.Throws<InvalidOperationException>(() =>
                SUT.GetScope(new CreationContext(typeof(PerCookieLifestyleAdapterTests),CreationContext.CreateEmpty(), false)));
        }

        [Test]
        public void should_add_new_default_lifestyle_scope_to_cache_when_requestcookie_lifestyle_is_null()
        {
            SUT.GetScope(_httpContext.Object);

            _cache.Verify(c => c.SetItem(It.IsAny<string>(), It.IsAny<DefaultLifetimeScope>()));
        }

        [Test]
        public void should_add_new_lifestyle_key_to_cookie()
        {
            SUT.GetScope(_httpContext.Object);

            var key = _httpContext.Object.Request.Cookies["Lifestyle"].Value;

            _cache.Verify(c => c.SetItem(key, It.IsAny<DefaultLifetimeScope>()));
        }

        [Test]
        public void should_return_new_lifestyle_that_was_added_to_cache()
        {
            var scope = SUT.GetScope(_httpContext.Object);

            _cache.Verify(c => c.SetItem(It.IsAny<string>(), scope));
        }


        [Test]
        public void should_return_cached_scope_from_cookie_key()
        {
            var key = Guid.Empty.ToString();
            _httpContext.Object.Request.Cookies.Add(new HttpCookie("Lifestyle", key));

            var scope = new DefaultLifetimeScope();
            _cache.Setup(c => c.GetItem(key)).Returns(scope).Verifiable();

            var result = SUT.GetScope(_httpContext.Object);
            
            _cache.Verify();

            Assert.That(result, Is.SameAs(scope));
        }

    }
}
