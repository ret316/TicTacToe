using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TicTacToe.WebApi.Models;

namespace TicTacToe.Tests.TestDataI.Game
{
    public class GameTestData2 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid Id2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc14");
        private static Guid GameId = Guid.Parse("65386E4A-F1B9-4131-928A-63ED06B9A961");
        private static Guid GameId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc15");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");

        private GameHistory gh1 = new GameHistory
        {
            GameId = GameId,
            PlayerId = PlayerId1,
            XAxis = 1,
            YAxis = 1,
            IsBot = false,
            MoveDate = DateTime.Now
        };
        GameHistory gh2 = new GameHistory
        {
            GameId = GameId2,
            PlayerId = PlayerId1,
            XAxis = 1,
            YAxis = 1,
            IsBot = false,
            MoveDate = DateTime.Now
        };

        public IEnumerator<object[]> GetEnumerator()
        {
                var json = JsonConvert.SerializeObject(gh1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var json1 = JsonConvert.SerializeObject(gh2);
                var content1 = new StringContent(json1, Encoding.UTF8, "application/json");

            yield return new object[] { content, content1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
