using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TicTacToe.Tests.IntegrationTests
{
    public class GameTests
    {
        public TestServer server;
        public string conString;

        private HttpClient GetConfiguration()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("test_appsettings.json", true, true)
                .AddEnvironmentVariables().Build();

            conString = builder.GetConnectionString("DbConString");

            server ??= new TestServer(new WebHostBuilder()
                .UseEnvironment("Debug")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(builder).UseStartup<Startup>());

            return server.CreateClient();
        }

        public Task GetGames()
        {
            var client = GetConfiguration();

            return null;
        }
    }
}
