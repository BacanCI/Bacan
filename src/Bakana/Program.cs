using System;
using System.IO;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Repositories;
using Bakana.ServiceInterface;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Api.OpenApi;

namespace Bakana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseModularStartup<Startup>()
                .UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost:5000/")
                .Build();

            host.Run();
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
            : base("Bakana", typeof(BatchServices).Assembly) { }

        public override void Configure(Container container)
        {
            Plugins.Add(new OpenApiFeature());
        }
    }
}