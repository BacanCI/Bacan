using System;
using Bacan.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceStack;

namespace Bacan
{
    public class BacanHostBuilder
    {
        public static IBacanHost CreateDefaultHost(string[] args)
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
                    webBuilder.UseModularStartup<BacanStartup, BacanStartupActivator>();
                })
                .Build();
            
            return new BacanHost(webHost2);
        }
    }
}