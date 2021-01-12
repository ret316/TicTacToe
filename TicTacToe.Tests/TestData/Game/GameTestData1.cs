using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.Game
{
    public class GameTestData1 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");


        List<GameBL> list1 = new List<GameBL>
        {
            new GameBL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false,
                IsGameFinished = false
            },
            new GameBL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = null,
                IsPlayer2Bot = true,
                IsGameFinished = false
            }
        };
        List<GameDL> list2 = new List<GameDL>
        {
            new GameDL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false,
                IsGameFinished = false
            },
            new GameDL
            {
                Id = Id,
                GameId = GameId,
                Player1Id = PlayerId1,
                Player2Id = PlayerId2,
                IsPlayer2Bot = false,
                IsGameFinished = false
            }
        };
        List<bool> list3 = new List<bool> {true, false};

        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < list1.Count; i++)
            {
                yield return new object[] {list1[i], list2[i], list3[i]};
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
