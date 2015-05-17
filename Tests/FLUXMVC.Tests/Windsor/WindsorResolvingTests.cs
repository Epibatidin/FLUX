using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using FLUXMVC.Windsor;
using NUnit.Framework;

namespace FLUXMVC.Tests.Windsor
{
    [TestFixture]
    public class WindsorResolvingTests
    {
        private WindsorContainer _container;

        [TestFixtureSetUp]
        public void Setup()
        {
            _container = new WindsorContainer();

            _container.Install(new WindsorInstaller());
        }

        [TestCase("DataDelivery")]
        public void should_resolve_controllers_by_name(string name)
        {
            var hasComponent = _container.Kernel.HasComponent(name);
            Assert.That(hasComponent, Is.True);
            
            var controller = _container.Resolve<IController>(name);

            Assert.That(controller, Is.Not.Null);
        }

    }
}
