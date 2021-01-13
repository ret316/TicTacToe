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
using TicTacToe.DL.Config;
using TicTacToe.DL.Models;
using TicTacToe.Tests.Config;
using TicTacToe.WebApi.Models;
using Xunit;
using ResultStatus = TicTacToe.DL.Models.ResultStatus;

namespace TicTacToe.Tests.IntegrationTests
{
    public class StatisticTests : ClientConfig, IDisposable
    {

        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid Id2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc14");
        private static Guid Id3 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc15");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        public void Dispose()
        {
            using (var db = ContextBuilder())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM \"Games\"");
                db.Database.ExecuteSqlCommand("DELETE FROM \"GameHistories\"");
                db.Database.ExecuteSqlCommand("DELETE FROM \"GameResults\"");
                db.Database.ExecuteSqlCommand("DELETE FROM \"Users\" WHERE \"Id\" != 'F403AA84-B314-4044-93C3-AD514D35EA4A'");
            }
        }

        public StatisticTests()
        {
            client = GetConfiguration();

            var game = new GameDL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false,
                IsGameFinished = true
            };
            var result1 = new GameResultDL
            {
                Id = Id2,
                GameId = GameId,
                PlayerId = PlayerId1,
                Result = ResultStatus.Won
            };
            var result2 = new GameResultDL
            {
                Id = Id3,
                GameId = GameId,
                PlayerId = PlayerId2,
                Result = ResultStatus.Lost
            };

            IEnumerable<GameHistoryDL> list = new List<GameHistoryDL>
            {
                new GameHistoryDL
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 1,
                    MoveDate = DateTime.Parse("2020-10-10")
                },
                new GameHistoryDL
                {
                    PlayerId = PlayerId2,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 2,
                    YAxis = 2,
                    MoveDate = DateTime.Parse("2020-10-11")
                },
                new GameHistoryDL
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 0,
                    MoveDate = DateTime.Parse("2020-10-12")
                },
                new GameHistoryDL
                {
                    PlayerId = PlayerId2,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 2,
                    YAxis = 1,
                    MoveDate = DateTime.Parse("2020-10-13")
                },
                new GameHistoryDL
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 2,
                    MoveDate = DateTime.Parse("2020-10-14")
                }
            };

            using (var db = ContextBuilder())
            {
                try
                {
                    db.Games.Add(game);
                    db.GameHistories.AddRange(list);
                    db.GameResults.Add(result1);
                    db.GameResults.Add(result2);
                    db.SaveChanges();
                }
                catch
                {

                }

                //var (hash, salt) = GetPassHash("123456");
                //var user0 = new UserDL
                //{
                //    Id = IdA,
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

        [Fact]
        public async Task Test1_GetUserStatistic()
        {
            var result = await client.GetAsync($"api/statistics/games/{PlayerId1}");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Test2_GetGameChoronos()
        {
            var result = await client.GetAsync($"api/statistics/history/{GameId}");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Test3_GetTop10()
        {
            var result = await client.GetAsync($"api/statistics/");

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }
    }
}
