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
using TicTacToe.DataComponent.Config;
using TicTacToe.WebApi.Models;
using Xunit;
using System.Linq;
using System.Net.Http.Headers;
using Serilog;
using TicTacToe.DataComponent.Models;
using TicTacToe.Tests.Config;
using TicTacToe.Tests.TestDataI.User;

namespace TicTacToe.Tests.IntegrationTests
{
    [Collection("Database collection")]
    public class UserTests
    {
        private static Guid Id1 = Guid.Parse("EAC9A1A1-CD29-43E0-AB07-D381615F9993");
        private static Guid Id2 = Guid.Parse("EF466876-2572-4A18-8F83-B5174E57FABA");

        private HttpClient _client;
        private DataBaseContext _db;
        private DatabaseFixture _databaseFixture;

        public UserTests(DatabaseFixture databaseFixture)
        {
            this._client = databaseFixture.client;
            this._db = databaseFixture._context;
            this._databaseFixture = databaseFixture;
        }

        [Fact]
        public async Task GetUsers()
        {
            var result = await _client.GetAsync("api/users?pageNumber=1&pageSize=10");

            var json = await result.Content.ReadAsStringAsync();
            var r1 = JsonConvert.DeserializeObject<IEnumerable<WebApi.Models.User>>(json);

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [ClassData(typeof(UserTestData1))]
        public async Task AddUser(WebApi.Models.User user, StringContent content)
        {
            var resp = await _client.PostAsync("api/users", content);

            var us = await _db.Users.FirstOrDefaultAsync(x => x.Name == user.Name && x.Email == user.Email);
            Assert.True(user.Name == us.Name && user.Email == us.Email);
        }

        private (WebApi.Models.User, StringContent) GetContent()
        {
            var user = new WebApi.Models.User
            {
                Id = null,
                Name = "Alexey",
                Email = "a25@ss.com",
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

            var resp = await _client.PutAsync($"api/users/{Id1}", content);
            //var us1 = await _db.Users.FirstOrDefaultAsync(x => x.Id == Id1);
            using (var db = _databaseFixture.ContextBuilder())
            {
                var us1 = await db.Users.FirstOrDefaultAsync(x => x.Id == Id1);
                Assert.True(user.Name == us1.Name && user.Email == us1.Email);
            }
        }

        [Fact]
        public async Task DeleteUser()
        {
            var res = await _client.DeleteAsync($"api/users/{Id2}");
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == Id2);
            Assert.Null(user);
        }
    }
}
