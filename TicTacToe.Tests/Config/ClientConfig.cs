using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using TicTacToe.DataComponent.Config;
using TicTacToe.WebApi.Models;

namespace TicTacToe.Tests.Config
{
    public class ClientConfig
    {
        public TestServer server;
        public HttpClient client;
        public string conString;
        public string token;

        protected void GetToken()
        {
            var user = new UserAuth { Email = "test1@gmail.com", Password = "123456" };
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = client.PostAsync("api/users/authenticate", content).Result;
            var d = res.Content.ReadAsStringAsync().Result;
            var r1 = JsonConvert.DeserializeObject<AuthUser>(d);
            token = r1.Token;
        }

        protected HttpClient GetConfiguration()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("test_appsettings.json", true, true)
                .AddEnvironmentVariables().Build();

            conString = builder.GetConnectionString("DbConString");

            server ??= new TestServer(new WebHostBuilder()
                .UseEnvironment("Debug")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(builder).UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
                    loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
                })
            );

            return server.CreateClient();
        }

        public DataBaseContext ContextBuilder()
        {
            var optionsBuiler = new DbContextOptionsBuilder<DataBaseContext>();
            var options = optionsBuiler.UseNpgsql(conString).Options;
            return new DataBaseContext(options);
        }

        protected (byte[] hash, byte[] salt) GetPassHash(string password)
        {
            using (var sha = new HMACSHA512())
            {
                return (sha.ComputeHash(Encoding.UTF8.GetBytes(password)), sha.Key);
            }
        }

        
    }
}
