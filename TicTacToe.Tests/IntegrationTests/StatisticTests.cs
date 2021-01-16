using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Enum;
using TicTacToe.DataComponent.Models;
using TicTacToe.Tests.Config;
using TicTacToe.WebApi.Models;
using Xunit;

namespace TicTacToe.Tests.IntegrationTests
{
    [Collection("Database collection")]
    public class StatisticTests
    {

        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        private HttpClient _client;
        private DataBaseContext _db;
        private DatabaseFixture _databaseFixture;

        public StatisticTests(DatabaseFixture databaseFixture)
        {
            this._client = databaseFixture.client;
            this._db = databaseFixture._context;
            this._databaseFixture = databaseFixture;
        }

        [Fact]
        public async Task Test1_GetUserStatistic()
        {
            var result = await _client.GetAsync($"api/statistics/games/{PlayerId1}");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Test2_GetGameChoronos()
        {
            var result = await _client.GetAsync($"api/statistics/history/{GameId}");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Test3_GetTop10()
        {
            var result = await _client.GetAsync($"api/statistics/");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }
    }
}
