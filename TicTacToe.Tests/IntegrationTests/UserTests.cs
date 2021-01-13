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
using System.Net.Http.Headers;
using Serilog;
using TicTacToe.DL.Models;
using TicTacToe.Tests.Config;
using TicTacToe.Tests.TestDataI.User;

namespace TicTacToe.Tests.IntegrationTests
{
    public class UserTests : ClientConfig, IDisposable
    {

        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid IdA = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc40");

        public UserTests()
        {
            client = GetConfiguration();

            using (var db = ContextBuilder())
            {
                var (hash, salt) = GetPassHash("123456");
                var user0 = new UserDL
                {
                    Id = IdA,
                    Name = "Alex",
                    Email = "a1@ss.com",
                    Password = hash,
                    PasswordSalt = salt
                };

                try
                {
                    db.Users.Add(user0);
                    db.SaveChanges();
                }
                catch
                {

                }

            }

            GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void Dispose()
        {
            using (var db = ContextBuilder())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM \"Users\" WHERE \"Id\" != 'F403AA84-B314-4044-93C3-AD514D35EA4A'");
            }
        }

        [Fact]
        public async Task GetUsers()
        {
            var result = await client.GetAsync("api/users?pageNumber=1&pageSize=10");

            var json = await result.Content.ReadAsStringAsync();
            var r1 = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(json);

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [ClassData(typeof(UserTestData1))]
        public async Task AddUser(UserModel user, StringContent content)
        {
            var resp = await client.PostAsync("api/users", content);

            await using (var db = ContextBuilder())
            {
                var us = await db.Users.FirstOrDefaultAsync(x => x.Name == user.Name && x.Email == user.Email);
                Assert.True(user.Name == us.Name && user.Email == us.Email);
            }
        }

        private (UserModel, StringContent) GetContent()
        {
            var user = new UserModel
            {
                Id = null,
                Name = "Alexey",
                Email = "a@ss.com",
                Password = "123456"
            };
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return (user, content);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var (user, content) = GetContent();

            var resp = await client.PutAsync($"api/users/{IdA}", content);
            await using (var db = ContextBuilder())
            {
                var us1 = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                Assert.True(user.Name == us1.Name && user.Email == us1.Email);
            }
        }

        [Fact]
        public async Task DeleteUser()
        {
            var res = await client.DeleteAsync($"api/users/{IdA}");
            await using (var db = ContextBuilder())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
                Assert.Null(user);
            }
        }
    }
}
