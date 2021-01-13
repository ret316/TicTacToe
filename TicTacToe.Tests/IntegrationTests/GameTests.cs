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
using TicTacToe.DL.Config;
using TicTacToe.DL.Models;
using TicTacToe.Tests.Config;
using TicTacToe.Tests.TestDataI.Game;
using TicTacToe.WebApi.Models;
using Xunit;

namespace TicTacToe.Tests.IntegrationTests
{
    public class GameTests : ClientConfig, IDisposable
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid Id2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc14");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid GameId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc15");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        private static Guid IdA = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc20");

        public GameTests()
        {
            client = GetConfiguration();

            var game1 = new GameDL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false
            };
            var game2 = new GameDL
            {
                Id = Id2,
                GameId = GameId2,
                Player1Id = PlayerId1,
                Player2Id = null,
                IsPlayer2Bot = true
            };

            using (var db = ContextBuilder())
            {
                try
                {
                    db.Games.Add(game1);
                    db.Games.Add(game2);
                    db.SaveChanges();
                }
                catch
                {

                }

                //var (hash, salt) = GetPassHash("123456");
                //var user0 = new UserDL
                //{
                //    Id = Id,
                //    Name = "Alex",
                //    Email = "a1@ss.com",
                //    Password = hash,
                //    PasswordSalt = salt
                //};

                //db.Users.Add(user0);

            }
            GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void Dispose()
        {
            using (var db = ContextBuilder())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM \"Games\"");
                db.Database.ExecuteSqlCommand("DELETE FROM \"GameHistories\"");
                db.Database.ExecuteSqlCommand("DELETE FROM \"Users\" WHERE \"Id\" != 'F403AA84-B314-4044-93C3-AD514D35EA4A'");
            }
        }

        [Theory]
        [ClassData(typeof(GameTestData1))]
        public async Task Test1_CreateGame(StringContent content, GameDL gd0)
        {
            var result = await client.PostAsync("api/games/create", content);

            using (var db = ContextBuilder())
            {
                var game = await db.Games.FirstOrDefaultAsync(x =>
                    x.Player1Id == gd0.Player1Id && x.Player2Id == gd0.Player2Id);

                Assert.NotNull(game);
            }
        }

        [Fact]
        public async Task Test2_GetGames()
        {
            var result = await client.GetAsync("api/games/");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(GameTestData2))]
        public async Task Test3_MakeMove(StringContent content1, StringContent content2)
        {
            var result0 = await client.PostAsync("api/games/move", content1);

            var result1 = await client.PostAsync("api/games/move", content2);

            Assert.True(System.Net.HttpStatusCode.OK == result0.StatusCode);
            Assert.True(System.Net.HttpStatusCode.OK == result1.StatusCode);

            using (var db = ContextBuilder())
            {
                var r1 = await db.GameHistories.Where(x => x.GameId == GameId).ToListAsync();
                var r2 = await db.GameHistories.Where(x => x.GameId == GameId2).ToListAsync();

                Assert.True(r1.Count() == 1);
                Assert.True(r2.Count() == 2);
            }
        }

        [Theory]
        [ClassData(typeof(GameTestData3))]
        public async Task Test4_FullGame(IEnumerable<StringContent> content)
        {
            HttpResponseMessage result = null;
            foreach (var item in content)
            {
                Thread.Sleep(1000);
                var res = await client.PostAsync("api/games/move", item);
                result = res;
                Assert.Equal(System.Net.HttpStatusCode.OK, res.StatusCode);
            }

            var str = await result.Content.ReadAsStringAsync();
            Assert.Equal("Won in line", str);
        }
    }
}
