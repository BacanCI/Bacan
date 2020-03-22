using System;
using Bakana.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceStack;

namespace Bakana
{
    public class BakanaHostBuilder
    {
        public static IBakanaHost CreateDefaultHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddInMemoryCollection()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            
            var webHost2 = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost:5000/");
                    webBuilder.UseModularStartup<BakanaStartup, BakanaStartupActivator>();
                })
                .Build();
            
            return new BakanaHost(webHost2);
        }
    }
}