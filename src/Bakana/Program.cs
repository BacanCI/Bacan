using Bakana.Core;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface.Batches;
using Bakana.ServiceInterface.Mapping;
using Bakana.ServiceInterface.Validators;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Validation;
using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Bakana.AutofacModules;
using IContainer = Autofac.IContainer;

namespace Bakana
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseModularStartup<Startup>()
                .UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost:5000/")
                .Build();
            
            await host.StartAsync();
            
            Console.WriteLine("Press Enter");
            Console.ReadLine();

            using var container = GetContainer();
            var runner = container.Resolve<IConsoleRunner>();
                
            var result = await runner.Run(args); 
            
            Environment.ExitCode = result;
            
            await host.StopAsync();
        }
        
        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<OperationsModule>();

            builder.RegisterType<ConsoleRunner>()
                .AsImplementedInterfaces();

            return builder.Build();
        }
    }

    public class Startup : ModularStartup
    {
        public new void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IShortIdGenerator, ShortIdGenerator>();
            services.AddSingleton<IBatchRepository, BatchRepository>();
            services.AddSingleton<IStepRepository, StepRepository>();
            services.AddSingleton<ICommandRepository, CommandRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseServiceStack(new AppHost());

            app.Run(context =>
            {
                context.Response.Redirect("/metadata");
                return Task.FromResult(0);
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Bakana", typeof(BatchService).Assembly) { }

        public override void Configure(Container container)
        {
            container.RegisterValidators(typeof(CreateBatchArtifactOptionRequestValidator).Assembly);

            Plugins.Add(new ValidationFeature());
            Plugins.Add(new OpenApiFeature());
        }

        public override void OnAfterInit()
        {
            base.OnAfterInit();
            
            Mappers.Register();
        }
    }
}