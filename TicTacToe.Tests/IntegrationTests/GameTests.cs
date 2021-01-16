using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Models;
using TicTacToe.Tests.Config;
using TicTacToe.Tests.TestDataI.Game;
using TicTacToe.WebApi.Models;
using Xunit;

namespace TicTacToe.Tests.IntegrationTests
{
    [Collection("Database collection")]
    public class GameTests
    {
        private static Guid GameId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc15");
        private static Guid Id38 = Guid.Parse("65386E4A-F1B9-4131-928A-63ED06B9A961");


        private HttpClient _client;
        private DataBaseContext _db;
        private DatabaseFixture _databaseFixture;

        public GameTests(DatabaseFixture databaseFixture)
        {
            this._client = databaseFixture.client;
            this._db = databaseFixture._context;
            this._databaseFixture = databaseFixture;
        }

        [Theory]
        [ClassData(typeof(GameTestData1))]
        public async Task Test1_CreateGame(StringContent content, DataComponent.Models.Game gd0)
        {
            var result = await _client.PostAsync("api/games/create", content);

            var game = await _db.Games.FirstOrDefaultAsync(x =>
                x.Player1Id == gd0.Player1Id && x.Player2Id == gd0.Player2Id);

            Assert.NotNull(game);
        }

        [Fact]
        public async Task Test2_GetGames()
        {
            var result = await _client.GetAsync("api/games/");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(GameTestData2))]
        public async Task Test3_MakeMove(StringContent content1, StringContent content2)
        {
            var result0 = await _client.PostAsync("api/games/move", content1);

            var result1 = await _client.PostAsync("api/games/move", content2);

            Assert.True(System.Net.HttpStatusCode.OK == result0.StatusCode);
            Assert.True(System.Net.HttpStatusCode.OK == result1.StatusCode);

            var r1 = await _db.GameHistories.Where(x => x.GameId == Id38).ToListAsync();
            var r2 = await _db.GameHistories.Where(x => x.GameId == GameId2).ToListAsync();

            Assert.True(r1.Count() == 1);
            Assert.True(r2.Count() == 2);
        }

        [Theory]
        [ClassData(typeof(GameTestData3))]
        public async Task Test4_FullGame(IEnumerable<StringContent> content)
        {
            HttpResponseMessage result = null;
            foreach (var item in content)
            {
                Thread.Sleep(1000);
                var res = await _client.PostAsync("api/games/move", item);
                result = res;
                Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            }

            var str = await result.Content.ReadAsStringAsync();
            Assert.Equal("Won in line", str);
        }
    }
}
