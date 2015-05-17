using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FLUXMVC.App_Start;
using FLUXMVC.Windsor;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TestHelpers;

namespace FLUXMVC.Tests
{
    [TestFixture]
    public class ApplicationStarterTests : FixtureBase<ApplicationStarter>
    {
        private Mock<IWindsorContainer> _container;

        protected override void Customize()
        {
            _container = Fixture.Freeze<Mock<IWindsorContainer>>();

            RouteTable.Routes.Clear();
        }


        [Test]
        public void should_install_windsor_container()
        {
            SUT.Setup();

            _container.Verify(c => c.Install(It.Is<IWindsorInstaller[]>(r => r.Any(k => k.GetType() == typeof(WindsorInstaller)))));
        }

        [Test]
        public void should_setup_controller_factory()
        {
            SUT.Setup();

            Assert.That(ControllerBuilder.Current.GetControllerFactory(), Is.TypeOf<WindsorControllerFactory>());
        }

        [Test]
        public void should_register_routes()
        {
            RouteTable.Routes.Clear();

            SUT.Setup();

            Assert.That(RouteTable.Routes.Count, Is.GreaterThan(0));
        }
    }
}
