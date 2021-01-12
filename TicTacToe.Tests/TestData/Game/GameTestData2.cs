using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.Game
{
    public class GameTestData2 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");

        static GameDL gd0 = new GameDL
        {
            Id = Id,
            GameId = GameId,
            Player1Id = PlayerId1,
            Player2Id = PlayerId2,
            IsPlayer2Bot = false,
            IsGameFinished = false
        };

        IEnumerable<GameDL> gdh0 = new List<GameDL>
        {
            gd0
        };

        IEnumerable<GameBL> gbh0 = new List<GameBL>
        {
            new GameBL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false,
                IsGameFinished = false
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {Id, PlayerId1, gd0, gdh0, gbh0};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
