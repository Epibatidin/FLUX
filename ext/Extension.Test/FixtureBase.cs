using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;

namespace Extension.Test
{
    public abstract class FixtureBase<TSystemUnderTest> where TSystemUnderTest : class
    {
        public TSystemUnderTest SUT { get; set; }
        public Fixture Fixture { get; set; }

        protected FixtureBase()
        {
            Fixture = new Fixture();
            Fixture.Customize(new MultipleCustomization());//.Customize(new AutoMoqCustomization());
            Fixture.Customize<TSystemUnderTest>(r => new MethodInvoker(new GreedyConstructorQuery()));

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

        protected Mock<TInterface> FreezeMock<TInterface>() where TInterface : class 
        {
            return Fixture.Freeze<Mock<TInterface>>();
        }

        protected TObject Create<TObject>()
        {
            return Fixture.Create<TObject>();
        }

        protected virtual TSystemUnderTest CreateSUT()
        {
            return Fixture.Create<TSystemUnderTest>();
        }
    }
}
