using FleetManagementAPI;
using FleetManagementAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace FleetManagementTest
{
    class TestClientProvider : IDisposable
    {
        public HttpClient Client { get; private set; }

        private TestServer server;
        public TestClientProvider()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(s => s.AddDbContext<FleetContext>(options => options.UseSqlServer(configuration.GetConnectionString("FleetManagementDB"))));
            webHostBuilder.UseStartup<Startup>();

            server = new TestServer(webHostBuilder);

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
