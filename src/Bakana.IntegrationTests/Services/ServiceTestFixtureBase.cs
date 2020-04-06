using Bakana.Core;
using Bakana.Core.Repositories;
using Funq;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.IntegrationTests.Services
{
    public abstract class ServiceTestFixtureBase<T> where T : Service
    {
        private const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost appHost;

        protected IServiceClient Sut { get; set; }

        protected ServiceTestFixtureBase()
        {
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }
        
        [OneTimeSetUp]
        public virtual void OneTimeSetup()
        {
            Sut = new JsonServiceClient(BaseUri);
        }
        
        [OneTimeTearDown]
        public virtual void OneTimeTearDown() => appHost.Dispose();

        private class AppHost : AppSelfHostBase
        {
            public AppHost() : base(typeof(T).Name, typeof(T).Assembly) { }

            public override void Configure(Container container)
            {
                var dbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
                using (var db = dbConnectionFactory.Open())
                {
                    db.DropBakanaTables();
                    db.CreateBakanaTables();
                }
            
                container.AddSingleton<IDbConnectionFactory>(dbConnectionFactory);
                container.AddSingleton<IShortIdGenerator, ShortIdGenerator>();
                container.AddSingleton<IBatchRepository, BatchRepository>();
                container.AddSingleton<IStepRepository, StepRepository>();
                container.AddSingleton<ICommandRepository, CommandRepository>();
            }
        }
    }
}