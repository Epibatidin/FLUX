﻿//using Moq;
//using Ploeh.AutoFixture;
//using Ploeh.AutoFixture.AutoMoq;
//using Ploeh.AutoFixture.Kernel;

using Moq;
using NUnit.Framework;

namespace Extension.Test
{
    [TestFixture]
    public abstract class FixtureBase<TSystemUnderTest> where TSystemUnderTest : class
    {
        private MyAutofixture _myFixture;

        public TSystemUnderTest SUT { get; set; }
        //public Fixture Fixture { get; set; }

        protected FixtureBase()
        {
            //Fixture = new Fixture();
            //Fixture.Customize(new MultipleCustomization());
            //Fixture.Customize(new AutoMoqCustomization());
            //Fixture.Customize<TSystemUnderTest>(r => new MethodInvoker(new GreedyConstructorQuery()));

            _myFixture = new MyAutofixture();

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
            return new Mock<TInterface>();
            //return Fixture.Freeze<Mock<TInterface>>();
        }

        protected TObject Create<TObject>() where TObject : class, new()
        {
            return _myFixture.Build<TObject>();         
        }

        protected abstract TSystemUnderTest CreateSUT();        
    }
}
