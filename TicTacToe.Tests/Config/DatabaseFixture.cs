using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Enum;

namespace TicTacToe.Tests.Config
{
    public class DatabaseFixture : ClientConfig, IDisposable
    {
        public readonly DataBaseContext _context;
        public DatabaseFixture()
        {
            client = GetConfiguration();
            _context = ContextBuilder();
            GetToken();
            InitDb();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private static Guid Id0 = Guid.Parse("2D6CD055-3C72-42CE-A22C-B4F288DFEF09");
        private static Guid Id1 = Guid.Parse("EAC9A1A1-CD29-43E0-AB07-D381615F9993");
        private static Guid Id2 = Guid.Parse("EF466876-2572-4A18-8F83-B5174E57FABA");
        private static Guid Id3 = Guid.Parse("8C4D9376-C9B5-4962-8FFA-94714788BEE3");

        private static Guid Id11 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid Id22 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc14");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid GameId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc15");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        private static Guid Id33 = Guid.Parse("2623DE4D-0CE7-4165-8F9A-9192DFCD4204");
        private static Guid Id34 = Guid.Parse("37789EB6-2C3C-4455-BEF7-0733474D2524");
        private static Guid Id35 = Guid.Parse("2A9DF59F-F731-47B7-A5A3-0F6F56BBE6EA");
        private static Guid Id36 = Guid.Parse("65386E4A-F1B9-4131-928A-63ED06B9A957");

        private static Guid Id37 = Guid.Parse("65386E4A-F1B9-4131-928A-63ED06B9A960");
        private static Guid Id38 = Guid.Parse("65386E4A-F1B9-4131-928A-63ED06B9A961");



        public void InitDb()
        {
            var (hash, salt) = GetPassHash("123456");
            var user0 = new DataComponent.Models.User
            {
                Id = Id0,
                Name = "Alex",
                Email = "a1@ss.com",
                Password = hash,
                PasswordSalt = salt
            };
            var user1 = new DataComponent.Models.User
            {
                Id = Id1,
                Name = "Alex",
                Email = "a2@ss.com",
                Password = hash,
                PasswordSalt = salt
            };
            var user2 = new DataComponent.Models.User
            {
                Id = Id2,
                Name = "Alex",
                Email = "a3@ss.com",
                Password = hash,
                PasswordSalt = salt
            };

            _context.Users.Add(user0);
            _context.Users.Add(user1);
            _context.Users.Add(user2);


            var game1 = new DataComponent.Models.Game
            {
                Id = Id11,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false
            };
            var game2 = new DataComponent.Models.Game
            {
                Id = Id22,
                GameId = GameId2,
                Player1Id = PlayerId1,
                Player2Id = null,
                IsPlayer2Bot = true
            };
            var game3 = new DataComponent.Models.Game
            {
                Id = Guid.NewGuid(),
                GameId = Id37,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false
            };
            var game4 = new DataComponent.Models.Game
            {
                Id = Guid.NewGuid(),
                GameId = Id38,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false
            };

            _context.Games.Add(game1);
            _context.Games.Add(game2);
            _context.Games.Add(game3);
            _context.Games.Add(game4);

            var game = new DataComponent.Models.Game
            {
                Id = Id33,
                GameId = Id34,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false,
                IsGameFinished = true
            };
            var result1 = new DataComponent.Models.GameResult
            {
                Id = Id35,
                GameId = Id34,
                PlayerId = PlayerId1,
                Result = ResultStatus.Won
            };
            var result2 = new DataComponent.Models.GameResult
            {
                Id = Id36,
                GameId = Id34,
                PlayerId = PlayerId2,
                Result = ResultStatus.Lost
            };
            IEnumerable<DataComponent.Models.GameHistory> list = new List<DataComponent.Models.GameHistory>
            {
                new DataComponent.Models.GameHistory
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 1,
                    MoveDate = DateTime.Parse("2020-10-10")
                },
                new DataComponent.Models.GameHistory
                {
                    PlayerId = PlayerId2,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 2,
                    YAxis = 2,
                    MoveDate = DateTime.Parse("2020-10-11")
                },
                new DataComponent.Models.GameHistory
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 0,
                    MoveDate = DateTime.Parse("2020-10-12")
                },
                new DataComponent.Models.GameHistory
                {
                    PlayerId = PlayerId2,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 2,
                    YAxis = 1,
                    MoveDate = DateTime.Parse("2020-10-13")
                },
                new DataComponent.Models.GameHistory
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 2,
                    MoveDate = DateTime.Parse("2020-10-14")
                }
            };

            _context.Games.Add(game);
            _context.GameResults.Add(result1);
            _context.GameResults.Add(result2);
            _context.GameHistories.AddRange(list);

            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM \"Users\" WHERE \"Id\" != 'F403AA84-B314-4044-93C3-AD514D35EA4A'");
            _context.Database.ExecuteSqlCommand("DELETE FROM \"Games\"");
            _context.Database.ExecuteSqlCommand("DELETE FROM \"GameHistories\"");
            _context.Database.ExecuteSqlCommand("DELETE FROM \"GameResults\"");
        }
    }
}
