using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FLUXMVC.Components;
using Interfaces.Config;
using NUnit.Framework;
using Rhino.Mocks;
using TestHelpers.MVC;

namespace FLUXMVC.Tests.Components
{
    [TestFixture]
    public class VirtualFileProviderSessionHandlerTests
    {
        private HttpSessionStateBase _session;
        private IVirtualFileFactory _factory;
        
        [SetUp]
        public void Setup()
        {
            _session = new FakeHttpSessionState();
            _factory = MockRepository.GenerateStub<IVirtualFileFactory>();
        }
        
        [Test]
        public void ShouldCreateSelectListFromProviders()
        {
            _factory.Stub(c => c.AvailableProviders).Return(new List<string>()
            {
                "Provider0", "Provider1"
            });
            _factory.Stub(c => c.DefaultProviderKey).Return("Blub");

            var provider = new VirtualFileProviderSessionHandler(_session, _factory);

            var selList = provider.Providers;
            Assert.That(selList.Count(), Is.EqualTo(2));
            Assert.That(selList.First().Selected, Is.EqualTo(false));
            Assert.That(selList.Last().Selected , Is.EqualTo(false));
        }

        [Test]
        public void ShouldSetDefaultValueIfNotInSession()
        {
            string defValue = "default";
            _factory.Stub(c => c.DefaultProviderKey).Return(defValue);

            var provider = new VirtualFileProviderSessionHandler(_session, _factory);
            Assert.That(_session[VirtualFileProviderSessionHandler.SessionValueKey], Is.EqualTo(defValue));
        }

        [Test]
        public void ShouldIgnoreDefaultValueIfInSession()
        {
            string sessionVal = "sessionValue";
            _session[VirtualFileProviderSessionHandler.SessionValueKey] = sessionVal;

            var provider = new VirtualFileProviderSessionHandler(_session, _factory);
            Assert.That(_session[VirtualFileProviderSessionHandler.SessionValueKey], Is.EqualTo(sessionVal));
        }

        [Test]
        public void ShouldSetSelectedValueByDefaultValue()
        {
            _factory.Stub(c => c.AvailableProviders).Return(new List<string>()
            {
                "Provider0",
                "Provider2"
            });
            _factory.Stub(c => c.DefaultProviderKey).Return("Provider2");

            var provider = new VirtualFileProviderSessionHandler(_session, _factory);

            var selList = provider.Providers;

            Assert.That(selList.Count(), Is.EqualTo(2));
            Assert.That(selList.Last().Selected, Is.EqualTo(true));
        }

        [Test]
        public void ShouldPersistValueChangeToSession()
        {
            string newVal = "newValue";

            var provider = new VirtualFileProviderSessionHandler(_session, _factory);
            provider.CurrentProviderKey = newVal;
            Assert.That(_session[VirtualFileProviderSessionHandler.SessionValueKey], Is.EqualTo(newVal));
        }
        
        [Test]
        public void ShouldResetProviderOnKeyChange()
        {
            var provider = new VirtualFileProviderSessionHandler(_session, _factory);

            _session[VirtualFileProviderSessionHandler.SessionDataKey] = new Object();
            provider.CurrentProviderKey = "irgendeinwert";

            Assert.That(_session[VirtualFileProviderSessionHandler.SessionDataKey], Is.Null);
        }

        [Test]
        public void ShouldReturnStoredProviderIfPreviouslySet()
        {
            var fprovider = MockRepository.GenerateStub<IVirtualFileProvider>();
            _session[VirtualFileProviderSessionHandler.SessionValueKey] = "irgendeinwett";
            _session[VirtualFileProviderSessionHandler.SessionDataKey] = fprovider;
            var provider = new VirtualFileProviderSessionHandler(_session, _factory);
            _factory.AssertWasNotCalled(c => c.GetProvider(Arg<string>.Is.Anything));

            var result = provider.GetVirtualFileProvider();

            Assert.That(result, Is.SameAs(fprovider));
        }

        [Test]
        public void ShouldCreateNewProviderBySelectedValueIfEmpty()
        {
            var fprovider = MockRepository.GenerateStub<IVirtualFileProvider>();

            //_session[VirtualFileProviderSessionHandler.SessionDataKey] = fprovider;
            _factory.Stub(c => c.DefaultProviderKey).Return("Value");
            _factory.Stub(c => c.GetProvider(Arg<string>.Is.Anything)).Return(fprovider);
            
            var provider = new VirtualFileProviderSessionHandler(_session, _factory);
            var result = provider.GetVirtualFileProvider();
            
            _factory.AssertWasCalled(c => c.GetProvider(Arg.Is("Value")));
            Assert.That(result, Is.SameAs(fprovider));
        }

    }

}
