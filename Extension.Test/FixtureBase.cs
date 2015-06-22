using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;

namespace Extension.Test
{
    [TestFixture]
    public abstract class FixtureBase<TSystemUnderTest>
    {
        public TSystemUnderTest SUT { get; set; }
        public Fixture Fixture { get; set; }

        [SetUp]
        public void SetupBase()
        {
            Fixture = new Fixture();
            Fixture.Customize(new MultipleCustomization()).Customize(new AutoMoqCustomization());

            // Fixture.Customize<TSystemUnderTest>(r => new MethodInvoker(new GreedyConstructorQuery()));
            
            Customize();

            SUT = CreateSUT();

            PostBuild();
        }

        protected virtual void Customize()
        {
            
        }

        protected virtual void PostBuild()
        {
            
        }

        protected virtual TSystemUnderTest CreateSUT()
        {
            return Fixture.Create<TSystemUnderTest>();
        }
    }
}
