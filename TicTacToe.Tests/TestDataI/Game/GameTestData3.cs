using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TicTacToe.WebApi.Models;

namespace TicTacToe.Tests.TestDataI.Game
{
    public class GameTestData3 : IEnumerable<object[]>
    {
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        private static IEnumerable<StringContent> list = new List<StringContent>
        {
            new StringContent(JsonConvert.SerializeObject(new GameHistoryModel
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            }), Encoding.UTF8, "application/json"),
            new StringContent(JsonConvert.SerializeObject(new GameHistoryModel
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-11")
            }), Encoding.UTF8, "application/json"),
            new StringContent(JsonConvert.SerializeObject(new GameHistoryModel
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-12")
            }), Encoding.UTF8, "application/json"),
            new StringContent(JsonConvert.SerializeObject(new GameHistoryModel
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-13")
            }), Encoding.UTF8, "application/json"),
            new StringContent(JsonConvert.SerializeObject(new GameHistoryModel
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-14")
            }), Encoding.UTF8, "application/json")
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {list};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
