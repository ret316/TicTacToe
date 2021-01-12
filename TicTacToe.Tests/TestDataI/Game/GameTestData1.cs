using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestDataI.Game
{
    public class GameTestData1 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");


        GameDL gd0 = new GameDL
        {
            GameId = GameId,
            Player1Id = PlayerId1,
            Player2Id = PlayerId2,
            IsPlayer2Bot = false,
            IsGameFinished = false
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            var json = JsonConvert.SerializeObject(gd0);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            yield return new object[] {content, gd0};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
