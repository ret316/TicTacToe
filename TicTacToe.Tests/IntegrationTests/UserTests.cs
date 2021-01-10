using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TicTacToe.DL.Config;
using TicTacToe.WebApi.Models;
using Xunit;
using System.Linq;
using Serilog;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.IntegrationTests
{
    public class UserTests
    {

        public TestServer server;
        public string conString;

        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

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
                .UseConfiguration(builder).UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => {
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

        private DataBaseContext ContextBuiler()
        {
            var optionsBuiler = new DbContextOptionsBuilder<DataBaseContext>();
            var options = optionsBuiler.UseNpgsql(conString).Options;
            return new DataBaseContext(options);
        }

        private (byte[] hash, byte[] salt) GetPassHash(string password)
        {
            using (var sha = new HMACSHA512())
            {
                return (sha.ComputeHash(Encoding.UTF8.GetBytes(password)), sha.Key);
            }
        }

        [Fact]
        public async Task GetUsers()
        {
            var client = GetConfiguration();
            var result = await client.GetAsync("api/users?pageNumber=1&pageSize=10");

            var json = await result.Content.ReadAsStringAsync();
            var r1 = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(json);

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddUser()
        {
            var user = new UserModel
            {
                Id = null,
                Name = "Alex",
                Email = "a@ss.com",
                Password = "123456"
            };
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = GetConfiguration();
            var resp = await client.PostAsync("api/users", content);

            await using (var db = ContextBuiler())
            {
                var us = await db.Users.FirstOrDefaultAsync(x => x.Name == user.Name && x.Email == user.Email);
                Assert.True(user.Name == us.Name && user.Email == us.Email);
                db.Users.Remove(us);
                await db.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task UpdateUser()
        {
            var client = GetConfiguration();
            var user = new UserModel
            {
                Id = null,
                Name = "Alexey",
                Email = "a@ss.com",
                Password = "123456"
            };
            await using (var db = ContextBuiler())
            {
                var (hash, salt) = GetPassHash("123456");
                var user0 = new UserDL
                {
                    Id = Id,
                    Name = "Alex",
                    Email = "a@ss.com",
                    Password = hash,
                    PasswordSalt = salt
                };

                await db.Users.AddAsync(user0);
                await db.SaveChangesAsync();

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.PutAsync($"api/users/{Id}", content);
            }
            await using (var db = ContextBuiler())
            {
                var us1 = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                Assert.True(user.Name == us1.Name && user.Email == us1.Email);
                db.Users.Remove(us1);
                await db.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task DeleteUser()
        {
            var client = GetConfiguration();
            await using (var db = ContextBuiler())
            {
                var (hash, salt) = GetPassHash("123456");
                var user0 = new UserDL
                {
                    Id = Id,
                    Name = "Alex",
                    Email = "a@ss.com",
                    Password = hash,
                    PasswordSalt = salt
                };

                await db.Users.AddAsync(user0);
                await db.SaveChangesAsync();
            }

            var res = await client.DeleteAsync($"api/users/{Id}");
            await using (var db = ContextBuiler())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
                Assert.Null(user);
            }
        }
    }
}
