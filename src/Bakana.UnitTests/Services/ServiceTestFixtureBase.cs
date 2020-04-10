using Bakana.ServiceInterface;
using Bakana.ServiceInterface.Mapping;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace Bakana.UnitTests.Services
{
    public abstract class ServiceTestFixtureBase<T> where T : Service
    {
        private readonly ServiceStackHost appHost;
        protected T Sut { get; set; }

        protected ServiceTestFixtureBase()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<T>();
            
            ConfigureAppHost(appHost.Container);
        }

        protected virtual void ConfigureAppHost(IContainer container)
        {
        }
        
        [OneTimeSetUp]
        public virtual void OneTimeSetup()
        {
            Sut = appHost.Resolve<T>();
            
            Mappers.Register();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();
    }
}