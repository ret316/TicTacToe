using System;
using System.Collections;
using System.Collections.Generic;
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

namespace TicTacToe.Tests.IntegrationTests
{
    public class UserTests
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
                var us = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.PutAsync($"api/users/{us.Id}", content);
            }
            await using (var db = ContextBuiler())
            {
                var us1 = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                Assert.True(user.Name == us1.Name && user.Email == us1.Email);
            }
        }

        [Fact]
        public async Task DeteleUser()
        {
            var client = GetConfiguration();
            Guid id;
            await using (var db = ContextBuiler())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Email == "a@ss.com");
                id = user.Id;
            }

            var res = await client.DeleteAsync($"api/users/{id}");
            await using (var db = ContextBuiler())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
                Assert.Null(user);
            }
        }
    }
}
